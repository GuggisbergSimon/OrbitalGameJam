using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplats : MonoBehaviour
{
	[SerializeField] private int minAnimID = 0;
	[SerializeField] private int maxAnimID = 3;
	private bool isAlive = true;

	private void Start()
	{
		GetComponent<Animator>().SetTrigger(Mathf.RoundToInt(Random.Range(minAnimID, maxAnimID)));
	}

	public void Destroy()
	{
		if (isAlive)
		{
			isAlive = false;
			Destroy(gameObject);
		}
	}
}