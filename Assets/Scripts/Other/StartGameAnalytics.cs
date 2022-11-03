using UnityEngine;
using GameAnalyticsSDK;

public class StartGameAnalytics : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
        Debug.Log("Complete");
    }
}
