using UnityEngine;
using System.Collections;

public class TopDownCameraScript : MonoBehaviour {

	public GameObject character;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(character.transform.position.x, this.transform.position.y,character.transform.position.z),Time.deltaTime * 3f);
	}
}
