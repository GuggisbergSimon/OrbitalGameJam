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
	private float _currentBlood;
	private bool _isAlive;
	private List<Minion> _minionsList = new List<Minion>();

	public float CurrentBlood => _currentBlood;
	public float MaxBlood => maxBlood;

	public void AddBlood(float bloodValue)
	{
		_currentBlood += bloodValue;
		if (_currentBlood > maxBlood)
		{
			_currentBlood -= maxBlood;
			lives++;
			AddMinion();
		}

		GameManager.Instance.UIManager.RefreshInterface();
	}

	private void Start()
	{
		for (int i = 0; i < lives; i++)
		{
			AddMinion();
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
			//TODO implement death of player here
		}
		else if (lives > 0)
		{
			lives--;
			_minionsList[_minionsList.Count - 1].Die();
			_minionsList.Remove(_minionsList[_minionsList.Count - 1]);
		}
	}
}