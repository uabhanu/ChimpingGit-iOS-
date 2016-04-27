using UnityEngine;
using System.Collections;

public class GameSpeed : MonoBehaviour 
{
	public GameObject gsObj;
	public levelCreator levelScript;
	public SpriteRenderer[] gsSprites;

	void Start () 
	{
		gsSprites[0].enabled = false;
		gsSprites[1].enabled = false;
		gsSprites[2].enabled = false;
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		if(levelScript != null)
		{
			if(levelScript.gameSpeed == 5)
			{
				gsSprites[0].enabled = true;
				gsSprites[1].enabled = false;
				gsSprites[2].enabled = false;
			}

			else if(levelScript.gameSpeed == 7)
			{
				gsSprites[0].enabled = false;
				gsSprites[1].enabled = true;
				gsSprites[2].enabled = false;
			}

			else if(levelScript.gameSpeed == 8)
			{
				gsSprites[0].enabled = false;
				gsSprites[1].enabled = false;
				gsSprites[2].enabled = true;
			}
		}
	}
}
