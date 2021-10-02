using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    void Update() {
        transform.eulerAngles = Vector3.zero;
    }
}
