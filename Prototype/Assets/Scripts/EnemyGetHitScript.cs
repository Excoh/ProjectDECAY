using UnityEngine;
using System.Collections;

public class EnemyGetHitScript : MonoBehaviour {

	public float lifeRemaining;
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
		}
    }
    void init(){
    	if(this.gameObject.tag=="enemyCrow"){
    		 lifeRemaining = 1;
		 }else{
		 	lifeRemaining = 1;//default val
		 }
    }

	void getHit(float damage){
		lifeRemaining-=damage;
		if(lifeRemaining<=0){
			Destroy(this.gameObject);
		}
	}
}
