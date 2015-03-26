using UnityEngine;
using System.Collections;

public class PlayerGetsHit : MonoBehaviour {

	bool hurt;
	static float lifeRemaining;
	float hurtStateTime;
	float hurtStateTimeRemaining;
	public MeshRenderer MRtoMessWith;
	static float damageRatio;

	// Use this for initialization
	void Start () {
		hurt = false;
		lifeRemaining = 5f;
		hurtStateTime = 2;
		damageRatio = 1f;
		hurtStateTimeRemaining = hurtStateTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(hurt){
			countdownHurtTime();
		}
	}

	public static void SetDamageRatio(float newDamageRatio)
	{
		damageRatio = newDamageRatio;
	}

//NEED TO HAVE A CATCH FOR GOING OVER MAX LIFE
	public static void IncreaseLife(float increase)
	{
		lifeRemaining += increase;
	}

	void OnTriggerEnter(Collider collider) {
		if(!hurt && (collider.gameObject.tag =="enemy" || collider.gameObject.tag =="enemyCrow" || collider.gameObject.tag == "carnivore"||collider.gameObject.tag == "enemyNoxiousCrawler")){
			float damageDealt = effectiveDamageTabulation(collider);
			lifeRemaining-= damageDealt;
			if(lifeRemaining<=0){
				Debug.Log("YOU DIED");
				Destroy(this.gameObject);
			}
		startHurtState();
		}
	}

	float effectiveDamageTabulation(Collider collider){
		float retVal = 0;
		if(collider.gameObject.tag =="enemy") retVal=1;
		if(collider.gameObject.tag =="enemyCrow") retVal=0.5f;
		if(collider.gameObject.tag == "carnivore") retVal=01.0f;
		if(collider.gameObject.tag == "enemyNoxiousCrawler"){
			retVal=1.0f;
			collider.gameObject.GetComponent<NoxiousCrawlerEnemyScript>().reproduce();
			Destroy(collider.gameObject);
		}
		return retVal * damageRatio;
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
