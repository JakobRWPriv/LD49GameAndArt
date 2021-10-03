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
    public bool shouldTakeKnockback = true;

    public GameObject deathParticles;
    public GameObject deathParticlesBig;

    public bool isBig;

    public SpriteRenderer[] allSprites;

    public virtual void Awake() {
        allSprites = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public void ChangeOrderInLayer(int orderInLayerToAdd) {
        foreach(SpriteRenderer sr in allSprites) {
            sr.sortingOrder += orderInLayerToAdd;
        }
        print("CHANGE " + gameObject.name + " BY " + orderInLayerToAdd);
    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "PlayerBullet") {
            TakeDamage();
            if (shouldTakeKnockback) {
                KnockedBack(otherCollider.GetComponent<PlayerBullet>(), 30f);
            }

            otherCollider.GetComponent<PlayerBullet>().HitEnemyDestroy();
        }
    }

    void TakeDamage() {
        health--;

        if (health < 1) {
            Die();
        } else {
            AudioHandler.Instance.PlaySound(AudioHandler.Instance.enemyHurt1, 0.5f, Random.Range(0.9f, 1.5f));
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

    public void Die() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.enemyHurt1, 0.7f, Random.Range(0.6f, 0.7f));
        if (!isBig) {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        } else {
            Instantiate(deathParticlesBig, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
