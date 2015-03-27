﻿using UnityEngine;
using System.Collections;

public class PlayerGetsHit : MonoBehaviour {

	bool hurt;
	static float lifeRemaining;
	float hurtStateTime;
	float hurtStateTimeRemaining;
	public MeshRenderer MRtoMessWith;
	static float damageAmount;

	// Use this for initialization
	void Start () {
		hurt = false;
		lifeRemaining = 5f;
		hurtStateTime = 2;
		damageAmount = 1f;
		hurtStateTimeRemaining = hurtStateTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(hurt){
			countdownHurtTime();
		}
	}

	public static void SetDamage(float newDamage)
	{
		damageAmount = newDamage;
	}

//NEED TO HAVE A CATCH FOR GOING OVER MAX LIFE
	public static void IncreaseLife(float increase)
	{
		lifeRemaining += increase;
	}

	void OnTriggerEnter(Collider collider) {

			if(!hurt && (collider.gameObject.tag =="enemy" || collider.gameObject.tag =="enemyCrow" || collider.gameObject.tag == "enemyGrozzle" || collider.gameObject.tag == "carnivore")){
			if(collider.gameObject.tag =="enemy") lifeRemaining-=1;
			if(collider.gameObject.tag =="enemyCrow") lifeRemaining-=0.5f;
			if(collider.gameObject.tag == "enemyGrozzle") lifeRemaining-=2f;
			if(collider.gameObject.tag == "carnivore") lifeRemaining-=01.0f;
			if(lifeRemaining<=0){
				Debug.Log("YOU DIED");
				Destroy(this.gameObject);
			}
			startHurtState();
		}
    }
    void startHurtState(){
    	hurt = true;
    	hurtStateTimeRemaining = hurtStateTime;
    	Debug.Log("IT IS NOW HURT" + " Life Remaining is " + lifeRemaining);

    }
    void countdownHurtTime(){
    	hurtStateTimeRemaining-= Time.deltaTime;
    	MRtoMessWith.enabled = ((Time.frameCount%3)!=0) ? false : true;
    	if(hurtStateTimeRemaining<=0){
    		 hurt = false;
    		 Debug.Log("IT IS NO LONGER HURT");
    		 MRtoMessWith.enabled = true;
    	}
    }
}
