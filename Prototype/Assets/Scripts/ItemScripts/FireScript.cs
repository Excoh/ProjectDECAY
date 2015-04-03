using UnityEngine;
using System.Collections;

/*Fire is destroyed if it hits fluid*/




/*THIS SCRIPT IS NO LONGER BEING USED!!!!*/






public class FireScript : MonoBehaviour {

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "fluid")
		{
			Destroy(gameObject);
		}
	}
}
