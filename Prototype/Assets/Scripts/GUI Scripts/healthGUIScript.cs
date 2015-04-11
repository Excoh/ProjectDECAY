using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthGUIScript : MonoBehaviour {
	public static int maxHearts;
	public Sprite heartImage;
	//this is to make the hearts black when health is lost
	public Material imageShader;
	//this will store all of the heart objects
	private static GameObject[] heartsArray;
	//the current health
	private static int currentHearts;
	//this is the first heart.  It is used to base all the other hearts off of
	private Image firstHeartImage;
	//this is the space between each heart, more means more space inbetween
	private float heartSpacing = 10;
	//this is the black that's used to color inactive hearts
	private static Color inactiveHeartColor = new Color(0,0,0,1);

	// Use this for initialization
	void Start () {
		maxHearts = (int)TopDownCharacterController.maxLife;
		//set current hearts to maximum hearts
		currentHearts = maxHearts;
		//This is for GUI, not calculating health
		heartsArray = new GameObject[maxHearts];
		//get the first heart image set up as a basis for all the others
		firstHeartImage = transform.GetChild (0).GetComponent<Image>();
		heartsArray [0] = transform.GetChild (0).gameObject;
		heartsArray [0].GetComponent<Image> ().sprite = heartImage;
		heartsArray [0].GetComponent<Image> ().material = new Material (imageShader);
		//put the first heart at the top left of the screen
		transform.localPosition = new Vector3 ((-transform.parent.GetComponent<RectTransform> ().rect.width/2 + GetComponent<RectTransform> ().rect.width / 2),
		                                       transform.parent.GetComponent<RectTransform> ().rect.height/2 - GetComponent<RectTransform>().rect.height/2, 0);

		//now set up all the other hearts based off of the first heart
		for (int i = 1; i < maxHearts; i++) {
			heartsArray[i] = (GameObject) Instantiate (transform.GetChild (0).gameObject);
			heartsArray[i].transform.SetParent (transform);
			heartsArray[i].GetComponent<Image>().sprite = heartImage;
			//this step is so that all the hearts don't all go black together but rather independently
			heartsArray[i].GetComponent<Image>().material = new Material(imageShader);
			//this step puts each sequential heart spaced to the right of the initial heart scaling with incremental i
			heartsArray[i].GetComponent<Image>().rectTransform.position = 
				new Vector3(firstHeartImage.rectTransform.position.x + (i * (firstHeartImage.rectTransform.rect.width + heartSpacing)),
			                                                                          firstHeartImage.rectTransform.position.y, 0);
		}
	}

	// Update is called once per frame
	void Update () {
		//---------------------------------------------------------------------------------------------
		//This stuff is just for the debuggin/demonstration, it should be deleted before implementation
		if(Input.GetKeyDown (KeyCode.J)){
			modifyHearts (-1);
		}
		if(Input.GetKeyDown (KeyCode.K)){
			modifyHearts (1);
		}
		//---------------------------------------------------------------------------------------------
	}

	//this function checks hearts with the currentHearts value to see which are active/inactive
	private static void updateHearts(){
		for (int i = 0; i < maxHearts; i++) {
			if(i > currentHearts - 1){
				//set the heart as black since it's innactive
				heartsArray[i].GetComponent<Image>().material.color = inactiveHeartColor;
			}
			else{
				//set the heart as its original color, as it's active
				heartsArray[i].GetComponent<Image>().material.color = new Color(1,1,1,1);
			}
		}
	}

	//this function changes the current GUIHearts.  Negative input means the character took damage
	public static void modifyHearts(int modification){
		setHearts(currentHearts + modification );
	}

	//this function set the current GUIHearts directly.
	public static void setHearts(int modification){
		//change the current GUIHearts
		currentHearts = modification;
		//make sure it doesn't go out of bounds
		if (currentHearts <= 0) {
			currentHearts = 0;
		}
		if (currentHearts > maxHearts){
			currentHearts = maxHearts;
		}
		//update the GUI to reflect the new Hearts
		updateHearts ();
	}






}