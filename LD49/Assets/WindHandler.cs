using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindHandler : MonoBehaviour
{
    public ParticleSystem windParticles;
    public PlayerController player;
    public float waitTime;
    float timeToReach;
    public AudioSource windSource;
    public ScoreHandler scoreHandler;

    void Start() {
    }

    void Update() {
        if (Time.time > timeToReach) {
            timeToReach = Time.time + (waitTime + Random.Range(-2f, 2f));
            SetWind();
        }
    }

    void SetWind() {
        if (scoreHandler.score < 20) return;

        int random = 0;
        float windStrength = 2f;

        if (scoreHandler.score < 60) {
            random = Random.Range(0, 5);
            windStrength = 2f;
        } else if (scoreHandler.score < 120) {
            random = Random.Range(0, 4);
            windStrength = 2.25f;
        } else if (scoreHandler.score < 180) {
            random = Random.Range(0, 4);
            windStrength = 2.5f;
        } else if (scoreHandler.score < 240) {
            random = Random.Range(0, 3);
            windStrength = 2.5f;
        }

        if (random == 0) {
            var fo = windParticles.forceOverLifetime;
            fo.x = 20;
            player.windPower = windStrength;
            windSource.loop = true;
            windSource.Play();
        } else if (random == 1) {
            var fo = windParticles.forceOverLifetime;
            fo.x = -20;
            player.windPower = -windStrength;
            windSource.loop = true;
            windSource.Play();
        } else {
            var fo = windParticles.forceOverLifetime;
            fo.x = 0;
            player.windPower = 0;
            windSource.loop = false;
        }
    }
}
