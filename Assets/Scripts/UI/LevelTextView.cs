using TMPro;
using UnityEngine;

public class LevelTextView : MonoBehaviour
{
    private const string DefaultText = "LEVEL       ";

    [SerializeField] private TMP_Text _text;
    [SerializeField] private LevelController _levelController;

    private void OnEnable()
    {
        _levelController.Changed += OnLevelChanged;
        OnLevelChanged(_levelController.CurrentLevel);
    }

    private void OnDisable()
    {
        _levelController.Changed -= OnLevelChanged;
    }

    private void OnLevelChanged(int index)
    {
        _text.text = DefaultText + index;
    }
}
