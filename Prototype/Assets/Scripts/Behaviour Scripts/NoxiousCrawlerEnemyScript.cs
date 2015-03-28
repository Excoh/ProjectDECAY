using UnityEngine;
using System.Collections;

public class NoxiousCrawlerEnemyScript : MonoBehaviour {
	enum eBehaviourState {pursue, wait};
	bool isMiniCrawler = false;
	eBehaviourState currentState;
	// Use this for initialization
	public void setMiniCrawlerStatus(bool newVal){
		isMiniCrawler = newVal;
	}
	void Start () {
		playerObject = GameObject.FindWithTag("Player");
		currentState = eBehaviourState.wait;
		reportStateChange();

	}
	public GameObject playerObject;
	// Update is called once per frame
	void Update () {
		if(playerObject!=null)distBetween = 
		Vector3.Distance(playerObject.transform.position, gameObject.transform.position);
		switch(currentState){
			case eBehaviourState.pursue:
				pursue();
				break;
			case eBehaviourState.wait:
				wait();
				break;
		}
	
	}

	float aggroHorizon = 70;
	float distBetween = 500;

	void pursue(){
		if(playerObject!=null){
			transform.position = Vector3.MoveTowards(transform.position, playerObject.transform.position, 15 * Time.deltaTime);
		}
	}

	void wait(){
		//check aggro horizon vs distance and hope player comes into range
		//Debug.Log(distBetween);
		if(playerObject!=null && aggroHorizon>distBetween){
			currentState = eBehaviourState.pursue;
			reportStateChange();
		}
	}

	public void reproduce(){
		GameObject hijo;
		if(!isMiniCrawler){
			hijo = (GameObject)Instantiate(gameObject, gameObject.transform.position+ (new Vector3(2.6f,0f,0f)), gameObject.transform.rotation);
			hijo.GetComponent<NoxiousCrawlerEnemyScript>().setMiniCrawlerStatus(true);
			hijo.transform.localScale *=0.5f;
			//
			hijo = (GameObject)Instantiate(gameObject, gameObject.transform.position+ (new Vector3(-2.6f,0f,0f)), gameObject.transform.rotation);
			hijo.GetComponent<NoxiousCrawlerEnemyScript>().setMiniCrawlerStatus(true);
			hijo.transform.localScale *=0.5f;
		}
		currentState = eBehaviourState.wait;
		reportStateChange();
	}

	void reportStateChange(){
		Debug.Log("The current state is now: " + currentState);
	}
}
