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
	public AudioClip tick;
	public AudioClip startAnnouncer;
	private AudioSource sound;
	
	public int TimeValue = 3;

	void Start()
	{
        sound = GetComponent<AudioSource>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        CdText = GameObject.Find("CountdownText");
        GoText = GameObject.Find("GoText");
		NumberText = CdText.GetComponent<TextMeshProUGUI>();

        GoText.SetActive(false);
		StartCoroutine(TickDownTime(TimeValue));
	}

	IEnumerator TickDownTime(int tv)
	{
		sound.clip = tick;
        NumberText.text = tv.ToString();
        for (int t = tv; t > 0; t--)
		{
			sound.Play();
            NumberText.text = t.ToString();
            yield return new WaitForSeconds(1.0f);
		}

        sound.clip = startAnnouncer;
        sound.Play();
		CdText.SetActive(false);
		GoText.SetActive(true);

		yield return new WaitForSeconds(1.0f);
		GoText.SetActive(false);
		gm.StartGame();
	}
}