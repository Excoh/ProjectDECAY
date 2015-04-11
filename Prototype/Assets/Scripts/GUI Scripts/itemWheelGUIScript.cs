using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Defines how a single itemWheel object should behave
public class itemWheelGUIScript : MonoBehaviour {
	//the color of available items
	Color availableColor = new Color(1, 1, 1, 1);
	public Material imageShader;
	//array of the images
	public Sprite[] imagesArray;

	//the ranged Text objects in the GUI
	Image negativeTwoAway;
	Image negativeOneAway;
	Image equipped;
	Image positiveOneAway;
	Image positiveTwoAway;

	//the integer representing the currently selected item and the array of the values around it
	int selected;
	int[] selectedArray = new int[5];

	//array of each item's availability
	//------------------------------------------------------------------------------------------------
	//this array is public for demonstration/debuggin purposes, change it to private on implementation
	public int[] availableArray;
	//------------------------------------------------------------------------------------------------

	//Use this for initialization
	void Start(){
		//instantiate the array of item availability
		//--------------------------------------------------------------------------------------------------
		//this is currently a comment for demonstration purposes, change it to a statement on implementation
		//availableArray = new int[imagesArray.Length];
		//--------------------------------------------------------------------------------------------------

		//get the text components
		negativeTwoAway = transform.GetChild (0).gameObject.GetComponent<Image>();
		negativeOneAway = transform.GetChild (1).gameObject.GetComponent<Image>();
		equipped = transform.GetChild (2).gameObject.GetComponent<Image>();
		positiveOneAway = transform.GetChild (3).gameObject.GetComponent<Image>();
		positiveTwoAway = transform.GetChild (4).gameObject.GetComponent<Image>();

		//initialize each unequipped spot with its own shader
		negativeTwoAway.material = new Material(imageShader);
		negativeOneAway.material = new Material(imageShader);
		equipped.material = new Material(imageShader);
		positiveOneAway.material = new Material(imageShader);
		positiveTwoAway.material = new Material(imageShader);

		//initialize selected to default
		int selected = 0;

		//now an initial drawing of the items so that the GUI starts up
		initializeItemGUI ();
	}

	//Update is called once per frame
	void Update(){
		//check for toggling through items every update
		itemScroll ();
	}

	//call this from anywhere to update the GUI with the current values for item availability
	//this function allows updating of only one item at a time
	public void updateItem(int index, int newValue){
		//update index with the new availability
		availableArray [index] += newValue;
		//since items have been update, reevaluate the current selectedArray
		displayItems ();
	}

	//Scrolls through items
	void itemScroll(){
		if (Input.GetKeyDown (KeyCode.RightArrow)){ //scrolling right
			selected++;
			//wrap selected around
			if (selected > imagesArray.Length - 1){
				selected = 0;
			}
			//update the selectedArray
			for (int i = -2; i < 3; i++) {
				selectedArray[i + 2] = selected + i;
			}
			//wrap the selectedArray
			for(int i = 0; i < 5; i++){
				if(selectedArray[i] > imagesArray.Length - 1)
					selectedArray[i] -= imagesArray.Length;
				if(selectedArray[i] < 0)
					selectedArray[i] += imagesArray.Length;
			}
			//update the displayed text to be in line with the selectedArray
			displayItems();
		} 
		if (Input.GetKeyDown (KeyCode.LeftArrow)){ //scrolling left
			selected --;
			//wrap selected around
			if (selected < 0){
				selected = imagesArray.Length - 1;
			}
			//update the selectedArray
			for (int i = -2; i < 3; i++) {
				selectedArray[i + 2] = selected + i;
			}
			//wrap the selectedArray
			for(int i = 0; i < 5; i++){
				if(selectedArray[i] > imagesArray.Length - 1)
					selectedArray[i] -= imagesArray.Length;
				if(selectedArray[i] < 0)
					selectedArray[i] += imagesArray.Length;
			}
			//update the displayed text to be in line with the selectedArray
			displayItems();
		}	
	}

	public void initializeItemGUI(){
		//fill the text components with the proper image
		negativeTwoAway.sprite = imagesArray[selectedArray[0]];	//two left clicks away
		negativeOneAway.sprite = imagesArray[selectedArray[1]];	//one left click away
		equipped.sprite = imagesArray[selectedArray[2]];		//currently selected
		positiveOneAway.sprite = imagesArray[selectedArray[3]];	//one right click away
		positiveTwoAway.sprite = imagesArray[selectedArray[4]];	//two right clicks away

		//make all of the items start dissapeared except for the currently selected one
		negativeTwoAway.material.color = Color.clear;
		negativeOneAway.material.color = Color.clear;
		positiveOneAway.material.color = Color.clear;
		positiveTwoAway.material.color = Color.clear;
	}

	//this function returns the remaining ammo of a specific item.  this function will mainly
	//be used by the ammo text portion of the GUI
	public int getAmmo(){
		return availableArray [selected];
	}

	void displayItems(){
		//fill the text components with the proper image
		negativeTwoAway.sprite = imagesArray[selectedArray[0]];	//two left clicks away
		negativeOneAway.sprite = imagesArray[selectedArray[1]];	//one left click away
		equipped.sprite = imagesArray[selectedArray[2]];		//currently selected
		positiveOneAway.sprite = imagesArray[selectedArray[3]];	//one right click away
		positiveTwoAway.sprite = imagesArray[selectedArray[4]];	//two right clicks away
		//if you're already fading, cut the fade so the new one can start
		StopCoroutine ("fadeItems");
		//start the new fade out
		StartCoroutine ("fadeItems");
	}
	
	IEnumerator fadeItems(){
		//these are temp colors used to set the color of the text
		//tempAvailable is the default, but if an item is used up, it will be greyed out
		//tempUnavailable will be the same alpha/fade Available, but will be grey
		Color tempAvailable = new Color (availableColor.r, availableColor.g, availableColor.b, 1);
		Color tempUnavailable = new Color (Color.black.r, Color.black.g, Color.black.b, 1);
		
		//do an initial render of the images and let them sit for a bit
		if (availableArray [selectedArray[0]] > 0) {
			negativeTwoAway.material.color = tempAvailable;
		} else {
			negativeTwoAway.material.color = tempUnavailable;
		}
		if (availableArray [selectedArray [1]] > 0) {
			negativeOneAway.material.color = tempAvailable;
		} else {
			negativeOneAway.material.color = tempUnavailable;
		}
		if (availableArray [selectedArray[2]] > 0) {
			equipped.material.color = availableColor;
		} else {
			equipped.material.color = new Color(Color.black.r, Color.black.g, Color.black.b, 1);
		}
		if (availableArray [selectedArray [3]] > 0) {
			positiveOneAway.material.color = tempAvailable;
		} else {
			positiveOneAway.material.color = tempUnavailable;
		}
		if (availableArray [selectedArray [4]] > 0) {
			positiveTwoAway.material.color = tempAvailable;
		} else {
			positiveTwoAway.material.color = tempUnavailable;
		}
		//here's the waiting
		yield return new WaitForSeconds (1f);
		//now that the user has had time to see the images, make them fade
		for (float i = 1f; i > .0001f; i -= 2 * Time.deltaTime) {
			//set each of these colors to decrease in alpha over time due to i incrementind downward
			tempAvailable = new Color (availableColor.r, availableColor.g, availableColor.b, i);
			tempUnavailable = new Color (Color.black.r, Color.black.g, Color.black.b, i);
			
			//set each text's color to the approriate new temp color 
			//depending on if the item's available or not
			if (availableArray [selectedArray[0]] > 0) {
				negativeTwoAway.material.color = tempAvailable;
			} else {
				negativeTwoAway.material.color = tempUnavailable;
			}
			if (availableArray [selectedArray [1]] > 0) {
				negativeOneAway.material.color = tempAvailable;
			} else {
				negativeOneAway.material.color = tempUnavailable;
			}
			if (availableArray [selectedArray[2]] > 0) {
				equipped.material.color = availableColor;
			} else {
				equipped.material.color = new Color(Color.black.r, Color.black.g, Color.black.b, 1);
			}
			if (availableArray [selectedArray [3]] > 0) {
				positiveOneAway.material.color = tempAvailable;
			} else {
				positiveOneAway.material.color = tempUnavailable;
			}
			if (availableArray [selectedArray [4]] > 0) {
				positiveTwoAway.material.color = tempAvailable;
			} else {
				positiveTwoAway.material.color = tempUnavailable;
			}
			yield return new WaitForSeconds(.001f);
		}
		
		//after the text has faded, make it dissapear altogether
		//set each text's color to be transparent
		negativeTwoAway.material.color = Color.clear;
		negativeOneAway.material.color = Color.clear;
		positiveOneAway.material.color = Color.clear;
		positiveTwoAway.material.color = Color.clear;
		yield return null;
	}
}