using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	internal static ObjectPooler instance;
	private void Awake()
	{
		instance = this;
	}

	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject[] prefab;
		public int size;
	}

	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	private void Start()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab[Random.Range(0, pool.prefab.Length)]);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rot)
	{
		if (!poolDictionary.ContainsKey(tag))
			return null;
		GameObject objToSpawn = poolDictionary[tag].Dequeue();

		objToSpawn.SetActive(true);
		objToSpawn.transform.position = pos;
		objToSpawn.transform.rotation = rot;

		poolDictionary[tag].Enqueue(objToSpawn);
		return objToSpawn;
	}
}