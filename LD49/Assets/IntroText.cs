using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        if (PlayerPrefs.GetInt("INTRO", 0) == 1) {
            Destroy(gameObject);
        } else {
            PlayerPrefs.SetInt("INTRO", 1);
            StartCoroutine(DestroyCo());
        }
    }

    IEnumerator DestroyCo() {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}
