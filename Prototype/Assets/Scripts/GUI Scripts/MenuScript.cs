using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public void PlayGame(){
		Application.LoadLevel("Game");
	}

	public void QuitGame(){
		Application.Quit();
	}
}
