using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersHolder : MonoBehaviour
{
    #region Singleton
    public static SoldiersHolder Instance;
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] GameObject soldiersHolder;
    [SerializeField] List<Transform> soldiers;

    public void StartPlasticSoldiers()
    {
        foreach (Transform t in soldiers)
        {
            t.rotation = Quaternion.Euler(new Vector3(0, -180, 0));

        }

        soldiersHolder.SetActive(true);

        Invoke(nameof(DisableSoldiers), 25f);
    }


    private void DisableSoldiers()
    {
        soldiersHolder.SetActive(false);
    }
}
