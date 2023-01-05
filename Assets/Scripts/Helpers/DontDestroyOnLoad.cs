using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        List<GameObject> objects = new List<GameObject>();
        for (var i = 0; i < gameObject.transform.childCount; i++)
        {
            objects.Add(gameObject.transform.GetChild(i).gameObject);
        }

        if (objects.Count > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
