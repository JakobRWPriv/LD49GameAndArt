using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivotController : MonoBehaviour
{
    
    float leanDirToSet;
    float leanDirSmoothing;

    void Update()
    {
        float leanDir = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            leanDir = 20;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            leanDir = -20;
        }
        if ((!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))) {
            leanDir = 0;
        }

        leanDirToSet = Mathf.SmoothDamp(leanDirToSet, leanDir, ref leanDirSmoothing, 0.3f);
        transform.eulerAngles = new Vector3(0, 0, leanDirToSet);
    }
}
