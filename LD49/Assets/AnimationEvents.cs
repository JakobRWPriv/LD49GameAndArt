using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerController player;

    public void PlayStepSound() {
        if (!player.isDead) {
            AudioHandler.Instance.PlaySound(AudioHandler.Instance.playerStep, 0.5f, Random.Range(1f, 1.5f));
        }
    }

    public void PlayTeleportAwaySound() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.teleportAway, 0.7f, 1);
    }

    public void PlayTeleportBackSound() {
        AudioHandler.Instance.PlaySound(AudioHandler.Instance.teleportBack, 0.7f, 1);
    }
}
