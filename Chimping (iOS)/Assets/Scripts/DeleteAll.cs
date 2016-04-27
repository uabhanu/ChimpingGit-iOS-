using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class DeleteAll : MonoBehaviour 
{	
	void Start () 
	{
		GameCenterPlatform.ResetAllAchievements((resetResult) => 
        {
			Debug.Log((resetResult) ? "Reset done." : "Reset failed." );
		});

		PlayerPrefs.DeleteAll();
	}

	void Update () 
	{
	
	}
}
