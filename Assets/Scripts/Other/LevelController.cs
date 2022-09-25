using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour, IOnlyActionStartEnded, IOnlyActionLose
{
    [SerializeField] private float _delayEndLevel;
    [SerializeField] private UnityEvent End;
    [SerializeField] private int _maxLevel;

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

    public void StartGame()
    {
        _currentLevel++;
        Started?.Invoke();
        Changed?.Invoke(_currentLevel);
    }

    public void StartEndGame()
    {
        StartEnded?.Invoke();

        if (_currentLevel <= _maxLevel)
            Invoke(nameof(EndGame), _delayEndLevel);
        else
            WinGame();
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