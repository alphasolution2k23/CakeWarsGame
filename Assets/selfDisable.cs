using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDisable : MonoBehaviour
{
    public float disableTimer = 2f;
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("disableSelf", disableTimer);
    }

    // Update is called once per frame
    void disableSelf()
    {
        this.gameObject.SetActive(false);
    }
}
