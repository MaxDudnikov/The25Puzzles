using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp_moves;

    private int _moves = 0;

    public static UnityAction OnWin;

    private int _piecesLeft;

    private void Awake()
    {
        _piecesLeft = Plane._grid_count * Plane._grid_count - 1;
    }
    private void OnEnable()
    {
        Restart.OnResetStats += OnResetStats;
        ControlCubes.OnStart += Check_Start;
        ControlCubes.OnGame += Check_Game;
    }
    private void OnDisable()
    {
        Restart.OnResetStats -= OnResetStats;
        ControlCubes.OnStart -= Check_Start;
        ControlCubes.OnGame -= Check_Game;
    }

    private void OnResetStats()
    {
        _moves = 0;
        _tmp_moves.text = "0";
    }

    private void Check_Start(GameObject go, Cube cube, Dot dot)
    {
        if (Vector3.Distance(go.transform.position, dot.position) < cube._Scale && !cube._IsCorrect)
        {
            cube._IsCorrect = true;
            _piecesLeft--;
        }
        else if (Vector3.Distance(go.transform.position, dot.position) > cube._Scale && cube._IsCorrect)
        {
            cube._IsCorrect = false;
            _piecesLeft++;
        }

        if (_piecesLeft == 0)
            OnWin?.Invoke();
    }

    private void Check_Game(GameObject go, Cube cube, Dot dot)
    {
        _moves++;
        _tmp_moves.text = _moves.ToString();

        if (Vector3.Distance(go.transform.position, dot.position) < cube._Scale && !cube._IsCorrect)
        {
            cube._IsCorrect = true;
            _piecesLeft--;
        }
        else if (Vector3.Distance(go.transform.position, dot.position) > cube._Scale && cube._IsCorrect)
        {
            cube._IsCorrect = false;
            _piecesLeft++;
        }

        if (_piecesLeft == 0)
            OnWin?.Invoke();
    }
}
