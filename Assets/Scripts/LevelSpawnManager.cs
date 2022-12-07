using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnManager : MonoBehaviour
{
    private static LevelSpawnManager _instance;
    public static LevelSpawnManager Instance => _instance;

    private SO_LevelWords _levelData;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
