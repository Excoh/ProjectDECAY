using UnityEngine;
using System.Collections;

public class NightVisionScript : MonoBehaviour
{
	//Light Variables
	GameObject nightVision;
	Light lightComp;
	public Color lightColor;
	public float lightInt;
	public float lightRange;

	//Player Variables
	GameObject playerObject;
	public Transform target;

	// Use this for initialization
	void Start ()
	{
		//Locate Player
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		target = playerObject.transform;

		NightVisionMode();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (lightComp.color == Color.clear)
			{NightVisionOn();}

			else
			{NightVisionOff();}
		}

		//Update position of Night Vision Light
		nightVision.transform.position = target.position;
	}

	void NightVisionMode() //Defines Night Vision Object
	{
		//Light Components
		nightVision = new GameObject("Night Vision Mode");
		lightComp = nightVision.AddComponent<Light>();
		lightComp.color = Color.clear;
	}

	void NightVisionOn() //Switches Light Color On
	{
		lightComp.color = lightColor;

		//To make light follow player
		nightVision.transform.position = target.position;
		lightComp.intensity = lightInt;
		lightComp.range = lightRange;
	}

	void NightVisionOff() //Switches Light Color Off
	{lightComp.color = Color.clear;}
}
