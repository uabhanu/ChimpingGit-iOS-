using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
	public GameObject levelObj , playerObj;
	public int health;
	public levelCreator levelScript;
	public playerHandler playerScript;
	public Sprite[] healthSprites;
	public SpriteRenderer hudRender;

	void Start () 
	{
		if(this.gameObject != null)
		{
			hudRender = GetComponent<SpriteRenderer>();
		}

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

		StartCoroutine("HUD");
	}

	IEnumerator HUD()
	{
		yield return new WaitForSeconds(0.2f);

		if(levelScript != null)
		{
			if(!levelScript.justChimping)
			{
				if(health == 0)
				{
					hudRender.sprite = healthSprites[0];
				}
				
				else if(health == 1)
				{
					hudRender.sprite = healthSprites[1];
				}
				
				else if(health == 2)
				{
					hudRender.sprite = healthSprites[2];
				}
				
				else if(health == 3)
				{
					hudRender.sprite = healthSprites[3];
				}
			}

			else if(levelScript.justChimping)
			{
				hudRender.sprite = healthSprites[4];
			}
		}

		StartCoroutine("HUD");
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		if(playerScript != null)
		{
			health = playerScript.hitpoints;
		}
	}
}
