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

	public GameObject[] leftLaneEnnemies; //ORDER is: Top, Bottom, RightOrLeft
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
		if (vertical != 0 || horizontal != 0)
		{
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
			addElem(5.14285714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(6.85714285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(6.85714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(8.57142857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(10.2857142857143f, true, Ennemy.EnnemyType.Bottom),
			addElem(10.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(12f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(12.4285714285714f, false, Ennemy.EnnemyType.Top),
			addElem(12.6428571428571f, true, Ennemy.EnnemyType.Top),
			addElem(13.1785714285714f, false, Ennemy.EnnemyType.Top),
			addElem(13.2857142857143f, true, Ennemy.EnnemyType.Top),
			addElem(13.3928571428571f, false, Ennemy.EnnemyType.Top), addElem(13.5f, true, Ennemy.EnnemyType.Top),
			addElem(13.6071428571429f, false, Ennemy.EnnemyType.Top),
			addElem(13.6607142857143f, true, Ennemy.EnnemyType.Top),
			addElem(13.7142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(13.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(15.4285714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(17.1428571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(17.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(18.8571428571429f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(20.5714285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(20.5714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(22.2857142857143f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(24f, true, Ennemy.EnnemyType.Bottom), addElem(24f, false, Ennemy.EnnemyType.Bottom),
			addElem(25.7142857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(26.5714285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(26.7857142857143f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(27.1071428571429f, true, Ennemy.EnnemyType.Bottom),
			addElem(27.3214285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(34.2857142857143f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(34.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(34.7142857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(34.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(35.1428571428571f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(35.3571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(35.5714285714286f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(35.7857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(36f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(36.2142857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(36.4285714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(36.6428571428571f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(36.8571428571429f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(37.0714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(37.2857142857143f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(37.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(37.7142857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(37.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(38.1428571428571f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(38.3571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(38.5714285714286f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(38.7857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(39f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(39.2142857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(39.4285714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(39.6428571428571f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(39.8571428571429f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(40.0714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(40.2857142857143f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(40.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(40.7142857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(40.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(41.6785714285714f, true, Ennemy.EnnemyType.Bottom),
			addElem(42.2142857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(42.75f, false, Ennemy.EnnemyType.Bottom), addElem(43.3928571428571f, false, Ennemy.EnnemyType.Top),
			addElem(43.9285714285714f, true, Ennemy.EnnemyType.Top),
			addElem(44.4642857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(45.1071428571429f, true, Ennemy.EnnemyType.Bottom),
			addElem(45.6428571428571f, true, Ennemy.EnnemyType.Top),
			addElem(46.1785714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(46.8214285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(47.3571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(47.8928571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(48.5357142857143f, true, Ennemy.EnnemyType.Top),
			addElem(49.0714285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(49.6071428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(50.25f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(50.7857142857143f, true, Ennemy.EnnemyType.Top),
			addElem(51.3214285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(51.9642857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(52.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(53.0357142857143f, false, Ennemy.EnnemyType.Top),
			addElem(53.1428571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(53.3571428571429f, false, Ennemy.EnnemyType.Top),
			addElem(53.5714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(53.7857142857143f, true, Ennemy.EnnemyType.Top), addElem(54f, true, Ennemy.EnnemyType.Bottom),
			addElem(54.2142857142857f, false, Ennemy.EnnemyType.Top),
			addElem(54.5357142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(54.6428571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(54.75f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(54.8571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(55.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(55.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(56.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(56.5714285714286f, true, Ennemy.EnnemyType.Bottom), addElem(57f, false, Ennemy.EnnemyType.Bottom),
			addElem(57.6428571428571f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(57.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(58.2857142857143f, true, Ennemy.EnnemyType.Bottom),
			addElem(58.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(59.3571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(59.5714285714286f, false, Ennemy.EnnemyType.Bottom), addElem(60f, true, Ennemy.EnnemyType.Bottom),
			addElem(60.4285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(60.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(61.0714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(61.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(61.7142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(62.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(62.7857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(63f, false, Ennemy.EnnemyType.Bottom), addElem(63.4285714285714f, true, Ennemy.EnnemyType.Bottom),
			addElem(63.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(64.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(64.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(65.1428571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(65.5714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(66.2142857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(66.4821428571429f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(66.5892857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(66.8571428571429f, true, Ennemy.EnnemyType.Bottom),
			addElem(67.0714285714286f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(67.2857142857143f, true, Ennemy.EnnemyType.Top),
			addElem(67.2857142857143f, false, Ennemy.EnnemyType.Bottom), addElem(67.5f, true, Ennemy.EnnemyType.Top),
			addElem(67.7142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(67.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(68.0357142857143f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(68.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(68.25f, true, Ennemy.EnnemyType.Bottom), addElem(68.3571428571429f, true, Ennemy.EnnemyType.Top),
			addElem(68.4642857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(68.5714285714286f, true, Ennemy.EnnemyType.Top), addElem(69f, false, Ennemy.EnnemyType.Bottom),
			addElem(69.6428571428572f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(69.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(70.2857142857143f, true, Ennemy.EnnemyType.Bottom),
			addElem(70.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(71.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(71.3571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(71.5714285714286f, false, Ennemy.EnnemyType.Bottom), addElem(72f, true, Ennemy.EnnemyType.Bottom),
			addElem(72.4285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(73.0714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(73.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(73.7142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(74.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(74.7857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(75f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(75.3214285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(75.4285714285714f, true, Ennemy.EnnemyType.Bottom),
			addElem(75.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(76.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(76.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(77.1428571428572f, true, Ennemy.EnnemyType.Bottom),
			addElem(77.5714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(78f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(78.2142857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(78.4285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(78.8571428571429f, true, Ennemy.EnnemyType.Bottom),
			addElem(78.8571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(79.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(79.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(80.1428571428571f, false, Ennemy.EnnemyType.Bottom), addElem(81f, false, Ennemy.EnnemyType.Bottom),
			addElem(81.4285714285714f, false, Ennemy.EnnemyType.Top),
			addElem(81.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(82.0714285714286f, false, Ennemy.EnnemyType.Top),
			addElem(82.1785714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(82.2857142857143f, true, Ennemy.EnnemyType.Bottom),
			addElem(82.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(83.3571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(83.5714285714286f, false, Ennemy.EnnemyType.Bottom), addElem(84f, true, Ennemy.EnnemyType.Bottom),
			addElem(84.4285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(85.0714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(85.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(85.7142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(86.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(86.7857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(87f, false, Ennemy.EnnemyType.Bottom), addElem(87.4285714285714f, true, Ennemy.EnnemyType.Bottom),
			addElem(87.8571428571428f, false, Ennemy.EnnemyType.Bottom),
			addElem(88.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(88.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(89.1428571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(89.5714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(90.2142857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(90.4285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(90.8571428571429f, true, Ennemy.EnnemyType.Bottom),
			addElem(91.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(91.9285714285714f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(92.1428571428572f, false, Ennemy.EnnemyType.Bottom),
			addElem(92.5714285714286f, true, Ennemy.EnnemyType.Bottom), addElem(93f, false, Ennemy.EnnemyType.Bottom),
			addElem(93.6428571428571f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(93.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(94.2857142857143f, true, Ennemy.EnnemyType.Bottom),
			addElem(94.7142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(95.3571428571428f, true, Ennemy.EnnemyType.Top),
			addElem(95.5714285714286f, false, Ennemy.EnnemyType.Bottom), addElem(96f, true, Ennemy.EnnemyType.Bottom),
			addElem(96.4285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(97.0714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(97.2857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(97.7142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(98.1428571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(98.7857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(99f, false, Ennemy.EnnemyType.Bottom), addElem(99.4285714285714f, true, Ennemy.EnnemyType.Bottom),
			addElem(99.8571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(100.5f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(100.714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(101.142857142857f, false, Ennemy.EnnemyType.Bottom),
			addElem(101.464285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(101.785714285714f, false, Ennemy.EnnemyType.Top), addElem(102f, true, Ennemy.EnnemyType.Top),
			addElem(102.321428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(102.642857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(102.857142857143f, false, Ennemy.EnnemyType.Top),
			addElem(103.178571428571f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(103.714285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(104.25f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(104.571428571429f, false, Ennemy.EnnemyType.Top),
			addElem(104.785714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(105f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(105.214285714286f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(106.714285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(106.821428571429f, false, Ennemy.EnnemyType.Top),
			addElem(106.928571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(108f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(108.428571428571f, false, Ennemy.EnnemyType.Top),
			addElem(108.535714285714f, true, Ennemy.EnnemyType.Top),
			addElem(109.714285714286f, true, Ennemy.EnnemyType.Top),
			addElem(109.928571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(110.142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(110.357142857143f, false, Ennemy.EnnemyType.Top),
			addElem(110.571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(110.785714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(111f, true, Ennemy.EnnemyType.Bottom), addElem(111.214285714286f, false, Ennemy.EnnemyType.Bottom),
			addElem(111.428571428571f, true, Ennemy.EnnemyType.Top), addElem(111.75f, false, Ennemy.EnnemyType.Top),
			addElem(112.071428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(112.285714285714f, false, Ennemy.EnnemyType.Bottom),
			addElem(112.5f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(112.714285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(112.928571428571f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(113.142857142857f, false, Ennemy.EnnemyType.Top),
			addElem(113.142857142857f, true, Ennemy.EnnemyType.Top),
			addElem(116.571428571429f, true, Ennemy.EnnemyType.Bottom), addElem(117f, false, Ennemy.EnnemyType.Top),
			addElem(117.642857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(117.857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(118.607142857143f, true, Ennemy.EnnemyType.Bottom),
			addElem(118.928571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(119.464285714286f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(119.785714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(120.321428571429f, true, Ennemy.EnnemyType.Top),
			addElem(120.642857142857f, false, Ennemy.EnnemyType.Top),
			addElem(121.714285714286f, true, Ennemy.EnnemyType.Top),
			addElem(122.142857142857f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(122.357142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(123.428571428571f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(123.857142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(124.285714285714f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(124.714285714286f, true, Ennemy.EnnemyType.Top),
			addElem(124.928571428571f, false, Ennemy.EnnemyType.Top),
			addElem(125.142857142857f, true, Ennemy.EnnemyType.Bottom),
			addElem(125.303571428571f, false, Ennemy.EnnemyType.Bottom),
			addElem(125.571428571429f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(125.571428571429f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(126.107142857143f, false, Ennemy.EnnemyType.Top),
			addElem(126.214285714286f, true, Ennemy.EnnemyType.Top),
			addElem(126.321428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(126.428571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(126.535714285714f, false, Ennemy.EnnemyType.Top),
			addElem(126.642857142857f, true, Ennemy.EnnemyType.Top),
			addElem(126.75f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(126.75f, true, Ennemy.EnnemyType.RightOrLeft), addElem(127.5f, true, Ennemy.EnnemyType.Top),
			addElem(127.714285714286f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(127.928571428571f, true, Ennemy.EnnemyType.Bottom),
			addElem(128.142857142857f, false, Ennemy.EnnemyType.Top),
			addElem(128.357142857143f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(128.571428571429f, false, Ennemy.EnnemyType.Bottom),
			addElem(128.785714285714f, true, Ennemy.EnnemyType.Top),
			addElem(129f, false, Ennemy.EnnemyType.RightOrLeft),
			addElem(129.214285714286f, true, Ennemy.EnnemyType.Bottom),
			addElem(129.428571428571f, false, Ennemy.EnnemyType.Top),
			addElem(129.642857142857f, true, Ennemy.EnnemyType.RightOrLeft),
			addElem(129.857142857143f, false, Ennemy.EnnemyType.Bottom),
			addElem(129.857142857143f, true, Ennemy.EnnemyType.Bottom)
		};
	}

	public Tuple<float, GameObject> addElem(float timeStamp, bool left, Ennemy.EnnemyType type)
	{
		if (left)
		{
			return new Tuple<float, GameObject>(timeStamp - TIME_OFFSET_SPAWN, leftLaneEnnemies[(int) type]);
		}
		else
		{
			return new Tuple<float, GameObject>(timeStamp - TIME_OFFSET_SPAWN, rightLaneEnnemies[(int) type]);
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