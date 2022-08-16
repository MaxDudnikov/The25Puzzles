using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void FixedUpdate()
    {
        ObjectPooler.instance.SpawnFromPool("Cube", transform.position, Quaternion.identity);
    }
}
