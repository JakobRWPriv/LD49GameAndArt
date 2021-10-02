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

    public Transform leftPoint, rightPoint;

    void Start() {
        
    }

    void Update() {
        if (transform.position.x <= 0) {
            platform.speed = (Vector3.Distance(transform.position, leftPoint.position) / 10f);
        } else if (transform.position.x > 0) {
            platform.speed = (Vector3.Distance(transform.position, rightPoint.position) / 10f);
        }

        platform.rotationToReach = -(transform.position.x * 12f);
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

        Vector3 pos = transform.localPosition;
        pos.x =  Mathf.Clamp(transform.localPosition.x, -10f, 10f);
        transform.localPosition = pos;
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
}
