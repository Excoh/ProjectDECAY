using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

	public GameObject wall;
	public GameObject crow;
	public GameObject grozzle;
	public GameObject crawler;
	public GameObject walls;
	public GameObject enemies;
	public GameObject hazard;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject createWall(){
		return wall;
	}

	public GameObject createCrow(){
		return crow;
	}

	public GameObject createGrozzle(){
		return grozzle;
	}

	public GameObject createCrawler(){
		return crawler;
	}

	public GameObject createWalls(){
		return walls;
	}

	public GameObject createEnemies(){
		return enemies;
	}

	public GameObject createHazard(){
		return hazard;
	}
	
}
