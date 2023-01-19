using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelData : ScriptableObject
{
    [SerializeField] public int LevelNumber;
    
    [SerializeField] public List<string> Words;

    [SerializeField] public float SpawnTime;

    [SerializeField] public int LevelWinScore;

    [SerializeField] public int Life = 5;

    [SerializeField] public float WordVelocity;
}
