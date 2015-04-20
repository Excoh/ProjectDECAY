using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemiesLeft : MonoBehaviour {

	public GameObject enemiesLeftGB;
	GameObject[] crowCount;
	GameObject[] grozzleCount;
	GameObject[] crawlerCount;
	int enemiesLeftCount = 0;

	Text enemiesLeftText;
	// Use this for initialization
	void Start () {
		enemiesLeftText = enemiesLeftGB.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		crowCount = GameObject.FindGameObjectsWithTag("enemyCrow");
		grozzleCount = GameObject.FindGameObjectsWithTag("enemyGrozzle");
		crawlerCount = GameObject.FindGameObjectsWithTag("enemyNoxiousCrawler");
		enemiesLeftCount = crowCount.Length + grozzleCount.Length + crawlerCount.Length;

		enemiesLeftText.text = "Enemies Left: " + enemiesLeftCount;

		if(enemiesLeftCount <= 0){
			Application.LoadLevel("Win");
		}
	}
}
