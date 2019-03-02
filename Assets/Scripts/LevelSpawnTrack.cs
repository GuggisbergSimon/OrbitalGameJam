using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnTrack : MonoBehaviour
{
	public GameObject leftLaneEnnemy;
	public GameObject rightLaneEnnemy;
	public String levelToLoad = "level1";

	private List<Tuple<float, GameObject>> ennemies;
	private float totalTimeMillis = 0;

	private void Start()
	{
		if (levelToLoad == "level1")
		{
			ennemies = createLevel1();
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
		if (ennemies.Count > 0)
		{
			while (loop)
			{
				loop = false;
				Tuple<float, GameObject> pair = ennemies[0];
				if (pair.Item1 <= totalTimeMillis)
				{
					Ennemy.InstantiateEnnemy(pair.Item2);
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
}