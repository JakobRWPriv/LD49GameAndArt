using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public PlayerController player;

    void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "Enemy") {
             player.PlatformHit(otherCollider.gameObject);
        }
    }
}
