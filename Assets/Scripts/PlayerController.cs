using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bloodBath1;
    [SerializeField] private GameObject bloodBath2;
    [SerializeField] private GameObject bloodBath3;
    [SerializeField] private GameObject bloodBath4;
    [SerializeField] private float maxBlood = 10;
	[SerializeField] private int lives = 1;
	[SerializeField] private GameObject minionPrefab = null;
	[SerializeField] private GameObject startingPointMinions = null;
	[SerializeField] private float spaceBetweenMinions;
    private float _totalBlood = 0;
    private float _currentBlood;
	private bool _isAlive;
	private List<Minion> _minionsList = new List<Minion>();
    private GameObject _parentBloodBath;

	public float CurrentBlood => _currentBlood;
	public float MaxBlood => maxBlood;


	public void AddBlood(float bloodValue)
	{
		_currentBlood += bloodValue;
        _totalBlood += bloodValue;
        ChangeBloodBath();

        if (_currentBlood > maxBlood)
		{
			_currentBlood -= maxBlood;
			lives++;
			AddMinion();
		}

		GameManager.Instance.UIManager.RefreshInterface();
	}

    private void ChangeBloodBath()
    {
        if(_totalBlood == 1)
        {
            bloodBath1 = Instantiate(bloodBath1, _parentBloodBath.transform);
        }else if(_totalBlood == 2)
        {
            Destroy(bloodBath1);
            bloodBath2 = Instantiate(bloodBath2, _parentBloodBath.transform);
        }
        else if (_totalBlood == 3)
        {
            Destroy(bloodBath2);
            bloodBath3 = Instantiate(bloodBath3, _parentBloodBath.transform);
        }
        else if (_totalBlood == 4)
        {
            Destroy(bloodBath3);
            bloodBath4 = Instantiate(bloodBath4, _parentBloodBath.transform);
        }
    }

    /* TO visually test what gauge looks like
     * private float time = 0;
    private void Update()
    {
        time += Time.deltaTime;
        if(time > 2)
        {
            time = 0;
            AddBlood(1);
        }
    }*/

    private void Start()
	{
        _parentBloodBath = GameObject.FindGameObjectWithTag("hitZone");
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