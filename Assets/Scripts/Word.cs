using System;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [SerializeField] private TMP_Text _wordText;
    public float wordVelocity;
    private void LateUpdate()
    {
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y - wordVelocity, position.z);
    }

    public void SetText(string aliveWord)
    {
        _wordText.text = aliveWord;
    }
}