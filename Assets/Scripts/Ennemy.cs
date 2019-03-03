using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
	public enum EnnemyType
	{
		Top,
		Bottom,
		RightOrLeft
	}

	public EnnemyType ennemyType;
	public bool isLeftLane = true;
	public float moveSpeed = 10;
	public float coefDeadVelocity = 1;
	public float coefDeadRotationVelocity = 1;
	public Vector3 spawnPosition;
	public float bloodAmount = 1;
	public float hitDamages = 1;

	private bool isDead = false;
	private Vector3 deadVelocity;
	private Vector3 deadRotationVelocity;
	private bool isInHitZone = false;
	private Animator _myAnimator;
	private static float SPEED_MODIFIER = -0.1f; //TODO adjust for right time with music

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("hitZone"))
		{
			isInHitZone = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("hitZone") && !isDead)
		{
			isInHitZone = false;
			OnHitPlayerTroops();
		}
	}

	public bool OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger directionTrigger)
	{
		// Death animation (?)
		switch (ennemyType)
		{
			case EnnemyType.Top:
			{
				isDead = directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopRight && !isLeftLane ||
						 directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopLeft && isLeftLane;
				break;
			}
			case EnnemyType.Bottom:
			{
				isDead = directionTrigger == DirectionTrigger.CardinalDirectionTrigger.BottomRight && !isLeftLane ||
						 directionTrigger == DirectionTrigger.CardinalDirectionTrigger.BottomLeft && isLeftLane;
				break;
			}

			case EnnemyType.RightOrLeft:
			{
				if (isLeftLane)
				{
					isDead = directionTrigger == DirectionTrigger.CardinalDirectionTrigger.Left;
				}
				else
				{
					isDead = directionTrigger == DirectionTrigger.CardinalDirectionTrigger.Right;
				}

				break;
			}
			default:
				return false;
		}

		if (isDead)
		{
			GameManager.Instance.Player.AddBlood(bloodAmount);
			// TODO: DEATH animation here
			Destroy(gameObject, 3);
			Vector2 direction =
				(directionTrigger == DirectionTrigger.CardinalDirectionTrigger.Right ? Vector2.right : Vector2.left) +
				(directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopLeft ||
				 directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopRight
					? Vector2.up
					: Vector2.down);
			;
			deadVelocity = new Vector3(direction.x * coefDeadVelocity, direction.y * coefDeadVelocity, 0);
		}

		return !isDead;
	}

	void OnHitPlayerTroops()
	{
		Debug.Log("Attack");
		//todo add attack animation
		GameManager.Instance.Player.RemoveLife();
		isDead = true;
		Destroy(gameObject, 3);
	}

	/// <summary>
	/// Return true if the ennemy is hitable (in the hitzone, not dead yet and the right lane where the hit comes from)
	/// </summary>
	/// <param name="isLeftHand">True if the hit comes from the left lane</param>
	/// <returns></returns>
	public bool IsHitable()
	{
		return isInHitZone && !isDead;
	}

	// Start is called before the first frame update
	void Start()
	{
		transform.eulerAngles = Vector3.zero;
		_myAnimator = GetComponent<Animator>();
		if (ennemyType == EnnemyType.Top)
		{
			_myAnimator.SetTrigger("1");
		}
		else if (ennemyType == EnnemyType.Bottom)
		{
			_myAnimator.SetTrigger("2");
		}else
		{
			_myAnimator.SetTrigger("3");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDead)
		{
			transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime * SPEED_MODIFIER));
		}
		else
		{
			transform.Translate(deadVelocity * Time.fixedDeltaTime);
			transform.Rotate(deadRotationVelocity * Time.fixedDeltaTime);
		}
	}
}