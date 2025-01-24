using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
	public static Pool instances;

	[SerializeField] private Transform poolParent;
	[SerializeField] private List<Prefab> prefabList = new List<Prefab>();
	private Dictionary<string, Queue<GameObject>> prefabDictionary = new Dictionary<string, Queue<GameObject>>();

	private void Awake()
	{
		instances = this;

		foreach(Prefab p in prefabList)
		{
			Queue<GameObject> ng = new Queue<GameObject>();
			for (int i = 0; i < p.count; i++)
			{
				GameObject np = Instantiate(p.obj);

				np.name = p.tag.ToLower();
				np.transform.SetParent(poolParent);
				np.SetActive(false);

				ng.Enqueue(np);
			}
			prefabDictionary.Add(p.tag.ToLower(), ng);
		}
	}

	public GameObject Create(string tag, Vector3 pos, Quaternion rot, Transform parent = null)
	{
		if (!prefabDictionary.ContainsKey(tag.ToLower()))
			return null;

		GameObject cp = prefabDictionary[tag.ToLower()].Dequeue();
		cp.transform.position = pos;
		cp.transform.rotation = rot;

		if (parent != null)
			cp.transform.SetParent(parent);

		cp.SetActive(true);

		return cp;
	}

	public void Destroy(GameObject g)
	{
		if (!prefabDictionary.ContainsKey(g.name.ToLower()))
			return;

		g.transform.SetParent(poolParent);
		g.SetActive(false);
		prefabDictionary[g.name.ToLower()].Enqueue(g);
	}
}

[System.Serializable]
public class Prefab
{
	public string tag;
	public GameObject obj;
	public int count;
}
