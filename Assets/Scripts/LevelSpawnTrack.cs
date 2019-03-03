using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnTrack : MonoBehaviour
{
    private static float minX = -0.025f;
    private static float maxX = 0.025f;
    private static float SPAWN_Y = 0.3f;
    private static float SPAWN_X_LEFT_LANE = -0.06f;
    private static float SPAWN_X_RIGHT_LANE = 0.16f;
    private static float TIME_OFFSET_SPAWN = 3.63f;

    public GameObject[] leftLaneEnnemies;//ORDER is: Top, Bottom, RightOrLeft
    public GameObject[] rightLaneEnnemies;
	public String levelToLoad = "level1";
	
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
			//ennemiesAlive.Add(leftLaneEnnemies[0].GetComponent<Ennemy>());
			//ennemiesAlive.Add(rightLaneEnnemies[0].GetComponent<Ennemy>());
		}
		else
		{
			// Other potential levels
		}
	}

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (vertical != 0 || horizontal != 0) { 
            foreach (Ennemy e in ennemiesAlive)
            {
                if (e.IsHitable())
                {
                    if (vertical > 0)
                    {
                        e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.TopLeft);
                        if (e.IsHitable()) e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.TopRight);
                    }
                    else if (vertical < 0)
                    {

                    }
                    if (horizontal > 0)
                    {

                    }
                    else if (horizontal < 0)
                    {

                    }
                }
            }
        }
        

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
			addElem(4, true, Ennemy.EnnemyType.Top), addElem(4, false, Ennemy.EnnemyType.Top),
			addElem(5, true, Ennemy.EnnemyType.Top), addElem(5, false, Ennemy.EnnemyType.Top),
			addElem(6, true, Ennemy.EnnemyType.Top), addElem(6, false, Ennemy.EnnemyType.Top),
            addElem(9, true, Ennemy.EnnemyType.Top), addElem(9, false, Ennemy.EnnemyType.Top),
            addElem(13, true, Ennemy.EnnemyType.Top), addElem(13, false, Ennemy.EnnemyType.Top),
            addElem(15, true, Ennemy.EnnemyType.Top), addElem(15, false, Ennemy.EnnemyType.Top),
            addElem(25, true, Ennemy.EnnemyType.Top), addElem(25, false, Ennemy.EnnemyType.Top),

        };
	}

	public Tuple<float, GameObject> addElem(float timeStamp, bool left, Ennemy.EnnemyType type)
	{
		if (left)
		{
			return new Tuple<float, GameObject>(timeStamp - TIME_OFFSET_SPAWN, leftLaneEnnemies[(int)type]);
		}
		else
		{
			return new Tuple<float, GameObject>(timeStamp - TIME_OFFSET_SPAWN, rightLaneEnnemies[(int)type]);
		}
	}

	// Instantiate an ennemy (with random X coord between minX and maxX) given a GameObject Prefab of ennemy
	public Ennemy InstantiateEnnemy(GameObject o)
	{
        bool isLeftLane = o.GetComponent<Ennemy>().isLeftLane;
        float x = isLeftLane ? SPAWN_X_LEFT_LANE : SPAWN_X_RIGHT_LANE;
        GameObject instance = Instantiate(o,
            new Vector3(UnityEngine.Random.Range(minX, maxX) + x, SPAWN_Y, 0),
			new Quaternion(0, 0, 0, 0), transform);
		return instance.GetComponent<Ennemy>();
	}
}