using UnityEngine;
using System.Collections;

public class EnemySprite : MonoBehaviour {

	public GameObject topSprite;
	public GameObject bottomSprite;
	public GameObject leftSprite;
	public GameObject rightSprite;

	Vector3 prevLoc = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentLoc = (transform.position - prevLoc) / Time.deltaTime;
		if(currentLoc.x > 0){
			topSprite.SetActive(true);
			bottomSprite.SetActive(false);
			leftSprite.SetActive(false);
			rightSprite.SetActive(false);
		}
		else if(currentLoc.z < 0){
			topSprite.SetActive(false);
			bottomSprite.SetActive(false);
			leftSprite.SetActive(true);
			rightSprite.SetActive(false);
		}
		else if(currentLoc.z > 0){
			topSprite.SetActive(false);
			bottomSprite.SetActive(false);
			leftSprite.SetActive(false);
			rightSprite.SetActive(true);
		}
		//Default
		else{
			topSprite.SetActive(false);
			bottomSprite.SetActive(true);
			leftSprite.SetActive(false);
			rightSprite.SetActive(false);
		}
		prevLoc = transform.position;
	}
}
