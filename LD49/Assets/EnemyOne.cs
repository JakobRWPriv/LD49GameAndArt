using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : Enemy
{
    public Vector3 dir;
    public Vector3 followPos;
    public Vector3 offset;
    public PlayerController player;

    public float acceleration;
    public float maxSpeed;

    public float waitTime;
    float timeToReach;

    void Start() {
        if (transform.position.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            ChangeOrderInLayer(500);
        }

        followPos = new Vector3(0, 6.7f, 0) + offset;
        dir = (followPos - transform.position).normalized;

        if (Time.time > timeToReach) {
            timeToReach = Time.time + (waitTime + Random.Range(-0.5f, 0.5f));
            offset = new Vector3(0, Random.Range(-2f, 2f), 0);
        }
    }

    void FixedUpdate() {
        if (brake) {
            if (rb2d.velocity.magnitude > 0.1f) {
                rb2d.velocity = rb2d.velocity * brakeMultiplication;
            } else {
                rb2d.velocity = Vector2.zero;
            }
        }

        SetMaxSpeed();
        rb2d.AddForce(dir * acceleration);
    }

    private void SetMaxSpeed(int multipliedValue = 1) {
        if (rb2d.velocity.x > maxSpeed * multipliedValue)
            rb2d.velocity = new Vector2(maxSpeed * multipliedValue, rb2d.velocity.y);
        if (rb2d.velocity.x < -maxSpeed * multipliedValue)
            rb2d.velocity = new Vector2(-maxSpeed * multipliedValue, rb2d.velocity.y);
        if (rb2d.velocity.y > maxSpeed * multipliedValue)
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxSpeed * multipliedValue);
        if (rb2d.velocity.y < -maxSpeed * multipliedValue)
            rb2d.velocity = new Vector2(rb2d.velocity.x, -maxSpeed * multipliedValue);
    }

    
}
