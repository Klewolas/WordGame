using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class ComboUpdateListener : MonoBehaviour
{
    private ComboManager _comboManager;
    
    [SerializeField] private TMP_Text _comboText;
    private Sequence _sequence;
    [SerializeField] private float textScaleRate = 0.1f;

    [Inject]
    void Construct(ComboManager comboManager)
    {
        _comboManager = comboManager;
    }
    
    void Start()
    {
        _comboManager.ComboChanged += ComboTextUpdate;
        _sequence = DOTween.Sequence();
    }

    private void OnDestroy()
    {
        _comboManager.ComboChanged -= ComboTextUpdate;
    }

    void ComboTextUpdate(int combo)
    {
        if (combo <= 5)
        {
            _comboText.gameObject.SetActive(false);
            return;
        }

        _comboText.gameObject.SetActive(true);
        _sequence.Append(transform.DOScale(
                new Vector3(Mathf.Clamp(1 + textScaleRate * combo, 1, 3f), Mathf.Clamp(textScaleRate * combo, 1, 3f), 1),
                0.3f))
            .Append(transform.DOScale(new Vector3(1, 1, 1), 0.3f));
        _sequence.Play();
        _comboText.text = "Combo x" + combo;
    }
}