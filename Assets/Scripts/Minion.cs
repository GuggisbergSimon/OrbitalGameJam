using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
	private Animator _myAnimator;
	
	private void Start()
	{
		_myAnimator = GetComponent<Animator>();
	}

	public void Die()
	{
		_myAnimator.SetTrigger("Die");
	}

	public void DestroyThis()
	{
		Destroy(gameObject);
	}
}
