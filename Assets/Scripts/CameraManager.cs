using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	private CinemachineBasicMultiChannelPerlin _noise;
	private CinemachineVirtualCamera _vCam;
	private Coroutine _shakingCoroutine;

	private void Start()
	{
		_vCam = FindObjectOfType<CinemachineVirtualCamera>();
		_noise = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
	}

	public void Shake(float amplitudeGain, float frequencyGain, float time)
	{
		if (_shakingCoroutine != null)
		{
			StopCoroutine(_shakingCoroutine);
		}

		_shakingCoroutine = StartCoroutine(Shaking(amplitudeGain, frequencyGain, time));
	}

	private IEnumerator Shaking(float amplitudeGain, float frequencyGain, float time)
	{
		Noise(amplitudeGain, frequencyGain);
		yield return new WaitForSeconds(time);
		Noise(0.0f, 0.0f);
	}

	private void Noise(float amplitudeGain, float frequencyGain)
	{
		_noise.m_AmplitudeGain = amplitudeGain;
		_noise.m_FrequencyGain = frequencyGain;
	}
}
