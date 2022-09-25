using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _imageSelectorsPanel;
    [SerializeField] private float _delayActiveLosePanel; 
    [SerializeField] private float _delayActiveWinPanel;

    private void OnEnable()
    {
        _levelController.Lose += OnLose;
        _levelController.Win += OnWin;
    }

    private void OnDisable()
    {
        _levelController.Lose -= OnLose;
        _levelController.Win -= OnWin;
    }

    private void OnLose()
    {
        Invoke(nameof(ActiveLosePanel), _delayActiveLosePanel);
    }

    private void OnWin()
    {
       Invoke(nameof(ActiveWinPanel), _delayActiveWinPanel);
    }

    private void ActiveLosePanel()
    {
        _losePanel.SetActive(true);
        _imageSelectorsPanel.SetActive(false);
    }

    private void ActiveWinPanel()
    {
        _winPanel.SetActive(true);
        _imageSelectorsPanel.SetActive(false);
    }
}
