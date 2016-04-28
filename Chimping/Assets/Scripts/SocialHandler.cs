using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class SocialHandler : MonoBehaviour 
{
	public AudioSource backgroundMusic;
	public float splashTime;
	public GameObject persistentSplashObj , PF_PersistentSplash , quitObj , splashObj , startObj;
	public GUISkin gpgsSkin;
	public int incrementalCount = 5;
	public MenuButton menuScript;
	public Persistent persistentScript;
	public QuitPanel quitScript;
	public SpriteRenderer splashSprite;

	void Start () 
	{
		persistentSplashObj = GameObject.Find("PF_PersistentSplash(Clone)");

		if(persistentSplashObj == null)
		{
			persistentSplashObj = Instantiate(PF_PersistentSplash , new Vector3(0 , 0 , 0) , Quaternion.identity) as GameObject;
			persistentScript = persistentSplashObj.GetComponent<Persistent>();

			persistentScript.everyplayObj.SetActive(true);

			splashObj = GameObject.FindGameObjectWithTag("Splash");
			
			if(splashObj != null)
			{
				splashSprite = splashObj.GetComponent<SpriteRenderer>();
			}
			
			if(persistentScript.splashObj != null)
			{
				splashSprite.enabled = true;
				StartCoroutine("SplashTimer");
				persistentScript.splashObj = null;
			}
		}

		quitObj = GameObject.FindGameObjectWithTag("QuitPanel");

		if(quitObj != null)
		{
			quitScript = quitObj.GetComponent<QuitPanel>();
		}

		startObj = GameObject.FindGameObjectWithTag("Start");

		if(startObj != null)
		{
			backgroundMusic = startObj.GetComponent<AudioSource>();
		}

		StartCoroutine("MainMenuButtons");
		Time.timeScale = 1;
	}

	IEnumerator MainMenuButtons()
	{
		yield return new WaitForSeconds(0.2f);	

		if (splashSprite == null) 
		{
			menuScript.Active("Exit");
			menuScript.Active("Start");
		}
	}

	IEnumerator SplashTimer()
	{
		yield return new WaitForSeconds(splashTime);
		splashSprite.enabled = false;
		backgroundMusic.Play();
		menuScript.Active("Exit");
		menuScript.Active("Start");

		Social.localUser.Authenticate( success => 
		{
			if (success)
			{
				Debug.Log("Game Center Logged In");
			}
			else
			{
				Debug.Log ("Failed to authenticate");
			}
		});
	}
		
	void Update () 
	{

	}
}
