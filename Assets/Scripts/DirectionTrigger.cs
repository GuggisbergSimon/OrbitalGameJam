using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DirectionTrigger : MonoBehaviour
{
	[SerializeField] private CardinalDirectionTrigger directionTrigger;
	[SerializeField] private LevelSpawnTrack spawner = null;
    //[SerializeField] private HandsBehaviour handLeft = null;
    //[SerializeField] private HandsBehaviour handRight = null;

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

        if (other.CompareTag("Player") && other.gameObject.GetComponent<HandsBehaviour>() != null)
        {
            Debug.Log(other.name);
            Vector3 speed = other.gameObject.GetComponent<HandsBehaviour>().currentSpeed;
            Debug.Log("Speed " + speed + " type " + directionTrigger+" magn "+speed.magnitude);
            if (speed.magnitude > 0.5)
            {
                bool correct = false;
                switch (directionTrigger)
                {
                    case DirectionTrigger.CardinalDirectionTrigger.TopRight:
                    case DirectionTrigger.CardinalDirectionTrigger.TopLeft:
                        correct = speed.y > Mathf.Abs(speed.x);
                        break;
                    case DirectionTrigger.CardinalDirectionTrigger.BottomRight:
                    case DirectionTrigger.CardinalDirectionTrigger.BottomLeft:
                        correct = speed.y < 0 && Mathf.Abs(speed.y) > Mathf.Abs(speed.x);
                        break;
                    case DirectionTrigger.CardinalDirectionTrigger.Right:
                        correct = speed.x > Mathf.Abs(speed.y);
                        break;
                    case DirectionTrigger.CardinalDirectionTrigger.Left:
                        correct = -speed.x > Mathf.Abs(speed.y);
                        break;
                }
                if (correct)
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
    }
}