using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelWords : ScriptableObject
{
    [SerializeField] public List<string> Words;

    [SerializeField] public int SpawnTime;

    [SerializeField] public int LevelWinScore;

    [SerializeField] public int LevelTime;
}
