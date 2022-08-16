using System;

public static class MathExtensions
{
    public static int GetSumNumber(int current_value, int current_cell)
    {
        int result = 0;

        foreach (var item in InitBlocks._blocks)
        {
            if (item._cell > current_cell && Convert.ToInt32(item._name) < current_value)
                result++;
        }

        return result;
    }
    public static bool CorrectMagicBox(int sum_of_number)
    {
        int result = sum_of_number + 5;

        return result % 2 != 0 ? true : false;
    }
}
