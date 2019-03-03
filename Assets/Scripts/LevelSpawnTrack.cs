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
                        if (e.IsHitable()) e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.BottomRight);
                        if (e.IsHitable()) e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.BottomLeft);
                    }
                    if (horizontal > 0)
                    {
                        if (e.IsHitable()) e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.Right);
                    }
                    else if (horizontal < 0)
                    {
                        if (e.IsHitable()) e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.Left);
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
            addElem(5.14285714285714f,false,Ennemy.EnnemyType.RightOrLeft), addElem(6.85714285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(6.85714285714286f,false,Ennemy.EnnemyType.Bottom), addElem(8.57142857142857f,false,Ennemy.EnnemyType.RightOrLeft), addElem(10.2857142857143f,true,Ennemy.EnnemyType.Bottom) , addElem(10.2857142857143f,false,Ennemy.EnnemyType.Bottom), addElem(12f,false,Ennemy.EnnemyType.RightOrLeft), addElem(12.4285714285714f,false,Ennemy.EnnemyType.Top) , addElem(12.6428571428571f,true,Ennemy.EnnemyType.Top) , addElem(13.1785714285714f,false,Ennemy.EnnemyType.Top) , addElem(13.2857142857143f,true,Ennemy.EnnemyType.Top) , addElem(13.3928571428571f,false,Ennemy.EnnemyType.Top) , addElem(13.5f,true,Ennemy.EnnemyType.Top) , addElem(13.6071428571429f,false,Ennemy.EnnemyType.Top) , addElem(13.6607142857143f,true,Ennemy.EnnemyType.Top) , addElem(13.7142857142857f,true,Ennemy.EnnemyType.Bottom) , addElem(13.7142857142857f,false,Ennemy.EnnemyType.Bottom), addElem(15.4285714285714f,false,Ennemy.EnnemyType.RightOrLeft), addElem(17.1428571428571f,true,Ennemy.EnnemyType.Bottom) , addElem(17.1428571428571f,false,Ennemy.EnnemyType.Bottom), addElem(18.8571428571429f,false,Ennemy.EnnemyType.RightOrLeft), addElem(20.5714285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(20.5714285714286f,false,Ennemy.EnnemyType.Bottom), addElem(22.2857142857143f,false,Ennemy.EnnemyType.RightOrLeft), addElem(24f,true,Ennemy.EnnemyType.Bottom) , addElem(24f,false,Ennemy.EnnemyType.Bottom), addElem(25.7142857142857f,false,Ennemy.EnnemyType.RightOrLeft), addElem(26.5714285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(26.7857142857143f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(27.1071428571429f,true,Ennemy.EnnemyType.Bottom) , addElem(27.2142857142857f,false,Ennemy.EnnemyType.Bottom), addElem(27.3214285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(34.2857142857143f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(34.5f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(34.7142857142857f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(34.9285714285714f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(35.1428571428571f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(35.3571428571429f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(35.5714285714286f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(35.7857142857143f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(36f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(36.1071428571429f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(36.2142857142857f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(36.3214285714286f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(36.4285714285714f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(36.6428571428571f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(36.8571428571429f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(37.0714285714286f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(37.2857142857143f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(37.3928571428571f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(37.5f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(37.6071428571429f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(37.7142857142857f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(37.9285714285714f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(38.1428571428571f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(38.3571428571429f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(38.5714285714286f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(38.6785714285714f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(38.7857142857143f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(38.8928571428571f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(39f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(39.2142857142857f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(39.4285714285714f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(39.6428571428571f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(39.8571428571429f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(40.0714285714286f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(40.2857142857143f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(40.5f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(40.7142857142857f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(40.8214285714286f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(40.9285714285714f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(41.0357142857143f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(41.6785714285714f,true,Ennemy.EnnemyType.Bottom) , addElem(42.2142857142857f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(42.75f,false,Ennemy.EnnemyType.Bottom) , addElem(43.3928571428571f,false,Ennemy.EnnemyType.Top) , addElem(43.9285714285714f,true,Ennemy.EnnemyType.Top) , addElem(44.4642857142857f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(45.1071428571429f,true,Ennemy.EnnemyType.Bottom) , addElem(45.6428571428571f,true,Ennemy.EnnemyType.Top) , addElem(46.1785714285714f,false,Ennemy.EnnemyType.Bottom) , addElem(46.8214285714286f,false,Ennemy.EnnemyType.Bottom) , addElem(47.3571428571429f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(47.8928571428571f,true,Ennemy.EnnemyType.Bottom) , addElem(48.5357142857143f,true,Ennemy.EnnemyType.Top) , addElem(49.0714285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(49.6071428571429f,false,Ennemy.EnnemyType.Bottom) , addElem(50.25f,false,Ennemy.EnnemyType.RightOrLeft) , addElem(50.7857142857143f,true,Ennemy.EnnemyType.Top) , addElem(51.3214285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(51.9642857142857f,true,Ennemy.EnnemyType.Bottom) , addElem(52.5f,true,Ennemy.EnnemyType.RightOrLeft) , addElem(53.0357142857143f,false,Ennemy.EnnemyType.Top) , addElem(53.1428571428571f,true,Ennemy.EnnemyType.Bottom) , addElem(53.25f,false,Ennemy.EnnemyType.Bottom) , addElem(53.3571428571429f,true,Ennemy.EnnemyType.Bottom) , addElem(53.4642857142857f,false,Ennemy.EnnemyType.Bottom) , addElem(53.5714285714286f,true,Ennemy.EnnemyType.Bottom) , addElem(53.6785714285714f,false,Ennemy.EnnemyType.Bottom) , addElem(53.7857142857143f,true,Ennemy.EnnemyType.Bottom) , addElem(53.8928571428571f,false,Ennemy.EnnemyType.Bottom) , addElem(54f,true,Ennemy.EnnemyType.Bottom) , addElem(54.1071428571429f,false,Ennemy.EnnemyType.Bottom) , addElem(54.2142857142857f,true,Ennemy.EnnemyType.Bottom) , addElem(54.3214285714286f,false,Ennemy.EnnemyType.Bottom) , addElem(54.5357142857143f,false,Ennemy.EnnemyType.Bottom) , addElem(54.6428571428571f,true,Ennemy.EnnemyType.Bottom) , addElem(54.75f,false,Ennemy.EnnemyType.Bottom)

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