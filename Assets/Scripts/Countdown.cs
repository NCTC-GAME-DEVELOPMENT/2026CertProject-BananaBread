using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
	public TextMeshProUGUI NumberText;
    public GameObject CdText;
    public GameObject GoText;
	
	public int TimeValue = 3;

	void Start()
	{
		GoText.SetActive(false);
		StartCoroutine(TickDownTime(TimeValue));
	}

	IEnumerator TickDownTime(int tv)
	{
		for (int t = tv; t > 0; t--)
		{
			yield return new WaitForSeconds(1.0f);
			NumberText.text = TimeValue.ToString();
		}

		CdText.SetActive(false);
		GoText.SetActive(true);

		yield return new WaitForSeconds(1.0f);
		GoText.SetActive(false);
	}
}