using UnityEngine;

public class BlackScreenActivator : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CameraMover mover))
            _blackScreen.SetActive(true);
    }
}
