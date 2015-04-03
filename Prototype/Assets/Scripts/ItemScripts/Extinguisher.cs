using UnityEngine;
using System.Collections;

/*Controls extinguisher end time and collisions
 *with fire objects*/

/*!!!NOTE: to make sure this script works (as in,
 *the player will not get hurt if the fluid touches
 *an enemy) make sure all fires and fluid are placed
 *on the "fire" layer*/

public class Extinguisher : MonoBehaviour {

	public float EXTINGUISHER_TIME = 10f;
	private float timeOfStart;

	// Use this for initialization
	void Start ()
	{
		timeOfStart = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Time.time - timeOfStart > EXTINGUISHER_TIME)
		{
			Destroy (gameObject);
		}
	}

	//destroy fire if fluid touches it
	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "fire")
		{
			Destroy(hit.gameObject);
		}
	}

}
