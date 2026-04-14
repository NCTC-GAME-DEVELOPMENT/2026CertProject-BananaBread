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
	private GameManager gm;
	
	public int TimeValue = 3;

	void Start()
	{
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        CdText = GameObject.Find("CountdownText");
        GoText = GameObject.Find("GoText");
		NumberText = CdText.GetComponent<TextMeshProUGUI>();

        GoText.SetActive(false);
		StartCoroutine(TickDownTime(TimeValue));
	}

	IEnumerator TickDownTime(int tv)
	{
        NumberText.text = tv.ToString();
        for (int t = tv; t > 0; t--)
		{
            NumberText.text = t.ToString();
            yield return new WaitForSeconds(1.0f);
		}

		CdText.SetActive(false);
		GoText.SetActive(true);

		yield return new WaitForSeconds(1.0f);
		GoText.SetActive(false);
		gm.StartGame();
	}
}