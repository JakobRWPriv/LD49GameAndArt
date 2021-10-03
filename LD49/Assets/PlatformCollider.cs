using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public PlayerController player;
    public GameObject shakeSprite;
    public Color outlineColor, upperColor, lowerColor;
    public Color outlineColorHit, upperColorHit, lowerColorHit;
    public SpriteRenderer outlineSprite, upperSprite, lowerSprite;
    public GameObject stableGem, unstableGem;

    Coroutine colorCoroutine;

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Enemy") {
             player.PlatformHit(otherCollider.gameObject);
             if (colorCoroutine != null) {
                 StopCoroutine(colorCoroutine);
             }
             otherCollider.GetComponent<Enemy>().Die();
             colorCoroutine = StartCoroutine(ChangeColorsCo());
             StartCoroutine(DamageShakeCo());
        }
    }

    void ChangeToNormalColors() {
        outlineSprite.color = outlineColor;
        upperSprite.color = upperColor;
        lowerSprite.color = lowerColor;
        stableGem.SetActive(true);
        unstableGem.SetActive(false);
    }

    void ChangeToHitColors() {
        outlineSprite.color = outlineColorHit;
        upperSprite.color = upperColorHit;
        lowerSprite.color = lowerColorHit;
        stableGem.SetActive(false);
        unstableGem.SetActive(true);
    }

    IEnumerator ChangeColorsCo() {
        ChangeToHitColors();
        yield return new WaitForSeconds(3f);
        ChangeToNormalColors();
        colorCoroutine = null;
    }

    IEnumerator DamageShakeCo(float shakeMultiplier = 1) {
        shakeSprite.transform.localPosition = new Vector2(0.1f * shakeMultiplier, -0.05f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        shakeSprite.transform.localPosition = new Vector2(-0.05f * shakeMultiplier, 0.1f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        shakeSprite.transform.localPosition = new Vector2(0.05f * shakeMultiplier, -0.05f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        shakeSprite.transform.localPosition = new Vector2(-0.1f * shakeMultiplier, 0.05f * shakeMultiplier);
        yield return new WaitForSeconds(0.05f);
        shakeSprite.transform.localPosition = new Vector2(0, 0);
    }

}
