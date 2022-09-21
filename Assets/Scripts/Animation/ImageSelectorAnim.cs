using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class ImageSelectorAnim : MonoBehaviour
{
    [SerializeField] private float _value;
    [SerializeField] private float _speed;
    [SerializeField] private Image _energyImage;
    [SerializeField] private Color _energyImageColor;
    [SerializeField] private Image _energySlider;
    [SerializeField] private Color _energySliderColor;
    [SerializeField] private Image _energyFill;
    [SerializeField] private Color _energyFillColor;
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Color _energyTextColor;
    [SerializeField] private float _speedColor;
    [SerializeField] private LevelComplete _levelComplete;

    private void OnEnable()
    {
        _levelComplete.Completed += OnLevelComplete;
    }

    private void Start()
    {
        transform.DOScale(_value, _speed).OnComplete(() =>
        {
            _energyImage.DOColor(_energyImageColor, _speedColor);
            _energySlider.DOColor(_energySliderColor, _speedColor);
            _energyText.DOColor(_energyTextColor, _speedColor);
            _energyFill.DOColor(_energyFillColor, _speedColor);
        });
    }

    private void OnDisable()
    {
        _levelComplete.Completed -= OnLevelComplete;
    }

    private void OnLevelComplete(float delay)
    {
        Invoke(nameof(Disable), delay);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_value < 0)
            throw new ArgumentOutOfRangeException(nameof(_value));

        if (_speed < 0)
            throw new ArgumentOutOfRangeException(nameof(_speed));

        if (_speedColor < 0)
            throw new ArgumentOutOfRangeException(nameof(_speedColor));
    }
}
