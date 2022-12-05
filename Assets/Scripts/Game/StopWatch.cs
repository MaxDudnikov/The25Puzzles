using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(StopWatch))]
public class StopWatch : MonoBehaviour
{
    private StopWatch _stopWatch;

    private TextMeshProUGUI _time;

    internal float _timer = 0f;

    private void Start()
    {
        _stopWatch = GetComponent<StopWatch>();
        _time = GetComponent<TextMeshProUGUI>();
        Game.OnWin += EndGame;
        Restart.OnResetStats += OnResetStats;
    }

    private void OnDestroy()
    {
        Game.OnWin -= EndGame;
        Restart.OnResetStats -= OnResetStats;
    }

    private void EndGame()
    {
        _stopWatch.enabled = false;
    }

    private void OnResetStats()
    {
        _timer = 0;
    }

    private void Update()
    {
        if (_timer < 86400f)
        {
            _timer += Time.deltaTime;
        }
        else
            _timer = 0;

        var seconds = (int)(_timer % 60f);
        var minutes = (int)(_timer / 60f);

        _time.text = $"{minutes} min {seconds} sec";
    }
}
