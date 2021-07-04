using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _box1;

    public void Click()
    {
        System.Random rnd = new System.Random();
        List<int> _numbers = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
        int i = 0;

        for(var j = 0; j < 3; j++)
        {
            for (var k = 0; k < 3; i++, k++)
            {
                if (j == 2 && k == 2)
                    continue;
                var _num = rnd.Next(0, _box1.Length - i);
                var _val = _numbers[_num];
                _numbers.Remove(_val);
                _box1[_val].transform.position = new Vector3(-2.5f + 3 * j, 0.5f, -2.5f + 3 * k);
            }
        }
    }
}
