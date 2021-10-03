using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : Enemy
{
    public Vector3 dir;
    public Vector3 followPos;
    public PlayerController player;

    public float acceleration;
    public float maxSpeed;


    void Update() {
        followPos = player.transform.position + new Vector3(0, 0.5f, 0);
        dir = (followPos - transform.position).normalized;
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
