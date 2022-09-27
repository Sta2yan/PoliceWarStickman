using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour, IOnlyActionStartEnded, IOnlyActionLose
{
    [SerializeField] private float _delayEndLevel;
    [SerializeField] private UnityEvent End;
    [SerializeField] private LevelSpawnersActivator _activator;

    private int _maxLevel;
    private int _currentLevel = 1;

    public int CurrentLevel => _currentLevel;
    public bool IsLose { get; private set; }

    public event UnityAction<int> Changed;
    public event UnityAction Started;
    public event UnityAction StartEnded;
    public event UnityAction Ended
    {
        add => End.AddListener(value);
        remove => End.RemoveListener(value);
    }
    public event UnityAction Lose;
    public event UnityAction Win;

    private void Awake()
    {
        _maxLevel = _activator.CountLevels;
    }

    public void StartGame()
    {
        Started?.Invoke();
        Changed?.Invoke(_currentLevel);
    }

    public void StartEndGame()
    {
        StartEnded?.Invoke();

        if (_currentLevel == _maxLevel)
            WinGame();
        else if (_currentLevel % 10 != 0)
            Invoke(nameof(EndGame), _delayEndLevel);
        else
            WinGame();

        _currentLevel++;
    }

    public void LoseGame()
    {
        IsLose = true;
        Lose?.Invoke();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void WinGame()
    {
        Win?.Invoke();
    }

    private void EndGame()
    {
        End?.Invoke();
    }
}

public interface IOnlyActionStartEnded
{
    public event UnityAction StartEnded;
}
public interface IOnlyActionLose
{
    public event UnityAction Lose;
}