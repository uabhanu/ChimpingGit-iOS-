using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class levelCreator : MonoBehaviour 
{
	private GameObject collectedTiles;
	private const float tileWidth = 1.08f;
	private int chimpChampPercent = 0 , heightLevel = 0 , cchaSteps , chicSteps = 0 , testPercent;
	private GameObject gameLayer;
	private GameObject bgLayer;
	private GameObject tmpTile;
	private float startUpPosY;
	private float chimpingChimpPercent = 0.0f , outofbounceX , startTime;
	private string lastTile = "right" , achievementID01 = "ChiC" , achievementID02 = "CCha" , achievementIDTest = "T";
	private bool enemyAdded = false;

	public bool bannerShown = false, justChimping , playerDead;
	public float chimpingStartTime , chimpingEndTime , endTimeOffset1 , endTimeOffset2 , gameSpeed = 6.0f , outOfBounceY , startTimeOffset;
	public GameObject justChimpObj , monkeyMiaObj , playerObj , tilePos;
	public GUIText timeDisplay;
	public int blankCounter = 0 , enemy , middleCounter = 0 , minBlankRange , maxBlankRange; 
	public int chimping , minMiddleRange , maxMiddleRange , timeValue;
	public playerHandler playerScript;

	void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
	}
	
	void Start () 
	{
		cchaSteps = 0;
		testPercent = 0;

		gameLayer = GameObject.Find("gameLayer");
		bgLayer = GameObject.Find("backgroundLayer");
		collectedTiles = GameObject.Find("tiles");

		justChimping = false;
		playerDead = false;

		timeDisplay.enabled = false;

		StartCoroutine("Chimping");

		playerObj = GameObject.FindGameObjectWithTag("Player");

		if(playerObj != null)
		{
			playerScript = playerObj.GetComponent<playerHandler>();
		}

		for(int i = 0; i < 30; i++)
		{
			GameObject tmpG1 = Instantiate(Resources.Load("ground_left", typeof(GameObject))) as GameObject;
			tmpG1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
			tmpG1.transform.position = Vector2.zero;

			GameObject tmpG2 = Instantiate(Resources.Load("ground_middle", typeof(GameObject))) as GameObject;
			tmpG2.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
			tmpG2.transform.position = Vector2.zero;

			GameObject tmpG3 = Instantiate(Resources.Load("ground_right", typeof(GameObject))) as GameObject;
			tmpG3.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
			tmpG3.transform.position = Vector2.zero;

			GameObject tmpG4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
			tmpG4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
			tmpG4.transform.position = Vector2.zero;
		}

		for (int i = 0; i < 10; i++) 
		{
			GameObject tmpG5 = Instantiate(Resources.Load("PF_Enemy" , typeof(GameObject))) as GameObject;
			tmpG5.transform.parent = collectedTiles.transform.FindChild("killers").transform;
			tmpG5.transform.position = Vector2.zero;
		}

		collectedTiles.transform.position = new Vector2(-60.0f, -20.0f);
		tilePos = GameObject.Find("startTilePosition");
		startUpPosY = tilePos.transform.position.y;
		outofbounceX = tilePos.transform.position.x - 5.0f;
		outOfBounceY = startUpPosY - 3.0f;
		
		FillScene ();
		startTime = Time.time;
	}

	private void ChangeHeight()
	{
		int newHeightLevel = (int)Random.Range(0 , 4);

		if(newHeightLevel<heightLevel)
		{
			heightLevel--;
		}

		else if(newHeightLevel>heightLevel)
		{
			heightLevel++;
		}
	}

	IEnumerator Chimping()
	{
		if(!playerDead)
		{
			//timeDisplay.enabled = true;
			
			yield return new WaitForSeconds(1);

			chimping = (int)Random.Range(1 , 3);
			
			timeValue++;
			timeDisplay.text = timeValue.ToString();

			if(!playerDead && timeValue == chimpingStartTime - startTimeOffset)
			{
				minMiddleRange = 43;
				maxMiddleRange = 50;
			}
			
			if(timeValue == chimpingStartTime)
			{

				if(chicSteps < 200)
				{
					chicSteps++;
					PlayerPrefs.SetInt("ChimpingChimpSteps" , chicSteps);

					chimpingChimpPercent += 1.0f;
					PlayerPrefs.SetFloat("ChimpingChimp%Progress" , chimpingChimpPercent);

					ReportAchievementChimpingChimp(chimpingChimpPercent);
				}

				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				justChimping = true;

				gameSpeed = 5.0f;

				foreach(GameObject enemy in enemies)
				{
					BoxCollider2D enemyCollider2D = enemy.GetComponent<BoxCollider2D>();
					enemyCollider2D.enabled = false;
					//Debug.Log("Enemies");
				}
				
				if(chimping == 1)
				{
					justChimpObj = Instantiate(Resources.Load("PF_JustChimping", typeof(GameObject))) as GameObject;
					StartCoroutine("JustChimping");
				}
				
				else if(chimping == 2)
				{
					justChimpObj = Instantiate(Resources.Load("PF_MonkeyMia", typeof(GameObject))) as GameObject;
					StartCoroutine("JustChimping");
				}
				
				playerScript._animator.SetInteger(playerScript._animState , 2);
				
				playerScript.scoreValue += 50;
			}

			if(!playerDead && timeValue == chimpingEndTime - endTimeOffset1)
			{
				minMiddleRange = 1;
				maxMiddleRange = 8;
			}
			
			if(timeValue == chimpingEndTime - endTimeOffset2)
			{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

				justChimping = false;
				
				foreach(GameObject enemy in enemies)
				{
					BoxCollider2D enemyCollider2D = enemy.GetComponent<BoxCollider2D>();
					enemyCollider2D.enabled = true;
					//Debug.Log("Enemies");
				}
				
				timeValue = 0;
				
				playerScript._animator.SetInteger(playerScript._animState , 0);
			}
			
			StartCoroutine("Chimping");
		}
	}

	private	void FillScene()
	{
		if(!playerDead)
		{
			for (int i = 0; i < 15; i++)
			{
				SetTile("middle");
			}
			
			SetTile("right");
		}
	}

	IEnumerator JustChimping()
	{
		yield return new WaitForSeconds(10);
		Destroy(justChimpObj.gameObject);
	}

	private void RandomizeEnemy()
	{
		if(!playerDead)
		{
			if(enemyAdded)
			{
				return;
			}

			if(middleCounter > 4)
			{
				GameObject newEnemy = collectedTiles.transform.FindChild("killers").transform.GetChild(0).gameObject;
				newEnemy.transform.parent = gameLayer.transform;
				newEnemy.transform.position = new Vector2(tilePos.transform.position.x + (tileWidth * (middleCounter / 2))  , startUpPosY + (heightLevel * tileWidth + (tileWidth * 2)));
				enemyAdded = true;
			}
		}
	}
	
	private void SetTile(string type)
	{
		switch (type){
			case "left":
				tmpTile = collectedTiles.transform.FindChild("gLeft").transform.GetChild(0).gameObject;
			break;

			case "middle":
				tmpTile = collectedTiles.transform.FindChild("gMiddle").transform.GetChild(0).gameObject;
			break;

			case "right":
				tmpTile = collectedTiles.transform.FindChild("gRight").transform.GetChild(0).gameObject;
			break;

			case "blank":
				tmpTile = collectedTiles.transform.FindChild("gBlank").transform.GetChild(0).gameObject;
			break;
		}

		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector3(tilePos.transform.position.x+(tileWidth),startUpPosY+(heightLevel * tileWidth),0);
		tilePos = tmpTile;
		lastTile = type;
	}
	
	private void SpawnTile()
	{
		if (blankCounter > 0) 
		{
			//Debug.Log("Tile Spawn");
			SetTile("blank");
			blankCounter--;
			return;
		}
		
		if (middleCounter > 0) 
		{
			RandomizeEnemy();

			SetTile("middle");
			middleCounter--;
			return;	
		}
		
		enemyAdded = false;
		
		if (lastTile == "blank") 
		{
			ChangeHeight();
			SetTile("left");
			middleCounter = (int)Random.Range(minMiddleRange , maxMiddleRange);
		}
		
		else if(lastTile == "right")
		{
			if(playerScript != null)
			{
				playerScript.scoreValue++;
			}
			
			blankCounter = (int)Random.Range(minBlankRange , maxBlankRange);
		}
		
		else if(lastTile == "middle")
		{
			SetTile("right");
		}
	}

	void ReportAchievementChimpingChimp(float chimpingChimpPercent) 
	{
		Social.ReportProgress(achievementID01 , chimpingChimpPercent , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}

	void ReportAchievementChimpChamp(int chimpChampPercent) 
	{
		Social.ReportProgress(achievementID02 , chimpChampPercent , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}

	void ReportTestAchievement(int testPercent) 
	{
		Social.ReportProgress(achievementIDTest , testPercent , (result) => 
      	{
			Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
		});	
	}

	void Update()
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		if(chicSteps == 200)
		{
			if(!bannerShown)
			{
				bannerShown = true;
				GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
			}
		}

		gameLayer.transform.position = new Vector2 (gameLayer.transform.position.x - gameSpeed * Time.deltaTime , 0);
		bgLayer.transform.position = new Vector2 (bgLayer.transform.position.x - gameSpeed/16 * Time.deltaTime, 0);
		
		foreach (Transform child in gameLayer.transform) 
		{
			if(child.position.x < outofbounceX)
			{
				switch(child.gameObject.name)
				{
				case "ground_left(Clone)" :
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gLeft").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
					break;
					
				case "ground_middle(Clone)" :
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gMiddle").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
					break;
					
				case "ground_right(Clone)" :
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gRight").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
					break;
					
				case "blank(Clone)" :
					child.gameObject.transform.position = collectedTiles.transform.FindChild("gBlank").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
					break;
					
				case "PF_Enemy(Clone)" :
					child.gameObject.transform.position = collectedTiles.transform.FindChild("killers").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild("killers").transform;
					break;
					
				case "Banana" :
					GameObject.Find("Banana").GetComponent<Banana>().inBananaPlay = false;
					break;
					
				case "Medic" :
					GameObject.Find("Medic").GetComponent<Medic>().inMedicPlay = false;
					break;
					
				default :
					Destroy(child.gameObject);
					break;
				}
			}
		}

		if(justChimping)
		{
			gameSpeed = 5.0f;
		}
		
		if(gameLayer.transform.childCount < 25)
		{
			SpawnTile();
		}
	}
}
