using System.Collections.Generic;
using UnityEngine;

public class InitBlocks : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _go_blocks;

    public static List<Block> _blocks = new List<Block>();

    private void Awake()
    {
        _blocks.Clear();
        int idx = 1;
        foreach (var item in _go_blocks)
        {
            Block block = new Block();
            block._block = item;
            block._cell = idx;
            block._name = item.name;
            block._x = (int)item.transform.localPosition.x;
            block._z = (int)item.transform.localPosition.z;
            _blocks.Add(block);
        }
    }
}
