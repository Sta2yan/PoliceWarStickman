using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageSelectorObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _template;
    [SerializeField] private SpawnObjectOnTapScreen _spawner;
    [SerializeField] private Sprite _selectImage;
    [SerializeField] private Sprite _redImage;
    [SerializeField] private int _cost;
    [SerializeField] private EnergyViewCoins _coins;

    private Image _image;
    private Sprite _defaultSprite;

    public Sprite RedImage => _redImage;
    public Sprite Image => _image.sprite;

    public event UnityAction<ImageSelectorObject> Changed;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultSprite = _image.sprite;
    }

    private void OnEnable()
    {
        _coins.CoinChanged += OnCoinChanged;
        OnCoinChanged(_coins.CurrentCount);
    }

    private void OnDisable()
    {
        _coins.CoinChanged -= OnCoinChanged;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_image.sprite != _redImage)
        {
            _spawner.SetTemplateToSpawn(_template, _cost);
            _image.sprite = _selectImage;
            Changed(this);
        }
    }

    public void SetDefaultColor()
    {
        _image.sprite = _defaultSprite;
    }

    private void OnCoinChanged(int coin)
    {
        if (coin < _cost)
            _image.sprite = _redImage;
        else
            if (_image.sprite != _selectImage)
                _image.sprite = _defaultSprite;
    }
}