using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private float fadingToBlackTime = 0.5f;
	[SerializeField] private Image blackPanel = null;
	[SerializeField] private Image bloodGauge = null;
	[SerializeField] private float refreshingGaugeTime = 0.5f;
	private Coroutine _refreshingGaugeCoroutine;
	private bool _isFadingToBlack;
	public bool IsFadingToBlack => _isFadingToBlack;

	public void RefreshInterface()
	{
		if (_refreshingGaugeCoroutine != null)
		{
			StopCoroutine(_refreshingGaugeCoroutine);
		}

		_refreshingGaugeCoroutine =
			StartCoroutine(RefreshingGauge(GameManager.Instance.Player.CurrentBlood /
										   GameManager.Instance.Player.MaxBlood));
	}

	private IEnumerator RefreshingGauge(float fillAmount)
	{
		float timer = 0.0f;
		float initAmount = bloodGauge.fillAmount;
		while (timer < refreshingGaugeTime)
		{
			timer += Time.deltaTime;
			bloodGauge.fillAmount = Mathf.Lerp(initAmount, fillAmount, timer / refreshingGaugeTime);
			yield return null;
		}
	}

	public void FadeToBlack(bool value)
	{
		StartCoroutine(FadingToBlack(value, fadingToBlackTime));
	}

	private IEnumerator FadingToBlack(bool value, float time)
	{
		_isFadingToBlack = true;
		blackPanel.gameObject.SetActive(true);
		float timer = 0.0f;
		Color tempColor = blackPanel.color;
		while (timer < time)
		{
			timer += Time.unscaledDeltaTime;
			tempColor.a = Mathf.Lerp(value ? 0.0f : 1.0f, value ? 1.0f : 0.0f, timer / time);
			blackPanel.color = tempColor;
			yield return null;
		}

		blackPanel.gameObject.SetActive(value);
		_isFadingToBlack = false;
	}
}