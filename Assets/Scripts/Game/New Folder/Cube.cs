using UnityEngine;

public class Cube : MonoBehaviour
{
    public int _Id;
    public int _Number;
    public float _Scale;
    public bool _IsCorrect;

    public Cube(Cube cube)
    {
        _Id = cube._Id;
        _Number = cube._Number;
    }
    public void Refresh(Cube cube)
    {
        _Number = cube._Number;
        _Id = cube._Id;
    }
}
