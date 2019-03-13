using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float maxBlood = 10;
	[SerializeField] private int lives = 1;
	[SerializeField] private GameObject minionPrefab = null;
	[SerializeField] private GameObject startingPointMinions = null;
	[SerializeField] private float spaceBetweenMinions;
	[SerializeField] private SpriteRenderer bloodBath = null;
	[SerializeField] private int[] bloodLevels = null;
	[SerializeField] private Sprite[] bloodSprites = null;
	private float _totalBlood = 0;
	private float _currentBlood;
	private bool _isAlive = true;
	private List<Minion> _minionsList = new List<Minion>();
	private Animator _myAnimator;
	private AudioSource _myAudioSource;
	private float _previousHorizontalInput;
	private float _previousVerticalInput;
	private float _previousHorizontalLeftInput;
	private float _previousVerticalLeftInput;

	public float CurrentBlood => _currentBlood;
	public float MaxBlood => maxBlood;

	public void AddBlood(float bloodValue)
	{
		if (_isAlive)
		{
			_currentBlood += bloodValue;
			_totalBlood += bloodValue;


			if (_currentBlood > maxBlood)
			{
				_currentBlood -= maxBlood;
				lives++;
				_myAnimator.SetTrigger("Spawn");
				_myAudioSource.Play();
				AddMinion();
			}

			GameManager.Instance.UIManager.RefreshInterface();
			for (int i = 0; i < bloodLevels.Length; i++)
			{
				if (_totalBlood < bloodLevels[i])
				{
					break;
				}

				bloodBath.sprite = bloodSprites[i];
			}
		}
	}

	private void Start()
	{
		for (int i = 0; i < lives; i++)
		{
			AddMinion();
		}

		_myAnimator = GetComponent<Animator>();
		_myAudioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		float verticalInput = 0.0f;
		float vertical = Input.GetAxisRaw("Vertical");
		float horizontalInput = 0.0f;
		float horizontal = Input.GetAxisRaw("Horizontal");
		float verticalLeftInput = 0.0f;
		float verticalLeft = Input.GetAxisRaw("VerticalLeft");
		float horizontalLeftInput = 0.0f;
		float horizontalLeft = Input.GetAxisRaw("HorizontalLeft");

		if (_previousVerticalInput.CompareTo(vertical) != 0)
		{
			verticalInput = vertical;
		}
		else if (_previousHorizontalInput.CompareTo(horizontal) != 0)
		{
			horizontalInput = horizontal;
		}

		if (_previousVerticalLeftInput.CompareTo(verticalLeft) != 0)
		{
			verticalLeftInput = verticalLeft;
		}
		else if (_previousHorizontalLeftInput.CompareTo(horizontalLeft) != 0)
		{
			horizontalLeftInput = horizontalLeft;
		}


		_previousVerticalInput = vertical;
		_previousHorizontalInput = horizontal;
		_previousVerticalLeftInput = verticalLeft;
		_previousHorizontalLeftInput = horizontalLeft;

		if (verticalInput != 0 || horizontalInput != 0 || horizontalLeftInput != 0 || verticalLeftInput != 0)
		{
			foreach (Ennemy e in GameManager.Instance.spawner.EnnemiesAlive)
			{
				if (e.IsHitable())
				{
					if (verticalInput > 0)
					{
						e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.TopRight);
					}

					else if (verticalInput < 0)
					{
						e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.BottomRight);
					}

					if (verticalLeftInput > 0)
					{
						e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.TopLeft);
					}
					else if (verticalLeftInput < 0)
					{
						e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.BottomLeft);
					}

					if (horizontalInput > 0)
					{
						e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.Right);
					}

					if (horizontalLeftInput < 0)
					{
						e.OnHitByPlayer(DirectionTrigger.CardinalDirectionTrigger.Left);
					}
				}
			}
		}
	}

	private void AddMinion()
	{
		_minionsList.Add(Instantiate(minionPrefab,
			startingPointMinions.transform.position + Vector3.right * _minionsList.Count * spaceBetweenMinions,
			transform.rotation, transform).GetComponent<Minion>());
	}

	public void RemoveLife()
	{
		if (lives <= 0 && _isAlive)
		{
			_isAlive = false;
			_myAnimator.SetTrigger("Die");
		}
		else if (lives > 0)
		{
			lives--;
			_minionsList[_minionsList.Count - 1].Die();
			_minionsList.Remove(_minionsList[_minionsList.Count - 1]);
		}
	}

	public void GameOver()
	{
		GameManager.Instance.LoadLevel("GameOverScene");
	}
}