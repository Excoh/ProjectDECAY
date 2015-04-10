using UnityEngine;
using System.Collections;

public class EnemyGetHitScript : MonoBehaviour {

	float lifeRemaining;

	//Regeneration Variables
	const float regenRate = 1;
	float regenTime;
	const float regenDelay = 3;

	// Use this for initialization
	void Start () {
		init();
		regenTime = Time.time + regenDelay;
	}
	
	// Update is called once per frame
	void Update () {
		//Calls Regen function every regenDelay seconds
		if (Time.time > regenTime)
		{
			regenTime = Time.time + regenDelay;
			grozzleRegen ();
		}
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag =="sword"||collider.gameObject.tag =="bullet"){
			getHit(TopDownCharacterController.GetEffectiveAttackStrength());
			Destroy(collider.gameObject);// Bullet explodes sword ... bounces off?
		}
    }
    const float defaultInitialLife = 1.0f;
    const float crowInitialLife = 1.0f;
    const float crawlerInitialLife = 3.0f;
	const float grozzleInitialLife = 5.0f;

    void init(){
    	if(this.gameObject.tag=="enemyCrow"){
    		 lifeRemaining = crowInitialLife;
		 }else if(this.gameObject.tag=="enemyNoxiousCrawler"){
    		 lifeRemaining = crawlerInitialLife;
		 }else if(this.gameObject.tag=="enemyGrozzle"){
			lifeRemaining = grozzleInitialLife;
		 }else{
		 	lifeRemaining = defaultInitialLife;//default val
		 }
    }

	void getHit(float damage){
		lifeRemaining-=damage;
		if(lifeRemaining<=0){
			if(this.gameObject.tag=="enemyNoxiousCrawler"){
				gameObject.GetComponent<NoxiousCrawlerEnemyScript>().reproduce();
			} 
			Destroy(this.gameObject);
		}
	}

	//Regenerate Grozzle's HP
	void grozzleRegen()
	{
		if (this.gameObject.tag == "enemyGrozzle")
		{
			if (lifeRemaining < grozzleInitialLife)
			{
				lifeRemaining += regenRate;
				Debug.Log ("Grozzle HP Regenerated! Grozzle HP: " + lifeRemaining);
			}
		}
	}
}
