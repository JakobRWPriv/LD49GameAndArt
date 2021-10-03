using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Vector3 moveDirection;

    Vector3 mousePos;
    Vector3 myWorldPos;
    Vector2 offset;
    float angle;
    public float speed;

    public ParticleSystem myParticleSystem;
    public Collider2D myCollider;

    public ParticleSystem destroyParticles;

    void Awake() {
        mousePos = Input.mousePosition;
        myWorldPos = Camera.main.WorldToScreenPoint(transform.position);
        offset = new Vector2(mousePos.x - myWorldPos.x, mousePos.y - myWorldPos.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    void Start() {
        moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        moveDirection.z = 0;
        moveDirection.Normalize();
        StartCoroutine(Destroy());
    }

    void Update() {
        transform.position = transform.position + moveDirection * (speed * Time.deltaTime);
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public void HitEnemyDestroy() {
        myParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        destroyParticles.Play();
        myCollider.enabled = false;
        StartCoroutine(DestroyShort());
    }

    IEnumerator DestroyShort() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
