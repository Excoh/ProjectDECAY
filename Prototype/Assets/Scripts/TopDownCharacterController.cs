using UnityEngine;
using System.Collections;

public class TopDownCharacterController : MonoBehaviour {

	float mPlayerRotation;
	static float mBaseSpeed;
	static float mSpeedBonus;
	Vector2 unitVelo;
	enum WeaponModes: int{sword, gun}
	int numOfWeaponModes =2;
	static WeaponModes currentWeapon;
	static float baseAttackStrength = 1;


	static float noise = 2.0f;

	//
	//Get Hit Vars Below:
	//
	bool hurt;
	static float lifeRemaining;
	float hurtStateTime;
	float hurtStateTimeRemaining;
	MeshRenderer MRtoMessWith;
	static float inDamageRatio;

	// Use this for initialization
	void Start () {
		mSpeedBonus = 1;
		mPlayerRotation= 0;
		mBaseSpeed = 20;
		unitVelo = Vector2.zero;
		currentWeapon = WeaponModes.sword;
		MRtoMessWith = this.gameObject.GetComponent<MeshRenderer>();
	//
	//Get Hit Vars Below:
	//
		hurt = false;
		lifeRemaining = 5f;
		hurtStateTime = 2;
		inDamageRatio = 1f;
		hurtStateTimeRemaining = hurtStateTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(hurt){
			countdownHurtTime();
		}
		Control();
		Move();
		Weapons();
	}



	void Control(){
		unitVelo = Vector2.zero;
		if (Input.GetKey(KeyCode.W)){
			unitVelo.y+=1;
		}
		if (Input.GetKey(KeyCode.A)){
			unitVelo.x-=1;
		}
		if (Input.GetKey(KeyCode.S)){
			unitVelo.y-=1;
		}
		if (Input.GetKey(KeyCode.D)){
			unitVelo.x+=1;
		}
		unitVelo.Normalize();
	}

	void Move(){
		Vector3 vec3From2 = new Vector3(getEffectiveVelo().x, 0, getEffectiveVelo().y);
		gameObject.transform.position+= vec3From2;
		// rotate based on orientation of velo
		if (vec3From2 != Vector3.zero)transform.rotation = Quaternion.LookRotation(vec3From2);
	}

	public Vector2 getEffectiveVelo(){
		return unitVelo * (mBaseSpeed +  mSpeedBonus)* Time.deltaTime; // Insert modifier list here if need be
	}

	public GameObject swordPrefab;
	public GameObject currentSword;
	public GameObject cannonBallPrefab;
	public GameObject currentCannonBall;

	void Weapons(){
		if(Input.GetKeyUp(KeyCode.Tab)){
			if(currentWeapon== WeaponModes.sword){
				currentWeapon = WeaponModes.gun;
			}else{
				currentWeapon = WeaponModes.sword;
			}
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			if(currentWeapon==WeaponModes.sword)sword();
			if(currentWeapon==WeaponModes.gun)cannon();
		}
	}

	void sword(){
		if(currentSword!=null) Destroy(currentSword);
		currentSword = Instantiate(swordPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		currentSword.transform.parent = gameObject.transform;
		currentSword.transform.localPosition += new Vector3(2,0.5f,0);
	}


	void cannon(){
		currentCannonBall = Instantiate(cannonBallPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		currentCannonBall.transform.parent = gameObject.transform;
		currentCannonBall.transform.localPosition += new Vector3(0,0.75f,2.5f);
		currentCannonBall.GetComponent<Rigidbody>().velocity = (gameObject.transform.forward+(Vector3.up *0.05f))* 40; 
		currentCannonBall.transform.parent = null;
	}

	public static void SetNoise(float newNoise)
	{
		noise = newNoise;
	}

	public static float GetNoise()
	{
		return noise;
	}

	/*Set a new max speed for player*/
	public static void SetSpeedBonus(float newBonus)
	{
		mSpeedBonus = newBonus;
	}

	public static void SetBaseAttackStrength(float newStrength)
	{
		baseAttackStrength = newStrength;
	}

	public static float GetEffectiveAttackStrength()
	{
		float outVal = baseAttackStrength;
		if(currentWeapon == WeaponModes.gun){
			outVal-=0.5f;
		}else if(currentWeapon == WeaponModes.sword){
			outVal+=2;
		}
		return outVal;
	}

	
	public static void SetInDamageRatio(float newInDamageRatio)
	{
		inDamageRatio = newInDamageRatio;
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

	const float mEnemcyGenericInDamage = 1.0f;
	const float mEnemcyCrowInDamage = 0.5f;
	const float mEnemcyCarnivoreInDamage = 1.0f;
	const float mEnemcyNoxiousCrawlerInDamage = 1.0f;
	float effectiveDamageTabulation(Collider collider){
		float retVal = 0;
		if(collider.gameObject.tag =="enemy") retVal=mEnemcyGenericInDamage;
		if(collider.gameObject.tag =="enemyCrow") retVal=mEnemcyCrowInDamage;
		if(collider.gameObject.tag == "carnivore") retVal=mEnemcyCarnivoreInDamage;
		if(collider.gameObject.tag == "enemyNoxiousCrawler"){
			retVal=mEnemcyNoxiousCrawlerInDamage;
			collider.gameObject.GetComponent<NoxiousCrawlerEnemyScript>().reproduce();
			Destroy(collider.gameObject);
		}
		return retVal * inDamageRatio;
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
