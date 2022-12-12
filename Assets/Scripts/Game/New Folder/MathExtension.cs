using System;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtension
{
    public static int GetSumNumber(List<GameObject> objects, GameObject cur_cube, int idx)
    {
        var _cube = cur_cube.GetComponent<Cube>();

        int result = 0;

        for (int i = idx; i < objects.Count; i++)
        {
            if (objects[i].GetComponent<Cube>()._Number < _cube._Number)
                result++;
        }
        return result;
    }
    public static bool CorrectMagicBox(int sum_of_number, int grid_count)
    {
        int result = sum_of_number + grid_count;
        if (grid_count % 2 == 0)
            return result % 2 == 0 ? true : false;
        else
            return result % 2 != 0 ? true : false;
    }
}
