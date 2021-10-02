using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    float rotationToSet;
    float rotationToSmoothing;
    public float rotationToReach;
    public float speed;

    void Start() {
        //StartCoroutine(Sway());
    }

    IEnumerator Sway() {
        yield return new WaitForSeconds(Random.Range(0.8f, 2f));
        rotationToReach = Random.Range(-25f, 25f);
        StartCoroutine(Sway());
        print("CHANGE");
    }

    void Update() {
        RotateSprite(rotationToReach);
    }

    public void RotateSprite(float rotation) {
        rotationToSet = Mathf.SmoothDamp(rotationToSet, rotation, ref rotationToSmoothing, speed);
        transform.eulerAngles = new Vector3(0, 0, rotationToSet);
    }
}
