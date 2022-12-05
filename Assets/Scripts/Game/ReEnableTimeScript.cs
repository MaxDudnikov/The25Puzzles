using UnityEngine;

[RequireComponent(typeof(StopWatch))]
public class ReEnableTimeScript : MonoBehaviour
{
    private StopWatch _stopWatch;

    private void OnEnable()
    {
        _stopWatch = GetComponent<StopWatch>();
        Restart.OnReEnableTime += ReEnable;
    }

    private void OnDisable()
    {
        Restart.OnReEnableTime -= ReEnable;
    }

    private void ReEnable()
    {
        if (!_stopWatch.enabled)
            _stopWatch.enabled = true;
    }
}
