using System;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveByTime : MonoBehaviour
{
    [SerializeField] private List<GameObject> _disableds;

    private void OnDisable()
    {
        foreach (var item in _disableds)
            item.SetActive(false);
    }

    public void DelayActive(float delay)
    {
        if (delay < 0)
            throw new ArgumentOutOfRangeException(nameof(delay));

        Invoke(nameof(Active), delay);
    }

    private void Active()
    {
        foreach (var item in _disableds)
            item.SetActive(true);
    }
}
