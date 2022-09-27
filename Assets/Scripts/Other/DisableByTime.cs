using UnityEngine;

public class DisableByTime : MonoBehaviour
{
    [SerializeField] private float _time;

    private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= _time)
            gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if (_time < 0)
            _time = 0;
    }
}
