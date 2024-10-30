using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestroy : MonoBehaviour
{
    public float destroyTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", destroyTimer);
    }

    // Update is called once per frame
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
