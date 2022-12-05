using UnityEngine;
using UnityEngine.Events;

public class Restart : MonoBehaviour
{
    public static UnityAction OnResetStats;
    public static UnityAction OnReEnableTime;
    public static UnityAction OnRestart;

    public void Click()
    {
        OnResetStats?.Invoke();
        OnReEnableTime?.Invoke();
        OnRestart?.Invoke();
    }
}
