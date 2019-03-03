using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DirectionTrigger : MonoBehaviour
{
	[SerializeField] private CardinalDirectionTrigger directionTrigger;
	[SerializeField] private LevelSpawnTrack spawner = null;

	private CinemachineVirtualCamera _globalCam;
	private CinemachineBasicMultiChannelPerlin _noise;

	public enum CardinalDirectionTrigger
	{
		TopRight,
		TopLeft,
		BottomRight,
		BottomLeft,
		Right,
		Left
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			foreach (var ennemy in spawner.EnnemiesAlive)
			{
				if (ennemy.IsHitable())
				{
					ennemy.OnHitByPlayer(directionTrigger);
				}
			}
		}
	}
}