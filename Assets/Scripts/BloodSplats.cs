using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplats : MonoBehaviour
{
	[SerializeField] private int minAnimID = 1;
	[SerializeField] private int maxAnimID = 3;
	[SerializeField] private AudioClip[] splatsSounds = null;
	private bool isAlive = true;

	private void Start()
	{
		GetComponent<AudioSource>().PlayOneShot(splatsSounds[Random.Range(0, splatsSounds.Length)]);
		GetComponent<Animator>().SetTrigger(Mathf.RoundToInt(Random.Range(minAnimID, maxAnimID)).ToString());
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