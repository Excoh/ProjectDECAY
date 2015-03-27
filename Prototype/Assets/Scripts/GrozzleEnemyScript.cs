using UnityEngine;
using System.Collections;

public class GrozzleEnemyScript : MonoBehaviour 
{
	//Player Related Variables
	public Transform target;
	float playerDistance;
	GameObject playerObject;

	//Grozzle's Stats
	public float moveSpeed;
	public float moveMaxSpeed;
	public int rotationSpeed;
	public float grozzleHP;

	const float grozzleMaxHP = 10;
	const float moveSpeedMulti = 1.001f;
	float agroRange = 100;
	float attackRange = 10;
	float knockback = -500;

	//Regeneration Variables
	const float regenRate = 1;
	float regenTime;
	const float regenDelay = 3;

	private Transform grozzleTransform;

	void Awake()
	{
		//Automatically updates position
		grozzleTransform = transform;
	}

	// Use this for initialization
	void Start () 
	{
		//At start Grozzle is at full HP and in the idle state
		//grozzleHP = grozzleMaxHP;
		regenTime = Time.time + regenDelay;

		//Grozzle HP set to 1 in order to test Regen
		grozzleHP = 1;

		//Searches for object with "Player" Tag and locks on
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		target = playerObject.transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Updates distance between Player and Grozzle
		playerDistance = Vector3.Distance (target.position, grozzleTransform.position);

		//If Statements for Grozzle States
		if (playerDistance <= attackRange)
		{attackState();}
		else if (playerDistance <= agroRange) 
		{agroState();}
		else
		{idleState();}

		//Calls Regen function every regenDelay seconds
		if (Time.time > regenTime)
		{
			regenTime = Time.time + regenDelay;
			grozzleRegen ();
		}
	}

	void attackState()
	{
		//Grozzle Attacks
		Debug.Log ("The Grozzle is in the attack state");
	}

	void agroState ()
	{
		//Look at target
		grozzleTransform.rotation = Quaternion.Slerp (grozzleTransform.rotation, Quaternion.LookRotation (target.position - grozzleTransform.position), rotationSpeed * Time.deltaTime);
		
		//Moves towards target
		grozzleTransform.position += grozzleTransform.forward * moveSpeed * Time.deltaTime;

		//Speeds up
		if (moveSpeed < moveMaxSpeed)
		{moveSpeed = moveSpeed * moveSpeedMulti;}
	}

	void idleState()
	{
		//Player is too far away from Grozzle
		Debug.Log ("The Grozzle is in the idle state");
	}

	//Regenerate Grozzle's HP
	void grozzleRegen()
	{
		if (grozzleHP < grozzleMaxHP)
		{
			grozzleHP += regenRate;
			Debug.Log ("Grozzle HP Regenerated! Grozzle HP: " + grozzleHP);
		}
	}
}
