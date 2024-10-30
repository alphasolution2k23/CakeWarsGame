using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayConfatee : MonoBehaviour
{
    public GameObject Confatee;
    // Start is called before the first frame update
    public void EnableConfatee()
    {
        Confatee.transform.parent = null;
        Confatee.SetActive(true);
        if (Confatee.GetComponent<PlayConfatee>())
        Confatee.GetComponent<PlayConfatee>().Invoke("OnDisableConfatee", 2.0f);
    }
    public void OnDisableConfatee()
    {
        Destroy(Confatee);
    }
}
