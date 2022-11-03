using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class ImageSelectorAnim : MonoBehaviour
{
    [SerializeField] private float _value;
    [SerializeField] private float _speed;
    [Header("Image")]
    [SerializeField] private Image _energyImage;
    [SerializeField] private Color _energyImageColor;
    [Header("Slider")]
    [SerializeField] private Image _energySlider;
    [SerializeField] private Color _energySliderColor;
    [Header("Fill")]
    [SerializeField] private Image _energyFill;
    [SerializeField] private Color _energyFillColor;
    [Header("Text")]
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private Color _energyTextColor;
    [Header("Other")]
    [SerializeField] private float _speedColor;

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
