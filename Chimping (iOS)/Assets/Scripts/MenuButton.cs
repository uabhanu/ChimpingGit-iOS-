using EveryplayMiniJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms.GameCenter;

public class MenuButton : MonoBehaviour 
{
	private GameCenterPlatform gcp;
	private string leaderboardID = "HS";

	public BoxCollider2D achieveCollider2D , adsCollider2D , errorOkCollider2D , exitCollider2D , gPlusCollider2D , gMinusCollider2D , goButtonCollider2D , leaderCollider2D , noCollider2D , notButtonCollider2D , pauseCollider2D , quitCollider2D , resumeCollider2D , rewardOkCollider2D , startCollider2D , tryCollider2D , yesCollider2D;
	public GameObject achieveButtonObj , adsButtonObj , adsPanelObj , adsErrorPanelObj , bananaHelpObj , chimpingObj , errorOkButtonObj , exitButtonObj , goButtonObj , leaderButtonObj , noButtonObj , notButtonObj , pauseButtonObj , pauseMenuObj , persistentObj , playerObj , quitButtonObj , quitObj , resumeButtonObj , rewardOkButtonObj , rewardPanelObj , scoreLabel , scoreValueLabel , socialObj , startButtonObj , tryButtonObj , yesButtonObj;
	public int highScore;
	public playerHandler playerScript;
	public Persistent persistentScript;
	public QuitPanel quitPanelScript;
	public SocialHandler socialScript;
	public SpriteRenderer adsErrorPanelSprite , adsPanelSprite , adsSprite , chimpingSprite , exitSprite , pauseMenuSprite , pauseSprite , quitSprite , rewardPanelSprite , shareSprite , trySprite;
	public string button , zone = "defaultVideoAndPictureZone";

	void Awake()
	{
		Advertisement.Initialize("21217");
		gcp = new GameCenterPlatform();
	}

	void Start () 
	{
		achieveButtonObj = GameObject.FindGameObjectWithTag("AButton");

		if(achieveButtonObj != null)
		{
			achieveCollider2D = achieveButtonObj.GetComponent<BoxCollider2D>();
		}

		adsButtonObj = GameObject.FindGameObjectWithTag("AdsButton");

		if(adsButtonObj != null)
		{
			adsCollider2D = adsButtonObj.GetComponent<BoxCollider2D>();
			adsSprite = adsButtonObj.GetComponent<SpriteRenderer>();
		}

		adsErrorPanelObj = GameObject.Find("AdsErrorPanel");

		if(adsErrorPanelObj != null)
		{
			adsErrorPanelSprite = adsErrorPanelObj.GetComponent<SpriteRenderer>();
		}

		adsPanelObj = GameObject.Find("AdsPanel");

		if(adsPanelObj != null)
		{
			adsPanelSprite = adsPanelObj.GetComponent<SpriteRenderer>();
		}

		bananaHelpObj = GameObject.FindGameObjectWithTag("BHelp");

		chimpingObj = GameObject.FindGameObjectWithTag("Chimping");

		if(chimpingObj != null)
		{
			chimpingSprite = chimpingObj.GetComponent<SpriteRenderer>();
		}

		errorOkButtonObj = GameObject.Find("ErrorOKButton");

		if(errorOkButtonObj != null)
		{
			errorOkCollider2D = errorOkButtonObj.GetComponent<BoxCollider2D>();
		}

		exitButtonObj = GameObject.Find("ExitButton");

		if (exitButtonObj != null) 
		{
			exitCollider2D = exitButtonObj.GetComponent<BoxCollider2D>();
			exitSprite = exitButtonObj.GetComponent<SpriteRenderer>();
		}

		goButtonObj = GameObject.Find("GoButton");

		if(goButtonObj != null)
		{
			goButtonCollider2D = goButtonObj.GetComponent<BoxCollider2D>();
		}

		leaderButtonObj = GameObject.FindGameObjectWithTag("LButton");

		if(leaderButtonObj != null)
		{
			leaderCollider2D = leaderButtonObj.GetComponent<BoxCollider2D>();
		}

		noButtonObj = GameObject.FindGameObjectWithTag("NButton");

		if(noButtonObj != null)
		{
			noCollider2D = noButtonObj.GetComponent<BoxCollider2D>();
		}

		notButtonObj = GameObject.Find("NotButton");

		if(notButtonObj != null)
		{
			notButtonCollider2D = notButtonObj.GetComponent<BoxCollider2D>();
		}

		pauseButtonObj = GameObject.FindGameObjectWithTag("PButton");

		if(pauseButtonObj != null)
		{
			pauseCollider2D = pauseButtonObj.GetComponent<BoxCollider2D>();
			pauseSprite = pauseButtonObj.GetComponent<SpriteRenderer>();
		}

		pauseMenuObj = GameObject.FindGameObjectWithTag("PM");

		if(pauseMenuObj != null)
		{
			pauseMenuSprite = pauseMenuObj.GetComponent<SpriteRenderer>();
		}

		persistentObj = GameObject.FindGameObjectWithTag("Persistent");

		if(persistentObj != null)
		{
			persistentScript = persistentObj.GetComponent<Persistent>();
		}

		quitButtonObj = GameObject.FindGameObjectWithTag("QButton");

		if(quitButtonObj != null)
		{
			quitCollider2D = quitButtonObj.GetComponent<BoxCollider2D>();
		}

		quitObj = GameObject.FindGameObjectWithTag("QuitPanel");

		if(quitObj != null)
		{
			quitPanelScript = quitObj.GetComponent<QuitPanel>();
			quitPanelScript.quitRenderer.enabled = false;
		}

		resumeButtonObj = GameObject.FindGameObjectWithTag("RButton");

		if(resumeButtonObj != null)
		{
			resumeCollider2D = resumeButtonObj.GetComponent<BoxCollider2D>();
		}

		rewardOkButtonObj = GameObject.Find("RewardOKButton");

		if(rewardOkButtonObj != null)
		{
			rewardOkCollider2D = rewardOkButtonObj.GetComponent<BoxCollider2D>();
		}

		rewardPanelObj = GameObject.Find("RewardPanel");

		if(rewardPanelObj != null)
		{
			rewardPanelSprite = rewardPanelObj.GetComponent<SpriteRenderer>();
		}

		scoreLabel = GameObject.FindGameObjectWithTag("SLabel");
		scoreValueLabel = GameObject.FindGameObjectWithTag("SDisplay");

		socialObj = GameObject.FindGameObjectWithTag("Social");

		if(socialObj != null)
		{
			socialScript = socialObj.GetComponent<SocialHandler>();
		}

		startButtonObj = GameObject.Find("StartButton");

		if (startButtonObj != null) 
		{
			startCollider2D = startButtonObj.GetComponent<BoxCollider2D>();
		}

		tryButtonObj = GameObject.FindGameObjectWithTag("TButton");

		if(tryButtonObj != null)
		{
			tryCollider2D = tryButtonObj.GetComponent<BoxCollider2D>();
			trySprite = tryButtonObj.GetComponent<SpriteRenderer>();
		}

		yesButtonObj = GameObject.FindGameObjectWithTag("YButton");

		if(yesButtonObj != null)
		{
			yesCollider2D = yesButtonObj.GetComponent<BoxCollider2D>();
		}

		Inactive("Achieve");
		Inactive("AdsErrorPanel");
		Inactive("AdsPanel");
		Inactive("Exit");
		Inactive("Leader");
		Inactive("No");
		Inactive("PM");
		Inactive("Quit");
		Inactive("Resume");
		Inactive("RewardPanel");
		Inactive("Start");
		Inactive("Try");
		Inactive("Yes");

		StartCoroutine("GetPlayer");
	}

	IEnumerator GetPlayer()
	{
		yield return new WaitForSeconds(0.3f);

		playerObj = GameObject.FindGameObjectWithTag("Player");
		
		if(playerObj != null)
		{
			playerScript = playerObj.GetComponent<playerHandler>();
		}

		StartCoroutine("GetPlayer");
	}

	private void ReportScore(long score , string leaderboardID) 
	{
		Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
		
		Social.ReportScore(score , leaderboardID , success => 
   		{
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}

	private void ShowAd_CondensedVersion(string zone)
	{
		Advertisement.Show(zone, new ShowOptions 
   		{
			resultCallback = result => 
			{
				HandleShowResult(result);
			}
		});
	}

	private void ShowAd_ExpandedVersion(string zone)
	{
		ShowOptions options = new ShowOptions();
		options.pause = true;
		options.resultCallback = HandleShowResult;
		
		Advertisement.Show(zone,options);
	}

	private void HandleShowResult(ShowResult result)
	{
		switch(result)
		{
			case ShowResult.Finished:

				Debug.Log("The ad was successfully shown.");
				playerScript.scoreValue += 15;

				Active("RewardPanel");
				Inactive("Ads");
				Inactive("AdsPanel");
				
				if(playerScript != null)
				{
					playerScript.UI = true;
				}
			
				Time.timeScale = 0;

			break;

			case ShowResult.Skipped:

				Debug.Log("The ad was skipped before reaching the end.");

				Inactive("Ads");
				Inactive("AdsPanel");
				Inactive("BananaHelp");
				Inactive("Pause");
				Inactive("RewardPanel");
				Inactive("ScoreLabel");
				Inactive("ScoreValueDisplay");
				Active("Achieve");
				Active("Leader");
				Active("PM");
				Active("Quit");
				Active("Resume");
				
				if(playerScript != null)
				{
					playerScript.UI = true;
				}
				
				Time.timeScale = 0;

			break;

			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public void Active(string button)
	{
		switch(button)
		{
			case "Achieve" :

				if(achieveCollider2D != null)
				{
					achieveCollider2D.enabled = true;
				}

			break;

			case "Ads" :
				
				if(adsCollider2D != null)
				{
					adsCollider2D.enabled = true;
				}

				if(adsSprite != null)
				{
					adsSprite.enabled = true;
				}

			break;

			case "AdsErrorPanel" :

				if(adsErrorPanelSprite != null)
				{
					adsErrorPanelSprite.enabled = true;
				}

				if(errorOkCollider2D != null)
				{
					errorOkCollider2D.enabled = true;
				}

			break;

			case "AdsPanel" :

				if(adsPanelSprite != null)
				{
					adsPanelSprite.enabled = true;
				}

				if(goButtonCollider2D != null)
				{
					goButtonCollider2D.enabled = true;
				}

				if(notButtonCollider2D != null)
				{
					notButtonCollider2D.enabled = true;
				}

			break;

			case "BananaHelp" :
				bananaHelpObj.SetActive(true);
			break;
		
			case "Chimping" :
				
				if(chimpingSprite != null)
				{
					chimpingSprite.enabled = true;
				}
				
			break;

			case "Exit" :

				if(exitCollider2D != null)
				{
					exitCollider2D.enabled = true;
				}

				if(exitSprite != null)
				{
					exitSprite.enabled = true;
				}

			break;

			case "Leader" :
				
				if(leaderCollider2D != null)
				{
					leaderCollider2D.enabled = true;
				}

			break;

			case "No" :

				if(noCollider2D != null)
				{
					noCollider2D.enabled = true;
				}

			break;

			case "Pause" :

				if(pauseCollider2D != null)
				{
					pauseCollider2D.enabled = true;
				}
				
				if(pauseSprite != null)
				{
					pauseSprite.enabled = true;
				}

			break;

			case "PM" :
				
				if(pauseMenuSprite != null)
				{
					pauseMenuSprite.enabled = true;
				}

			break;

			case "Quit" :
				
				if(quitCollider2D != null)
				{
					quitCollider2D.enabled = true;
				}

			break;

			case "Resume" :

				if(resumeCollider2D != null)
				{
					resumeCollider2D.enabled = true;
				}

			break;

			case "RewardPanel" :

				if(rewardOkCollider2D != null)
				{
					rewardOkCollider2D.enabled = true;
				}

				if(rewardPanelSprite != null)
				{
					rewardPanelSprite.enabled = true;
				}

			break;

			case "ScoreLabel" :
				scoreLabel.SetActive(true);
			break;

			case "ScoreValueDisplay" :
				scoreValueLabel.SetActive(true);
			break;

			case "Start" :

				if(startCollider2D != null)
				{
					startCollider2D.enabled = true;
				}

			break;

			case "Try" :
				
				if(tryCollider2D != null)
				{
					tryCollider2D.enabled = true;
				}
				
				if(trySprite != null)
				{
					trySprite.enabled = true;
				}
			
			break;

			case "Yes" :

				if(yesCollider2D != null)
				{
					yesCollider2D.enabled = true;
				}

			break;
		}
	}

	public void Inactive(string button)
	{
		switch(button)
		{
			case "Achieve" :

				if(achieveCollider2D != null)
				{
					achieveCollider2D.enabled = false;
				}

			break;

			case "Ads" :
				
				if(adsCollider2D != null)
				{
					adsCollider2D.enabled = false;
				}
				
				if(adsSprite != null)
				{
					adsSprite.enabled = false;
				}
			
			break;

			case "AdsErrorPanel" :
				
				if(adsErrorPanelSprite != null)
				{
					adsErrorPanelSprite.enabled = false;
				}
				
				if(errorOkCollider2D != null)
				{
					errorOkCollider2D.enabled = false;
				}
			
			break;

			case "AdsPanel" :
				
				if(adsPanelSprite != null)
				{
					adsPanelSprite.enabled = false;
				}
				
				if(goButtonCollider2D != null)
				{
					goButtonCollider2D.enabled = false;
				}
				
				if(notButtonCollider2D != null)
				{
					notButtonCollider2D.enabled = false;
				}
			
			break;

			case "BananaHelp" :
				bananaHelpObj.SetActive(false);
			break;

			case "Chimping" :
			
				if(chimpingSprite != null)
				{
					chimpingSprite.enabled = false;
				}
			
			break;

			case "Exit" :
				
				if(exitCollider2D != null)
				{
					exitCollider2D.enabled = false;
				}
				
				if(exitSprite != null)
				{
					exitSprite.enabled = false;
				}
			
			break;

			case "Leader" :
				
				if(leaderCollider2D != null)
				{
					leaderCollider2D.enabled = false;
				}

			break;

			case "No" :

				if(noCollider2D != null)
				{
					noCollider2D.enabled = false;
				}

				if(quitPanelScript != null)
				{
					quitPanelScript.startCollider2D.enabled = true;
				}

			break;

			case "Pause" :
				
				if(pauseCollider2D != null)
				{
					pauseCollider2D.enabled = false;
				}

				if(pauseSprite != null)
				{
					pauseSprite.enabled = false;
				}
				
			break;

			case "PM" :
				
				if(pauseMenuSprite != null)
				{
					pauseMenuSprite.enabled = false;
				}
			
			break;

			case "Quit" :

				if(quitCollider2D != null)
				{
					quitCollider2D.enabled = false;
				}

			break;
			
			case "Resume" :

				if(resumeCollider2D != null)
				{
					resumeCollider2D.enabled = false;
				}

			break;

			case "RewardPanel" :
				
				if(rewardOkCollider2D != null)
				{
					rewardOkCollider2D.enabled = false;
				}
				
				if(rewardPanelSprite != null)
				{
					rewardPanelSprite.enabled = false;
				}
			
			break;

			case "ScoreLabel" :
				scoreLabel.SetActive(false);
			break;
			
			case "ScoreValueDisplay" :
				scoreValueLabel.SetActive(false);
			break;

			case "Start" :
				
				if(startCollider2D != null)
				{
					startCollider2D.enabled = false;
				}
				
			break;
				
			case "Try" :
				
				if(tryCollider2D != null)
				{
					tryCollider2D.enabled = false;
				}
				
				if(trySprite != null)
				{
					trySprite.enabled = false;
				}
				
			break;

			case "Yes" :

				if(yesCollider2D != null)
				{
					yesCollider2D.enabled = false;
				}

			break;
		}
	}

	public void ShowAd(string zone)
	{
		bool useCondensedVersion = true;

		if (useCondensedVersion)
		{
			ShowAd_CondensedVersion(zone);
		}
		else
		{
			ShowAd_ExpandedVersion(zone);
		}
	}

	void ButtonClick(string button)
	{
		switch(button)
		{
			case "Achieve" :
				gcp.ShowAchievementsUI();
			break;

			case "Ads" :

				Active("AdsPanel");

				if(pauseMenuSprite.enabled)
				{
					resumeCollider2D.enabled = false;
					achieveCollider2D.enabled = false;
					leaderCollider2D.enabled = false;
					quitCollider2D.enabled = false;
				}
				else
				{
					Time.timeScale = 0;
				}

				if(playerScript != null)
				{
					playerScript.UI = true;
				}

			break;

			case "Exit" :
				Active("No");
				Active("Yes");
				Inactive("Exit");
				Inactive("Start");
				quitPanelScript.quitRenderer.enabled = true;
			break;

			case "Go" :

				if(Advertisement.isReady())
				{
					Debug.Log("Showing Ads");

					if(playerScript != null)
					{
						playerScript.UI = true;	
					}
					
					if(!resumeCollider2D.enabled)
					{
						Time.timeScale = 0;	
					}
					
					ShowAd_CondensedVersion(zone);
				}

				else if(!Advertisement.isReady())
				{
					Debug.Log("No more Ads Left");

					if(pauseMenuSprite.enabled)
					{
						resumeCollider2D.enabled = false;
						achieveCollider2D.enabled = false;
						leaderCollider2D.enabled = false;
						quitCollider2D.enabled = false;

						Active("AdsErrorPanel");
						Inactive("AdsPanel");
					}
					else
					{
						Active("AdsErrorPanel");
						Inactive("AdsPanel");
					}
				}
				
				if(playerScript != null)
				{
					//Debug.Log("Ads Button");
					playerScript.UI = true;
				}

			break;

			case "Leader" :
				
				if(playerScript != null && playerScript.scoreValue > playerScript.highScoreValue)
				{
					ReportScore(playerScript.scoreValue , leaderboardID);
				}

				gcp.ShowLeaderboardUI();

			break;

			case "No" :
				//Debug.Log("No Button");
				Active("Exit");
				Active("Start");
				Inactive("No");
				Inactive("Yes");
				quitPanelScript.quitRenderer.enabled = false;
			break;

			case "Not" :

				Inactive("AdsPanel");

				if(pauseMenuSprite.enabled)
				{
					resumeCollider2D.enabled = true;
					achieveCollider2D.enabled = true;
					leaderCollider2D.enabled = true;
					quitCollider2D.enabled = true;
				}
				else
				{
					Active("Ads");
					Active("BananaHelp");
					Active("Pause");
					Active("ScoreLabel");
					Active("ScoreValueDisplay");
					Time.timeScale = 1;
				}

				if(playerScript != null)
				{
					playerScript.UI = true;
				}

			break;

			case "OK" :

				if(!pauseMenuSprite.enabled && !adsErrorPanelSprite.enabled)
				{
					Active("Ads");
					Active("BananaHelp");
					Active("Pause");
					Active("ScoreLabel");
					Active("ScoreValueDisplay");
					Time.timeScale = 1;
				}

				if(pauseMenuSprite.enabled)
				{
					resumeCollider2D.enabled = true;
					achieveCollider2D.enabled = true;
					leaderCollider2D.enabled = true;
					quitCollider2D.enabled = true;
					
					Inactive("AdsErrorPanel");
				}

				else
				{
					Inactive("AdsErrorPanel");
					
					if(pauseMenuSprite.enabled)
					{
						Time.timeScale = 0;
					}
					else
					{
						Time.timeScale = 1;
					}

				}

				Inactive("RewardPanel");

				if(playerScript != null)
				{
					playerScript.UI = true;
				}

			break;

			case "Pause" :

				Inactive("BananaHelp");
				Inactive("Pause");
				Inactive("ScoreLabel");
				Inactive("ScoreValueDisplay");
				Active("Achieve");
				Active("Leader");
				Active("PM");
				Active("Quit");
				Active("Resume");

				if(playerScript != null)
				{
					playerScript.PauseSound("Background");
					playerScript.UI = true;
				}

				Time.timeScale = 0;
			break;

			case "Quit" : 
				
				if(persistentScript != null)
				{
					persistentScript.quitButton = true;
				}

				Application.LoadLevel(Application.loadedLevel - 1);

			break;

			case "Record" :
				Debug.Log("Record Game");
				Everyplay.StartRecording();
			break;

			case "Resume" :
				Inactive("Achieve");
				Inactive("Leader");
				Inactive("PM");
				Inactive("Quit");
				Inactive("Resume");
				Active("Ads");
				Active("BananaHelp");
				Active("Pause");
				Active("ScoreLabel");
				Active("ScoreValueDisplay");
				playerScript.PlaySound("Background");
				playerScript.UI = true;
				Time.timeScale = 1;
			break;

			case "Start" :
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel + 1);
			break;

			case "Stop" :
				Debug.Log("Stop Recording");
				Everyplay.StopRecording();
			break;

			case "Try" :
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel);
			break;

			case "Yes" :
				Debug.Log("Yes Button");	
				Application.Quit();
			break;
		}
	}

	void OnMouseDown()
	{
		if(button == "Achieve")
		{
			ButtonClick("Achieve");
		}

		if(button == "Ads")
		{
			ButtonClick("Ads");
		}

		if (button == "Exit") 
		{
			ButtonClick("Exit");
		}

		if(button == "Go")
		{
			ButtonClick("Go");
		}

		if(button == "Leader")
		{
			ButtonClick("Leader");
		}

		if(button == "No")
		{
			ButtonClick("No");
		}

		if(button == "Not")
		{
			ButtonClick("Not");
		}

		if(button == "OK")
		{
			ButtonClick("OK");
		}

		if(button == "Pause")
		{
			ButtonClick("Pause");
		}

		if(button == "Quit")
		{
			ButtonClick("Quit");
		}

		if(button == "Record")
		{
			ButtonClick("Record");
		}

		if(button == "Resume")
		{
			ButtonClick("Resume");
		}

		if(button == "Start")
		{
			ButtonClick("Start");
		}

		if(button == "Stop")
		{
			ButtonClick("Stop");
		}

		if(button == "Try")
		{
			ButtonClick("Try");
		}

		if(button == "Yes")
		{
			ButtonClick("Yes");
		}
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		if(playerScript != null)
		{
			highScore = playerScript.highScoreValue;
		}
	}
}
