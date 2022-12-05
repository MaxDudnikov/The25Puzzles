using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public static List<Dot> _dots = new List<Dot>();

    public static int _grid_count { get; set; }
}
public class Dot
{
    public int Id { get; set; }
    public Vector3 position { get; set; }
    public string Print()
    {
        return "Id: " + Id + '\n' +
            "Position: " + position;
    }
}
