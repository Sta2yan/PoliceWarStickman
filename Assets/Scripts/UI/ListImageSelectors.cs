using System.Collections.Generic;
using UnityEngine;

public class ListImageSelectors : MonoBehaviour
{
    [SerializeField] private List<ImageSelectorObject> _selectors;

    private void OnEnable()
    {
        foreach (var selector in _selectors)
            selector.Changed += OnChanged;
    }

    private void OnDisable()
    {
        foreach (var selector in _selectors)
            selector.Changed -= OnChanged;
    }

    private void OnChanged(ImageSelectorObject selectorObject)
    {
        foreach (var selector in _selectors)
            if (selector != selectorObject && selector.Image != selector.RedImage)
                selector.SetDefaultColor();
    }
}
