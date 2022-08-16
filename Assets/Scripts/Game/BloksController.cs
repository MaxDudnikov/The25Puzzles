using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class BloksController : MonoBehaviour
{
    public delegate void d_Win();
    public static event d_Win e_Win;

    [SerializeField] private TextMeshProUGUI _moves;

    private int moves = 0;

    private void OnEnable()
    {
        MoveBlocks.e_MoveBlock += OnMoveBlock;
        Restart.e_ResetStats += OnResetStats;
        Restart.e_CheckCorrectness += OnCheckCorrectness;
    }

    private void OnDisable()
    {
        MoveBlocks.e_MoveBlock -= OnMoveBlock;
        Restart.e_ResetStats -= OnResetStats;
        Restart.e_CheckCorrectness -= OnCheckCorrectness;
    }

    private IEnumerator OnCheckPositions()
    {
        yield return new WaitForFixedUpdate();

        foreach (var item in InitBlocks._blocks)
        {
            var convert = Convert.ToInt32(item._name);
            if (convert != item._cell)
                yield break;
        }

        e_Win.Invoke();
    }

    public void OnMoveBlock(GameObject block)
    {
        Transform _block = block.transform;

        RaycastHit hit;

        Vector3 _local = _block.position;
        Vector3 _start_position = _block.localPosition;

        Block _cur_block = InitBlocks._blocks.FirstOrDefault(f => f._block == block);

        if (!Physics.Linecast(_local, _local + _block.forward * 2, out hit))
        {
            _block.localPosition += Vector3.forward * 3;
            _cur_block._cell += 5;
        }

        else if (!Physics.Linecast(_local, _local - _block.forward * 2, out hit))
        {
            _block.localPosition += Vector3.forward * (-3);
            _cur_block._cell -= 5;
        }

        else if (!Physics.Linecast(_local, _local + _block.right * 2, out hit))
        {
            _block.localPosition += Vector3.right * 3;
            _cur_block._cell -= 1;
        }

        else if (!Physics.Linecast(_local, _local - _block.right * 2, out hit))
        {
            _block.localPosition += Vector3.right * (-3);
            _cur_block._cell += 1;
        }

        _cur_block._x = (int)_block.localPosition.x;
        _cur_block._z = (int)_block.localPosition.z;

        if (_start_position != _block.localPosition)
        {
            OnEraseCountOfMove();
            StartCoroutine(OnCheckPositions());
        }
    }

    private void OnEraseCountOfMove()
    {
        moves++;
        _moves.text = $"{moves}";
    }

    private void OnResetStats()
    {
        moves = 0;
        _moves.text = $"{moves}";
    }

    private void OnCheckCorrectness()
    {
        int sum = 0;

        foreach (var item in InitBlocks._blocks)
        {
            sum += MathExtensions.GetSumNumber(Convert.ToInt32(item._name), item._cell);
        }

        if (!MathExtensions.CorrectMagicBox(sum))
        {
            var _23 = InitBlocks._blocks.FirstOrDefault(f => f._cell == 23);
            var _24 = InitBlocks._blocks.FirstOrDefault(f => f._cell == 24);

            var _pos = _23._block.transform.localPosition;
            _23._block.transform.localPosition = _24._block.transform.localPosition;
            _24._block.transform.localPosition = _pos;

            _23._cell = 24;
            _24._cell = 23;

            _23._x = (int)_23._block.transform.localPosition.x;
            _23._z = (int)_23._block.transform.localPosition.z;

            _24._x = (int)_24._block.transform.localPosition.x;
            _24._z = (int)_24._block.transform.localPosition.z;
        }
    }
}