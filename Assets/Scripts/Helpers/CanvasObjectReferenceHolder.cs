using UnityEngine;

public class CanvasObjectReferenceHolder : MonoBehaviour
{
    private static CanvasObjectReferenceHolder _instance;
    public static CanvasObjectReferenceHolder Instance => _instance;
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
