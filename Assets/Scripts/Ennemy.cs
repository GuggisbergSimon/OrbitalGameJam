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

	[SerializeField] private float amplitudeScreenshake = 0.2f;
	[SerializeField] private float frequencyScreenshake = 0.2f;
	[SerializeField] private float timeScreenshake = 0.2f;
	[SerializeField] private GameObject bloodSplatsPrefab = null;
	public EnnemyType ennemyType;
	public bool isLeftLane = true;
	public float moveSpeed = 10;
	public Vector3 spawnPosition;
	public float bloodAmount = 1;
	public float hitDamages = 1;

	private float coefDeadRotationVelocity = 1000;
	private float coefDeadVelocity = 1f;
	private bool isDead = false;
	private Vector3 deadVelocity;
	private Vector3 deadRotationVelocity;
	private bool isInHitZone = false;
	private Animator _myAnimator;
	private static float SPEED_MODIFIER = -0.1f; //TODO adjust for right time with music
	private static float DIRECTION_MODIFIER = 0.2f;

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
			Instantiate(bloodSplatsPrefab, transform.position, transform.rotation);
			GameManager.Instance.CameraManager.Shake(amplitudeScreenshake, frequencyScreenshake,
				timeScreenshake);
			Destroy(gameObject, 3);
			Vector2 direction =
				(directionTrigger == DirectionTrigger.CardinalDirectionTrigger.Right
					? Vector2.right
					: (directionTrigger == DirectionTrigger.CardinalDirectionTrigger.Right
						? Vector2.left
						: new Vector2(0, 0))) +
				(directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopLeft ||
				 directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopRight
					? Vector2.up
					: (directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopLeft ||
					   directionTrigger == DirectionTrigger.CardinalDirectionTrigger.TopRight
						? Vector2.down
						: new Vector2(0, 0)));
			;
			deadVelocity =
				new Vector3((direction.x + Random.Range(-DIRECTION_MODIFIER, DIRECTION_MODIFIER)) * coefDeadVelocity,
					(direction.y + Random.Range(-DIRECTION_MODIFIER, DIRECTION_MODIFIER)) * coefDeadVelocity, 0);
			deadRotationVelocity = new Vector3(0, 0, (Random.Range(-1, 1) > 0 ? 1 : -1) * coefDeadRotationVelocity);
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
		}
		else
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
			transform.Translate(deadVelocity * Time.fixedDeltaTime, Space.World);
			transform.Rotate(deadRotationVelocity * Time.fixedDeltaTime, Space.Self);
		}
	}
}