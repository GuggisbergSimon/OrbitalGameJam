using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
	[SerializeField] private AudioClip[] breakingNoises = null;
	private Animator _myAnimator;
	private AudioSource _myAudioSource;

	private void Start()
	{
		_myAnimator = GetComponent<Animator>();
		_myAudioSource = GetComponent<AudioSource>();
	}

	public void Die()
	{
		_myAnimator.SetTrigger("Die");
		_myAudioSource.PlayOneShot(breakingNoises[Random.Range(0, breakingNoises.Length)]);
	}

	public void DestroyThis()
	{
		Destroy(gameObject);
	}
}