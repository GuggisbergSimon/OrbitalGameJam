using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
	private Animator _myAnimator;
	
	private void Start()
	{
		_myAnimator = GetComponent<Animator>();
		//_myAnimator.SetTrigger("Spawn");
	}

	public void Die()
	{
		//_myAnimator.SetTrigger("Die");
	}

	//todo call this method from animation after end of Die
	public void DestroyThis()
	{
		Destroy(gameObject);
	}
}
