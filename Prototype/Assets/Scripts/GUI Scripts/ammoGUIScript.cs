using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ammoGUIScript : MonoBehaviour {
	Text ammoText;
	public Vector2 spacingFromTopLeft;
	public itemWheelGUIScript itemScript;

	// Use this for initialization
	void Start () {
		//this is to put the ammo text in the top left corner
		//use "spacingFromTopLeft" in the editor window to adjust its position
		transform.localPosition = new Vector3 ((
			-transform.parent.GetComponent<RectTransform> ().rect.width/2 + transform.GetChild(0).GetComponent<RectTransform> ().rect.width / 2) + spacingFromTopLeft.x,
		    transform.parent.GetComponent<RectTransform> ().rect.height/2 - transform.GetChild(0).GetComponent<RectTransform>().rect.height/2 - spacingFromTopLeft.y, 0);
	
		//get the text component
		ammoText = transform.GetChild (0).GetComponent<Text> ();
		//set the initial text of the text component to the proper value
		ammoText.text = "Ammo: " + itemScript.getAmmo ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//if the currently selected item has been changed, update the current item ammo
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow))
			ammoText.text = "Ammo: " + itemScript.getAmmo ();
	}
}
