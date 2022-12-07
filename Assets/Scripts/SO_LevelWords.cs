using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class SO_LevelWords : ScriptableObject
{
    [SerializeField] public List<string> Words;
}
