using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlatformHandler platform;
    public Rigidbody2D rb2d;

    float slideSpeedToSet;
    float slideSpeedSmoothing;
    public float slideSpeed;

    float walkSpeedToSet;
    float walkSpeedSmoothing;
    public float walkCalc;
    public float walkSpeed;

    public bool isNearEdge;
    public bool isDead;

    public float rotateDirToSet;
    float rotateDirSmoothing;

    public Transform leftPoint, rightPoint;
    public Transform ballTransform;
    public Transform bulletSpawnPos;

    public Animator legAnimator;
    public Animator animator;

    public ParticleSystem sweatParticles;

    public GameObject mouthNormal, mouthOpen, mouthWorried;
    public GameObject leftArmNormal, leftArmShoot, rightArmNormal, rightArmShoot;
    public Transform leftShootPos, rightShootPos;
    public GameObject bullet;

    public bool isControllingPlatformRotation = true;
    Coroutine platformCoroutine;

    bool playedSweatParticles;
    float deadSpeed = 1;
    void Update() {
        if (isDead) {
            transform.position = new Vector3(transform.position.x, transform.position.y - deadSpeed, 0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
            animator.ResetTrigger("Shoot");
            animator.SetTrigger("Shoot");
        }

        if (transform.localPosition.x <= 0) {
            platform.speed = (Vector3.Distance(transform.position, leftPoint.position) / 10f);
        } else if (transform.localPosition.x > 0) {
            platform.speed = (Vector3.Distance(transform.position, rightPoint.position) / 10f);
        }
        if (transform.localPosition.x <= -7.3f) {
            Die();
        } else if (transform.localPosition.x >= 7.3f) {
            Die();
        }
        if (platform.rotationToReach > 80 || platform.rotationToReach < -80) {
            Die();
        }

        if (isControllingPlatformRotation) {
            platform.rotationToReach = -(transform.position.x * 8f);
        } else {
            if (Input.GetKeyDown(KeyCode.R)) {
                platform.rotationToReach = Random.Range(-50f, 50f);
            }
        }

        slideSpeed = platform.speed;

        float slideCalc = ((platform.rotationToReach / 15f));
        slideSpeedToSet = Mathf.SmoothDamp(slideSpeedToSet, slideCalc, ref slideSpeedSmoothing, slideSpeed);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            walkCalc = -walkSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            walkCalc = walkSpeed;
        }
        if ((!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))) {
            walkCalc = 0;
        }

        walkSpeedToSet = Mathf.SmoothDamp(walkSpeedToSet, walkCalc, ref walkSpeedSmoothing, 0.2f);

        transform.Translate(-Vector2.right * ((slideSpeedToSet - (walkSpeedToSet)) * Time.deltaTime));
        legAnimator.SetFloat("HorSpeed", (slideSpeedToSet - (walkSpeedToSet * Time.deltaTime)));

        float superRunFloat = Mathf.Abs((slideSpeedToSet - (walkSpeedToSet * Time.deltaTime))) * 2;

        legAnimator.SetBool("LeftKey", Input.GetKey(KeyCode.LeftArrow));
        legAnimator.SetBool("RightKey", Input.GetKey(KeyCode.RightArrow));
        legAnimator.SetBool("SuperRun", (superRunFloat > 3.9f) || (superRunFloat < -3.9f));

        if ((superRunFloat > 3.9f) || (superRunFloat < -3.9f)) {
            if (!playedSweatParticles) {
                playedSweatParticles = true;
                sweatParticles.Play();

                mouthNormal.SetActive(false);
                mouthWorried.SetActive(true);
            }
        } else {
            sweatParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            playedSweatParticles = false;

            mouthNormal.SetActive(true);
            mouthWorried.SetActive(false);
        }

        rotateDirToSet = Mathf.SmoothDamp(rotateDirToSet, walkCalc == 0 ? -2 : 2, ref rotateDirSmoothing, 0.2f);

        legAnimator.speed = Mathf.Abs((slideSpeedToSet - (walkSpeedToSet * Time.deltaTime))) * 2;
        ballTransform.Rotate(0, 0, (slideSpeedToSet - (walkSpeedToSet * Time.deltaTime)) * -(rotateDirToSet));

        Vector3 pos = transform.localPosition;
        pos.x =  Mathf.Clamp(transform.localPosition.x, -10f, 10f);
        transform.localPosition = pos;

        var playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        var mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        if (mousePos.x <= playerScreenPos.x) {
            leftArmNormal.SetActive(false);
            leftArmShoot.SetActive(true);

            rightArmNormal.SetActive(true);
            rightArmShoot.SetActive(false);

            bulletSpawnPos = leftShootPos;
        } else {
            rightArmNormal.SetActive(false);
            rightArmShoot.SetActive(true);

            leftArmNormal.SetActive(true);
            leftArmShoot.SetActive(false);

            bulletSpawnPos = rightShootPos;
        }
    }

    void Shoot() {
        Instantiate(bullet, bulletSpawnPos.position, Quaternion.identity);
    }
/*
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb2d.AddForce(-transform.right * 2f);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            rb2d.AddForce(transform.right * 2f);
        }
    }*/


    public void PlatformHit(GameObject otherCollider) {
        print("HELLO");
        if (platformCoroutine != null) {
            StopCoroutine(platformCoroutine);
        }
        platformCoroutine = StartCoroutine(LoseControlOfPlatform());
        Destroy(otherCollider);
    }

    public IEnumerator LoseControlOfPlatform() {
        isControllingPlatformRotation = false;

        if (platform.rotationToReach >= 0) {
            platform.rotationToReach += 10f;
            print("MORE");
        } else {
            platform.rotationToReach -= 10f;
            print("LESS");
        }

        yield return new WaitForSeconds(2f);

        isControllingPlatformRotation = true;
        platformCoroutine = null;
    }

    void Die() {
        print("DIE");
        transform.parent = null;
        isDead = true;
    }
}
