using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelInGivenTime : MonoBehaviour
{
	[SerializeField] private float time = 150.0f;
	[SerializeField] private string nameLevel = null;

	private void Start()
	{
		Invoke("LoadLevel", time);
	}

	private void LoadLevel()
	{
		GameManager.Instance.LoadLevel(nameLevel);
	}
}