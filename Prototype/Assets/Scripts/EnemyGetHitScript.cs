using UnityEngine;
using System.Collections;

public class EnemyGetHitScript : MonoBehaviour {

	float lifeRemaining;
	// Use this for initialization
	void Start () {
		init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag =="sword"||collider.gameObject.tag =="bullet"){
			getHit(TopDownCharacterController.GetEffectiveAttackStrength());
			Destroy(collider.gameObject);// Bullet explodes sword ... bounces off?
		}
    }
    void init(){
    	if(this.gameObject.tag=="enemyCrow"){
    		 lifeRemaining = 1;
		 }else if(this.gameObject.tag=="enemyNoxiousCrawler"){
    		 lifeRemaining = 3;
		 }else{
		 	lifeRemaining = 1;//default val
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
}
