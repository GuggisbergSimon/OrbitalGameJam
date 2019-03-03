using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnTrack : MonoBehaviour
{
	public GameObject leftLaneEnnemy;
	public GameObject rightLaneEnnemy;
	public String levelToLoad = "level1";
	[SerializeField] private float minX;
	[SerializeField] private float maxX;
	[SerializeField] private float SPAWN_Y = 10f;
	[SerializeField] private GameObject ennemyPrefab = null;

	private List<Tuple<float, GameObject>> ennemies;
	private List<Ennemy> ennemiesAlive = new List<Ennemy>();
	public List<Ennemy> EnnemiesAlive => ennemiesAlive;
	private float totalTimeMillis = 0;

	private void Start()
	{
		if (levelToLoad == "level1")
		{
			ennemies = createLevel1();
			ennemiesAlive.Add(leftLaneEnnemy.GetComponent<Ennemy>());
			ennemiesAlive.Add(rightLaneEnnemy.GetComponent<Ennemy>());
		}
		else
		{
			// Other potential levels
		}
	}

	private void Update()
	{
		totalTimeMillis += Time.deltaTime;
		bool loop = true;

		while (loop)
		{
			loop = false;
			if (ennemies.Count > 0)
			{
				Tuple<float, GameObject> pair = ennemies[0];
				if (pair.Item1 <= totalTimeMillis)
				{
					ennemiesAlive.Add(InstantiateEnnemy(pair.Item2));
					ennemies.RemoveAt(0);
					loop = true;
				}
			}
		}
	}

	public List<Tuple<float, GameObject>> createLevel1()
	{
		return new List<Tuple<float, GameObject>>
		{
			addElem(0, true), addElem(0, false),
			addElem(1, true), addElem(1, false),
			addElem(2, true), addElem(2, false),
			addElem(5, true), addElem(5, false),
		};
	}

	public Tuple<float, GameObject> addElem(float timeStamp, bool left)
	{
		if (left)
		{
			return new Tuple<float, GameObject>(timeStamp, leftLaneEnnemy);
		}
		else
		{
			return new Tuple<float, GameObject>(timeStamp, rightLaneEnnemy);
		}
	}

	// Instantiate an ennemy (with random X coord between minX and maxX) given a GameObject Prefab of ennemy
	public Ennemy InstantiateEnnemy(GameObject o)
	{
		GameObject instance = Instantiate(ennemyPrefab,
			o.transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX), SPAWN_Y, 0),
			new Quaternion(0, 0, 0, 0), transform);
		return instance.GetComponent<Ennemy>();
	}
}