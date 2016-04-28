using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class enemyScript : MonoBehaviour 
{	
	private float bbqPercent = 0.0f;

	private string achievementID = "BC";

	public GameObject burningObj , burntObj , levelObj , playerObj;
	public int burntObjCount , steps = 0;
	public levelCreator levelScript;
	public playerHandler playerScript;
	public SpriteRenderer playerSprite;

	void Start () 
	{
		burntObjCount = 0;

		steps = PlayerPrefs.GetInt("BBQIncrement");
		bbqPercent = PlayerPrefs.GetFloat("BBQ%Progress");
	}

	IEnumerator BurningTimer()
	{
		yield return new WaitForSeconds(0.4f);
		Destroy(burningObj.gameObject);

		if(burntObjCount < 1)
		{
			burntObj = Instantiate(Resources.Load("PF_Burnt", typeof(GameObject))) as GameObject;
			burntObjCount++;
		}

		levelObj = GameObject.Find("Main Camera");

		playerObj = GameObject.FindGameObjectWithTag("Player");

		if(playerObj != null)
		{
			playerScript = playerObj.GetComponent<playerHandler>();
		}

		if(playerObj != null)
		{
			playerScript = playerObj.GetComponent<playerHandler>();

			if(burntObj != null && playerScript != null)
			{
				burntObj.transform.position = new Vector2(playerScript.transform.position.x , transform.position.y);
				StartCoroutine("BurntTimer");
			}
		}
	}

	IEnumerator BurntTimer()
	{
		yield return new WaitForSeconds(3);
		Destroy(burntObj.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			levelObj = GameObject.Find("Main Camera");

			if(levelObj != null)
			{
				levelScript = levelObj.GetComponent<levelCreator>();
			}

			playerObj = GameObject.FindGameObjectWithTag("Player");

			if(playerObj != null)
			{
				playerScript = playerObj.GetComponent<playerHandler>();
				playerSprite = playerObj.GetComponent<SpriteRenderer>();

				if(playerScript != null)
				{
					playerScript.hitpoints--;
				}

				if(playerScript != null && playerScript.hitpoints < 1)
				{
					if(steps < 500)
					{
						steps++;
						PlayerPrefs.SetInt("BBQIncrement" , steps);
						
						bbqPercent += 0.2f;
						PlayerPrefs.SetFloat("BBQ%Progress" , bbqPercent);
						
						if(steps == 500)
						{
							GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true); //This is not working for some unknown reason but the achievement itself is unlocking which is good
						}
						
						ReportAchievement(bbqPercent);
					}
					playerSprite.enabled = false;
					playerScript.Death(playerScript.enemyDeathTime);

					burningObj = Instantiate(Resources.Load("PF_Burning", typeof(GameObject))) as GameObject;

					if(burningObj != null)
					{
						burningObj.transform.position = new Vector2(playerScript.transform.position.x , transform.position.y);
						StartCoroutine("BurningTimer");
					}
				}
			}

			playerScript.PlaySound("PlayerHit");
		}
	}

	void ReportAchievement(float bbqPercent) 
	{
		Social.ReportProgress(achievementID , bbqPercent , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}

	void Update () 
	{

	}
}
