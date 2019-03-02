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

	[SerializeField] private int notInteractibleWithPlayerLayer = 0;
	public EnnemyType ennemyType;
	public bool isLeftLane = true;
	public float moveSpeed = 10;
	public float coefDeadVelocity = 1;
	public float coefDeadRotationVelocity = 1;
	public Vector3 spawnPosition;
	public float bloodAmount = 1;
	public float hitDamages = 1;
	public float minimalVelocityToDie = 10;

	private bool isDead = false;
	private Vector3 deadVelocity;
	private Vector3 deadRotationVelocity;
	private bool isInHitZone = false;
	private Rigidbody _myRigidbody;
	private static float SPEED_MODIFIER = -0.1f; //TODO adjust for right time with music

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("hitZone"))
		{
			isInHitZone = true;
			gameObject.layer = 0;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("hitZone"))
		{
			isInHitZone = false;
			OnHitPlayerTroops();
			gameObject.layer = notInteractibleWithPlayerLayer;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.transform.CompareTag("Player") && IsHitable(true))
		{
			if (!OnHitByPlayer(_myRigidbody.velocity))
			{
				_myRigidbody.velocity = Vector3.zero;
				_myRigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

	public bool OnHitByPlayer(Vector3 direction)
	{
		// Death animation (?)
		if (direction.magnitude < minimalVelocityToDie) return false;

		switch (ennemyType)
		{
			case EnnemyType.Top:
				isDead = direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
				break;
			case EnnemyType.Bottom:
				isDead = direction.y < 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x);
				break;
			case EnnemyType.RightOrLeft:
				if (isLeftLane)
				{
					isDead = direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
				}
				else
				{
					isDead = direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y);
				}

				break;
			default:
				return false;
		}

		if (isDead)
		{
			GameManager.Instance.Player.AddBlood(bloodAmount);
			// TODO: DEATH animation here
			Destroy(gameObject, 3);
			float angle = Vector3.Angle(new Vector3(direction.x, 0, 0), new Vector3(0, direction.y, 0));
			if ((direction.y > 0 && direction.x > 0 && direction.y > direction.x) ||
				(direction.y > 0 && direction.x < 0 && -direction.x > direction.y) ||
				(direction.y < 0 && direction.x < 0 && -direction.y > -direction.x) ||
				(direction.y < 0 && direction.x > 0 && direction.x > -direction.y))
			{
				angle = -angle;
			}

			deadRotationVelocity = new Vector3(0, 0, angle);
			deadVelocity = new Vector3(direction.x * coefDeadVelocity, direction.y * coefDeadVelocity, 0);
		}

		return !isDead;
	}

	void OnHitPlayerTroops()
	{
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
	public bool IsHitable(bool isLeftHand)
	{
		return isInHitZone && !isDead && (isLeftHand && isLeftLane);
	}

	// Start is called before the first frame update
	void Start()
	{
		_myRigidbody = GetComponent<Rigidbody>();
		transform.eulerAngles = Vector3.zero;
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