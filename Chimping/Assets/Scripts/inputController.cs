using System.Collections;
using UnityEngine;

public class inputController : MonoBehaviour 
{

	private bool isMobile = true;
	private playerHandler playerScript;

	void Start () 
	{
		if (Application.isEditor)
		{
			isMobile = false;
		}
	
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHandler>();

	}
	
	void Update () 
	{
		if (isMobile) 
		{

			int tmpC = Input.touchCount;
			tmpC--;

			if(Input.GetTouch(tmpC).phase == TouchPhase.Began)
			{
				handleInteraction(true);
			}

			if(Input.GetTouch(tmpC).phase == TouchPhase.Ended)
			{
				handleInteraction(false);
			}

		}
		else
		{

			if(Input.GetMouseButtonDown(0))
			{
				handleInteraction(true);
			}

			if(Input.GetMouseButtonUp(0))
			{
				handleInteraction(false);
			}
		}
	}
	
	void handleInteraction(bool starting)
	{
		if(playerScript != null)
		{
			if (starting && Time.timeScale == 1) 
			{
				playerScript.Jump();
			}
			else
			{
				playerScript.jumpPress = false;
			}
		}
	}
}
