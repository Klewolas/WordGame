using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelReferenceHolder", menuName = "ScriptableObjects/Level Reference Holder", order = 2)]
public class LevelReferenceHolder : ScriptableObject
{
    public List<LevelData> levelList;
}