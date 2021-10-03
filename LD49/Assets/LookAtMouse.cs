using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 myWorldPos;
    Vector2 offset;
    float angle;
    float angleToSet;
    float angleSmoothing;
    public float speed;
    public bool lookAwayFromMouse;

	void Update () {
        mousePos = Input.mousePosition;
        myWorldPos = Camera.main.WorldToScreenPoint(transform.position);

        offset = new Vector2(mousePos.x - myWorldPos.x, mousePos.y - myWorldPos.y);
        if (!lookAwayFromMouse) {
            angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        } else {
            angle = Mathf.Atan2(offset.y, offset.x) * (Mathf.Rad2Deg - 90);
        }

        angleToSet = Mathf.SmoothDampAngle(angleToSet, angle, ref angleSmoothing, speed);
        transform.eulerAngles = new Vector3(0, 0, angleToSet);
    }
}
