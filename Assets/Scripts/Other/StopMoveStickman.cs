using UnityEngine;

public class StopMoveStickman : MonoBehaviour
{
    [SerializeField] private StickmanMover _stickmanMover;
    [SerializeField] private AttackerAnim _anim;

    private void OnEnable()
    {
        _anim.LevelCompleted += OnLevelComplete;
    }

    private void OnDisable()
    {
        _anim.LevelCompleted -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        _stickmanMover.Stop();
    }
}
