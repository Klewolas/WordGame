using UnityEngine;

public class GameCanvasObjectReferenceHolder : MonoBehaviour
{
    private static GameCanvasObjectReferenceHolder _instance;
    public static GameCanvasObjectReferenceHolder Instance => _instance;
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
    
    public Transform WordsSpawnParent;

}
