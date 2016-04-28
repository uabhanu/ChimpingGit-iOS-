using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Medic : MonoBehaviour 
{
	private string achievementID = "RC";

	private bool releaseMedic = false;
	private float maxY , minY , medicPercent;
	private int direction = 1 , percent;
	private SpriteRenderer medicRenderer;

	public Animator anim;
	public bool inMedicPlay = true;
	public GameObject levelObj , playerObj;
	public int steps = 0;
	public levelCreator levelScript;
	public playerHandler playerScript;
	
	void Start () 
	{
		if(medicPercent == 100)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
		}

		maxY = this.transform.position.y + 0.5f;
		minY = maxY - 1.0f;

		levelObj = GameObject.Find("Main Camera");

		if(levelObj != null)
		{
			levelScript = levelObj.GetComponent<levelCreator>();
		}

		medicRenderer = this.transform.GetComponent<SpriteRenderer>();

		playerObj = GameObject.FindGameObjectWithTag("Player");

		if(playerObj != null)
		{
			playerScript = playerObj.GetComponent<playerHandler>();
		}

		steps = PlayerPrefs.GetInt("MedicIncrement");
		medicPercent = PlayerPrefs.GetFloat("Medic%Progress");
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			if(playerScript != null && playerScript.hitpoints < 2)
			{
				if(steps < 500)
				{
					steps++;
					PlayerPrefs.SetInt("MedicIncrement" , steps);

					medicPercent += 0.2f;
					PlayerPrefs.SetFloat("Medic%Progress" , medicPercent);

					ReportAchievement(medicPercent);
				}
			}

			if(medicRenderer != null)
			{
				switch(medicRenderer.sprite.name)
				{
					case "spread_30" :
						
						if(levelScript != null && playerScript != null)
						{
							if(!levelScript.playerDead)
							{
								playerScript.hitpoints = 3;
							}
						}
					
					break;
				}
			}

			inMedicPlay = false;

			this.transform.position = new Vector2(this.transform.position.x , this.transform.position.y + 30.0f);

			if(playerScript != null)
			{
				playerScript.PlaySound("Medic");
			}
		}
	}

	void PlaceMedic()
	{
		inMedicPlay = true;
		releaseMedic = false;
		GameObject tmpTile = GameObject.Find("Main Camera").GetComponent<levelCreator>().tilePos;
		this.transform.position = new Vector2(tmpTile.transform.position.x , tmpTile.transform.position.y + 5.5f); 
		maxY = this.transform.position.y + 0.5f;
		minY = maxY;
	}

	void ReportAchievement(float medicPercent) 
	{
		Social.ReportProgress(achievementID , medicPercent , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}
	
	void ReSpawnMedic()
	{
		releaseMedic = true;
		Invoke("PlaceMedic" , (float)Random.Range (3 , 6));
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}
		
		this.transform.position = new Vector2(this.transform.position.x , this.transform.position.y + (direction * 0.05f));
		
		if (this.transform.position.y > maxY)
		{
			direction = -1;
		}
		
		if (this.transform.position.y < minY)
		{
			direction = 1;
		}
		
		if (!inMedicPlay && !releaseMedic)
		{
			ReSpawnMedic();
		}
	}
}
