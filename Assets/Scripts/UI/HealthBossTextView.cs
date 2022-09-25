using TMPro;
using UnityEngine;

public class HealthBossTextView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _health.Hitted += OnHitted;
    }

    private void Start()
    {
        _text.text = _health.CurrentHealth.ToString();
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void OnDisable()
    {
        _health.Hitted -= OnHitted;
    }

    private void OnHitted()
    {
        _text.text = _health.CurrentHealth.ToString();
    }
}
