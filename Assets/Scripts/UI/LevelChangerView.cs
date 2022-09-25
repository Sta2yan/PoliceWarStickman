using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChangerView : MonoBehaviour
{
    [SerializeField] private List<Image> _images;
    [SerializeField] private LevelController _levelController;
    [SerializeField] private Color _completeImage;

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
        if (index <= _images.Count)
            for (int i = 0; i < index ; i++)
                _images[i].color = _completeImage;
    }
}
