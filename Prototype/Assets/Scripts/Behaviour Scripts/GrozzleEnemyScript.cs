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

	const float moveSpeedMulti = 1.001f;
	float agroRange = 100;
	float attackRange = 10;
	float knockback = -500;

	private Transform grozzleTransform;

	void Awake()
	{
		//Automatically updates position
		grozzleTransform = transform;
	}

	// Use this for initialization
	void Start () 
	{
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
		{
			attackState();
			agroState();
		}
		else if (playerDistance <= agroRange) 
		{agroState();}
		else
		{idleState();}
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
		//Debug.Log ("The Grozzle is in the idle state");
	}
}
