using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject damageShakeObject;
    public Rigidbody2D rb2d;
    public bool brake;
    public float brakeMultiplication = 0.9f;
    Coroutine brakeCo;

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet") {
            TakeDamage();
            KnockedBack(otherCollider.GetComponent<PlayerBullet>(), 30f);

            otherCollider.GetComponent<PlayerBullet>().HitEnemyDestroy();
        }
    }

    void TakeDamage() {
        health--;

        if (health < 1) {
            Die();
        }

        StartCoroutine(DamageShakeCo());
    }

    void KnockedBack(PlayerBullet bullet, float knockBack = 10f, bool shouldBrake = false) {
        rb2d.velocity += (Vector2)(bullet.transform.right * knockBack);

        if (!shouldBrake) return;

        if (brakeCo != null) {
            StopCoroutine(brakeCo);
        }
        brakeCo = StartCoroutine(BrakeCo(0.4f));
    }

    IEnumerator BrakeCo(float time) {
        brake = true;
        yield return new WaitForSeconds(time);
        brake = false;
    }

    IEnumerator DamageShakeCo(float shakeMultiplier = 1) {
        damageShakeObject.transform.localPosition = new Vector2(0.1f * shakeMultiplier, -0.05f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        damageShakeObject.transform.localPosition = new Vector2(-0.05f * shakeMultiplier, 0.1f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        damageShakeObject.transform.localPosition = new Vector2(0.05f * shakeMultiplier, -0.05f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        damageShakeObject.transform.localPosition = new Vector2(-0.1f * shakeMultiplier, 0.05f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        damageShakeObject.transform.localPosition = new Vector2(0, 0);
    }

    void Die() {
        Destroy(gameObject);
    }
}
