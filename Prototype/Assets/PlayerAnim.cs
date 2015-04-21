using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

	public GameObject character;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("Horizontal");
		float inputZ = Input.GetAxis("Vertical");
		anim.SetFloat("SpeedX", inputX);
		anim.SetFloat("SpeedZ", inputZ);

	}

	void FixedUpdate(){
		float lastInputX = Input.GetAxis("Horizontal");
		float lastInputZ = Input.GetAxis("Vertical");
		if(lastInputX != 0 || lastInputZ != 0){
			anim.SetBool("walking", true);
		}
		else{
			character.transform.rotation = Quaternion.Euler(0,0,0);
			anim.SetBool("walking", false);
		}
	}
}
