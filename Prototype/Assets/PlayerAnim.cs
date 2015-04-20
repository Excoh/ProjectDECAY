using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		anim.SetFloat("SpeedX", inputX);
		anim.SetFloat("SpeedY", inputY);

	}

	void FixedUpdate(){
		float lastInputX = Input.GetAxis("Horizontal");
		float lastInputY = Input.GetAxis("Vertical");
		if(lastInputX != 0 || lastInputY != 0){
			anim.SetBool("walking", true);
		}
		else{
			anim.SetBool("walking", false);
		}
	}
}
