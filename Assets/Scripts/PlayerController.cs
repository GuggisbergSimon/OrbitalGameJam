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

	public float CurrentBlood => _currentBlood;
	public float MaxBlood => maxBlood;

	public void AddBlood(float bloodValue)
	{
		_currentBlood += bloodValue;
		_totalBlood += bloodValue;


		if (_currentBlood > maxBlood)
		{
			_currentBlood -= maxBlood;
			lives++;
			_myAnimator.SetTrigger("Spawn");
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

	private void Start()
	{
		for (int i = 0; i < lives; i++)
		{
			AddMinion();
		}

		_myAnimator = GetComponent<Animator>();
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
			Debug.Log("I'm so dead xd");
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
		GameManager.Instance.LoadLevel("GameOver");
	}
}