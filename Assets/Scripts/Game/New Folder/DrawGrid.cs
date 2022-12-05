using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DrawGrid : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [Range(2,10)]
    [SerializeField] private int _grid_count;

    private GameObject obj;
    private List<GameObject> _objects = new List<GameObject>();

    private int _hashCode;
    private Material _material;

    [SerializeField] private Material[] _materials;

    private void Awake()
    {
        Draw();
        GetMaterial();
    }
    private void GetMaterial()
    {
        if (PlayerPrefs.HasKey("Material"))
        {
            _hashCode = PlayerPrefs.GetInt("Material");
            _material = _materials[_hashCode];
        }
    }

    private void OnEnable()
    {
        Restart.OnRestart += Draw;
    }

    private void OnDisable()
    {
        Restart.OnRestart -= Draw;
    }

    public void Draw()
    {
        Clear();

        _grid_count = GetGridCount();
        Plane._grid_count = _grid_count;

        var scale = _cube.transform.localScale;
        scale = new Vector3(scale.x / _grid_count, scale.y, scale.z / _grid_count);

        var leftupvert = transform.TransformPoint(GetComponent<MeshFilter>().sharedMesh.vertices[10]);

        var edge = GetEdge() / _grid_count;

        var row = 0;
        var column = 0;

        for (int i = 0; i < _grid_count * _grid_count; i++)
        {
            var pos = leftupvert + new Vector3(edge / 2, 0, 0) - new Vector3(0, edge / 2, 0);
            pos = pos + new Vector3(edge, 0, 0) * column - new Vector3(0, edge, 0) * row;

            if (i == _grid_count * _grid_count - 1)
                Add_Dot(i, pos);
            else
                Add_Dot(i, pos);

            column++;
            if (column != 0 && column % _grid_count == 0)
            {
                row++;
                column = 0;
            }
        }

        row = 0;
        column = 0;

        for (int i = 0; i < _grid_count * _grid_count - 1; i++)
        {
            obj = Instantiate(_cube, transform, false);
            obj.transform.localScale = scale;
            if (_material != null)
                obj.GetComponent<MeshRenderer>().sharedMaterial = _material;

            var b = obj.GetComponent<BoxCollider>();

            var dot = obj.transform.TransformPoint(new Vector3(
                -b.size.x,
                -b.size.y,
                b.size.z) * 0.5f);

            //var dot = transform.TransformPoint(obj.GetComponent<MeshFilter>().sharedMesh.vertices[7]);

            //dot = new Vector3(dot.x * obj.transform.localScale.x,
            //    dot.y * obj.transform.localScale.y,
            //    dot.z * obj.transform.localScale.z);
            //dot = new Vector3(obj.transform.position.x + dot.x,
            //    obj.transform.position.y + dot.y,
            //    obj.transform.position.z + dot.z);
            //dot = Quaternion.Euler(obj.transform.eulerAngles) * dot;

            obj.transform.position = leftupvert - (dot - obj.transform.position) + new Vector3(edge, 0,0) * column - new Vector3(0, edge, 0) * row;

            _objects.Add(obj);

            column++;
            if (column != 0 && column % _grid_count == 0)
            {
                row++;
                column = 0;
            }
        }
        Randomize();

        var sum = 0;
        for (int i = 0; i < _objects.Count; i++)
        {
            sum += MathExtension.GetSumNumber(_objects, _objects[i]);
        }
        if (!MathExtension.CorrectMagicBox(sum, _grid_count))
        {
            Cube cube_temp = new Cube(_objects[_objects.Count - 1].GetComponent<Cube>());

            _objects[_objects.Count - 1].GetComponent<Cube>().Refresh(_objects[_objects.Count - 2].GetComponent<Cube>());
            _objects[_objects.Count - 2].GetComponent<Cube>().Refresh(cube_temp);

            _objects[_objects.Count - 1].GetComponentInChildren<TextMeshPro>().text = _objects[_objects.Count - 1].GetComponent<Cube>()._Number.ToString();
            _objects[_objects.Count - 2].GetComponentInChildren<TextMeshPro>().text = _objects[_objects.Count - 2].GetComponent<Cube>()._Number.ToString();
        }
    }

    private int GetGridCount()
    {
        if (PlayerPrefs.HasKey("Grid_count"))
            return PlayerPrefs.GetInt("Grid_count");
        else
            return _grid_count;
    }

    private void Randomize()
    {
        System.Random rnd = new System.Random();

        List<int> ints = Enumerable.Range(1, _grid_count * _grid_count - 1).ToList();

        for (int i = 0; i < _objects.Count; i++)
        {
            var _num = rnd.Next(0, ints.Count);
            _objects[i].GetComponent<Cube>()._Id = ints[_num] - 1;
            _objects[i].GetComponent<Cube>()._Number = ints[_num];
            _objects[i].GetComponent<Cube>()._Scale = (float)(GetEdge() / _grid_count * Math.Sqrt(2) / 2);
            _objects[i].GetComponentInChildren<TextMeshPro>().text = ints[_num].ToString();
            ints.Remove(ints[_num]);
        }
    }

    private void Add_Dot(int idx, Vector3 position)
    {
        Dot dot = new Dot();
        dot.Id= idx;
        dot.position = position;
        Plane._dots.Add(dot);
    }

    private void Clear()
    {
        _objects.Clear();
        Plane._dots.Clear();
        var count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    private float GetEdge()
    {
        var right = transform.TransformPoint(GetComponent<MeshFilter>().sharedMesh.vertices[0]);
        var left = transform.TransformPoint(GetComponent<MeshFilter>().sharedMesh.vertices[10]);
        return Vector3.Distance(right, left);
    }
}
