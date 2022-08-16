using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public delegate void d_CheckCorrectness();
    public static event d_CheckCorrectness e_CheckCorrectness;

    public delegate void d_ResetStats();
    public static event d_ResetStats e_ResetStats;

    public delegate void d_ReEnableTime();
    public static event d_ReEnableTime e_ReEnableTime;

    private void Start()
    {
        Click();
    }

    public void Click()
    {
        if (e_ResetStats != null)
            e_ResetStats.Invoke();

        System.Random rnd = new System.Random();

        List<int> _numbers = new List<int>() { };
        for (int idx = 0; idx < 24; idx++)
        {
            _numbers.Add(idx);
        }

        int i = 0;

        for (var j = 0; j < 5; j++)
        {
            for (var k = 0; k < 5; i++, k++)
            {
                if (j == 4 && k == 4)
                    continue;
                var _num = rnd.Next(0, InitBlocks._blocks.Count - i);
                var _val = _numbers[_num];
                _numbers.Remove(_val);

                InitBlocks._blocks[_val]._block.transform.localPosition = new Vector3(-3 * k + 6, 0.5f, 3 * j - 6);
                InitBlocks._blocks[_val]._x = -3 * k + 6;
                InitBlocks._blocks[_val]._z = 3 * j - 6;
                InitBlocks._blocks[_val]._cell = i + 1;
            }
        }

        if (e_CheckCorrectness != null)
            e_CheckCorrectness.Invoke();

        if (e_ReEnableTime != null)
            e_ReEnableTime.Invoke();
    }
}
