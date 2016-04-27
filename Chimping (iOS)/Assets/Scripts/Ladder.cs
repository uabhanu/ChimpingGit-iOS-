using System.Collections;
using UnityEngine;

public class Ladder : MonoBehaviour 
{
	void Start () 
	{
	
	}

	void FixedUpdate()
	{
		transform.position = new Vector3(transform.position.x - 0.05f , transform.position.y , transform.position.z);	
	}

	void Update () 
	{
		if(Time.timeScale == 0)
		{
			return;
		}

		if(transform.position.x < -14.0f)
		{
			Destroy(this.gameObject);
			return;
		}
	}
}
