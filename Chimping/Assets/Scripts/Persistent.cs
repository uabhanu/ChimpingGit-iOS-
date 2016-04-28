using GameThrivePush;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Persistent : MonoBehaviour 
{	
	public AudioSource backgroundMusic;
	public bool quitButton;
	public float volume;
	public GameObject everyplayObj , splashObj;
	public int levelNo;
	public playerHandler playerScript;
	
	void Awake()
	{
		backgroundMusic = GetComponent<AudioSource>();

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		DontDestroyOnLoad(transform.gameObject);
		everyplayObj = GameObject.Find("Everyplay");
		everyplayObj.SetActive(false);
		splashObj = GameObject.FindGameObjectWithTag("Splash");
	}
	
	void Start () 
	{
		StartCoroutine("Push");
	}

	IEnumerator Push()
	{
		yield return new WaitForSeconds(4);
		GameThrive.Init("4b86a5d6-81e0-11e4-adc4-3308cc0636f1" , "931469856" , HandleNotification);
	}

	void HandleNotification(string message , Dictionary<string , object> additionalData , bool isActive)
	{
		
	}
	
	void Update () 
	{
		levelNo = Application.loadedLevel;

		if(levelNo < 1)
		{
			if(quitButton)
			{
				if(volume < 1)
				{
					volume++;
					backgroundMusic.volume = volume;
				}
			}
		}
		else
		{
			volume = 0;
			backgroundMusic.volume = volume;
		}
	}	
}
