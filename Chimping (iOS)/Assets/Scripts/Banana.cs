using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Banana : MonoBehaviour 
{
	private string achievementID = "HC";

	private bool releaseBanana = false;
	private int direction = 1;
	private float maxY , minY , bananaPercent = 0.0f;
	private SpriteRenderer bananaRenderer;
	
	public Animator anim;
	public bool inBananaPlay = true;
	public float defaultGameSpeed;
	public GameObject levelObj , playerObj;
	public int steps = 0;
	public levelCreator levelScript;
	public playerHandler playerScript;

	void Start () 
	{
		if(bananaPercent == 100)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
		}

		maxY = this.transform.position.y + 0.5f;
		minY = maxY - 1.0f;
		
		bananaRenderer = this.transform.GetComponent<SpriteRenderer>();

		levelObj = GameObject.Find("Main Camera");

		if(levelObj != null)
		{
			levelScript = levelObj.GetComponent<levelCreator>();
		}
		
		playerObj = GameObject.FindGameObjectWithTag("Player");
		
		if(playerObj != null)
		{
			playerScript = playerObj.GetComponent<playerHandler>();
		}

		bananaPercent = PlayerPrefs.GetFloat("Banana%Progress");
		steps = PlayerPrefs.GetInt("BananaIncrement");
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			if(steps < 500)
			{
				steps++;
				PlayerPrefs.SetInt("BananaIncrement" , steps);

				bananaPercent += 0.2f;
				PlayerPrefs.SetFloat("Banana%Progress" , bananaPercent);

				ReportAchievement(bananaPercent);
			}

			if(bananaRenderer != null)
			{
				switch(bananaRenderer.sprite.name)
				{
					case "spread_32" :

						if(levelScript != null)
						{
							levelScript.gameSpeed = defaultGameSpeed;
						}
						
					break;
				}
			}
			
			inBananaPlay = false;
			
			this.transform.position = new Vector2(this.transform.position.x , this.transform.position.y + 30.0f);
			
			if(playerScript != null)
			{
				playerScript.PlaySound("Banana");
			}
		}
	}

	void PlaceBanana()
	{
		inBananaPlay = true;
		releaseBanana = false;
		GameObject tmpTile = GameObject.Find("Main Camera").GetComponent<levelCreator>().tilePos;
		this.transform.position = new Vector2(tmpTile.transform.position.x , tmpTile.transform.position.y + 3.5f); 
		maxY = this.transform.position.y + 0.5f;
		minY = maxY;
	}

	void ReportAchievement(float bananaPercent) 
	{
		Social.ReportProgress(achievementID , bananaPercent , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}
	
	void ReSpawnBanana()
	{
		releaseBanana = true;
		Invoke("PlaceBanana" , (float)Random.Range(7 , 10));
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}
		
		this.transform.position = new Vector2(this.transform.position.x , this.transform.position.y + (direction * 0.1f));
		
		if (this.transform.position.y > maxY)
		{
			direction = -1;
		}
		
		if (this.transform.position.y < minY)
		{
			direction = 1;
		}
		
		if (!inBananaPlay && !releaseBanana)
		{
			ReSpawnBanana();
		}
	}
}
