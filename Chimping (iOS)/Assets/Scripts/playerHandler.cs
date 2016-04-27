using System;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SocialPlatforms.GameCenter;

public class playerHandler : MonoBehaviour 
{
	private AudioSource[] sounds;
	private bool inAir = false;

	private string achievementID01 = "CL";
	private string achievementID02 = "CA";

	public Animator _animator;
	public bool adShown , jumpPress = false , UI = false;
	public BoxCollider2D adsButtonCollider2D , pauseCollider2D , tryCollider2D;
	public float bananaTime , fallDeathTime , enemyDeathTime;
	public GameObject levelObj , ouchObj;
	public GUIText gameOverHighScoreDisplay , gameOverScoreDisplay , scoreDisplay , relativePathDisplay;
	public int chimpLegendShown , hitpoints , scoreValue , highScoreValue , incrementCount = 0 , ouch , timeValue;
	public int _animState = Animator.StringToHash("animState");
	public levelCreator levelScript;
	public ShowResult adResult;
	public SpriteRenderer adsButtonSprite , bananaHelpSprite , chimpingSprite , gameOverSprite , pauseSprite , resumeSprite , scoreSprite , trySprite;
	public SpriteRenderer[] gameSpeedRenderers;
	public string relativePath , zone = "defaultVideoAndPictureZone";

	void Start () 
	{
		adShown = false;

		chimpLegendShown = PlayerPrefs.GetInt("ChimpLegendShown");

		Advertisement.Initialize("21217");

		_animator = this.transform.GetComponent<Animator> ();

		Everyplay.SetMetadata("Score" , scoreValue);

		relativePath = Application.persistentDataPath;
		//relativePathDisplay.text = relativePath;

		chimpingSprite.enabled = false;
		gameOverSprite.enabled = false;
		gameOverHighScoreDisplay.enabled = false;
		gameOverScoreDisplay.enabled = false;
		resumeSprite.enabled = false;
		tryCollider2D.enabled = false;

		highScoreValue = PlayerPrefs.GetInt("highScoreValue");

		if(this.gameObject != null)
		{
			sounds = this.GetComponents<AudioSource>();
		}

		incrementCount = PlayerPrefs.GetInt("GameStarts");

		if (incrementCount < 500)
		{
			incrementCount++;
			PlayerPrefs.SetInt("GameStarts" , incrementCount);
		}

		if (incrementCount == 500)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
			ReportAchievement02();
		}

		StartCoroutine("BananaHelpOff");
		StartCoroutine("GameSpeed");
	}

	IEnumerator BananaHelpOff()
	{
		yield return new WaitForSeconds(bananaTime);
		bananaHelpSprite.enabled = false;
	}

	IEnumerator GameSpeed()
	{
		yield return new WaitForSeconds(3);

		if(levelScript != null)
		{
			if(!levelScript.playerDead)
			{
				levelScript.gameSpeed += 0.5f;
			}
		}

		StartCoroutine("GameSpeed");
	}

	IEnumerator Ouch()
	{
		yield return new WaitForSeconds(0.4f);
		Destroy(ouchObj.gameObject);
	}

	private void ReportAchievement01() 
	{
		Social.ReportProgress(achievementID01 , 100 , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}

	private void ReportAchievement02() 
	{
		Social.ReportProgress(achievementID02 , 0.2 , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
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
			break;
			
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
			break;
			
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public void Death(float time)
	{
		//Debug.Log("Death");

		if(levelScript != null)
		{
			levelScript.gameSpeed = 0;
		}

		hitpoints = 0;

		adsButtonCollider2D.enabled = false;
		adsButtonSprite.enabled = false;
		bananaHelpSprite.enabled = false;
		gameSpeedRenderers[0].enabled = false;
		gameSpeedRenderers[1].enabled = false;
		gameSpeedRenderers[2].enabled = false;
		gameSpeedRenderers[3].enabled = false;
		scoreDisplay.enabled = false;
		scoreSprite.enabled = false;
		pauseCollider2D.enabled = false;
		pauseSprite.enabled = false;
		resumeSprite.enabled = false;

		gameOverScoreDisplay.text = scoreValue.ToString();

		if(levelScript != null)
		{
			levelScript.playerDead = true;
		}

		if(scoreValue > highScoreValue)
		{
			PlayerPrefs.SetInt("highScoreValue" , scoreValue);
		}

		highScoreValue = PlayerPrefs.GetInt("highScoreValue");
		gameOverHighScoreDisplay.text = highScoreValue.ToString();

		Invoke("GameOver" , time);
	}

	public void GameOver()
	{
		if(Advertisement.isReady())
		{	
			if(!adShown)
			{
				ShowAd_CondensedVersion(zone);
				adShown = true;
			}
		}

		chimpingSprite.enabled = true;
		gameOverHighScoreDisplay.enabled = true;
		gameOverScoreDisplay.enabled = true;
		gameOverSprite.enabled = true;
		UI = true;
		
		tryCollider2D.enabled = true;
		trySprite.enabled = true;
	}

	public void Horizontal()
	{
		this.rigidbody2D.AddForce(Vector2.right * 300);
	}

	public void Jump()
	{
		if(!UI)
		{
			if (inAir)return;
			Vertical();

			if(levelScript != null && !levelScript.playerDead)
			{
				jumpPress = true;
				PlaySound("Jump");
			}
		}
		
		UI = false;
	}

	public void OnTriggerEnter2D(Collider2D col2D)
	{
		if(col2D.gameObject.tag.Equals("Enemy"))
		{
			ouch = (int)UnityEngine.Random.Range(1 , 3);

			if(ouch == 1)
			{
				ouchObj = Instantiate(Resources.Load("PF_Ouch" , typeof(GameObject))) as GameObject;
				StartCoroutine("Ouch");
			}

			else if(ouch == 2)
			{
				ouchObj = Instantiate(Resources.Load("PF_Oops" , typeof(GameObject))) as GameObject;
				StartCoroutine("Ouch");
			}
		}
	}

	public void Vertical()
	{
		this.rigidbody2D.AddForce(Vector2.up * 3500);
	}

//	public void OnVideoCompleted(string key , bool skipped)
//	{
//		adResult = ShowResult.Finished;
//	}

	public void PauseSound(string type)
	{
		switch (type) 
		{
			case "Background":
				sounds[4].Pause();
			break;
		}
	}

	public void PlaySound(string type)
	{
		switch (type) 
		{
			case "PlayerHurt":
				sounds[0].Play();
			break;
				
			case "Jump":
				sounds[1].Play();
			break;
				
			case "PlayerHit":
				sounds[2].Play();
			break;
				
			case "Medic":
				sounds[3].Play();
			break;

			case "Background":
				sounds[4].Play();
			break;

			case "Banana" :
				sounds[5].Play();
			break;
		}
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}
	
		levelObj = GameObject.FindGameObjectWithTag("MainCamera");
		
		if(levelObj != null)
		{
			levelScript = levelObj.GetComponent<levelCreator>();
		}

		if(levelScript != null)
		{
			timeValue = levelScript.timeValue;
		}

		if(!inAir && Mathf.Abs(this.rigidbody2D.velocity.y) > 0.01f)
		{
			_animator.SetInteger(_animState , 1);
			inAir =true;
		}

		else if(inAir && this.rigidbody2D.velocity.y == 0.00f)
		{
			if(levelScript != null && !levelScript.justChimping)
			{
				_animator.SetInteger(_animState , 0);
			}

			if(levelScript != null && levelScript.justChimping)
			{
				_animator.SetInteger(_animState , 2);
			}

			inAir = false;

			if(jumpPress)
			{
				Jump();
			}
		}

		if(transform.position.y < -6.5f)
		{
			Death(fallDeathTime);
		}

		if(scoreValue >= 2000)
		{
			if(chimpLegendShown < 1)
			{
				Social.localUser.Authenticate(success => 
              	{
					if(success)
					{
						GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
						ReportAchievement01();
					}
					else
					{
						Debug.Log("Failed to authenticate");
					}
				});

				chimpLegendShown++;
				PlayerPrefs.SetInt("ChimpLegendShown" , chimpLegendShown);
			}
		}

		scoreDisplay.text = scoreValue.ToString();
	}
}