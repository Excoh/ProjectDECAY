using UnityEngine;
using System.Collections;

public class SimpleEnemyScript : MonoBehaviour {

	GameObject characterObject;
	private GameObject meatToFollow;
	private GameObject flareToFollow;

	bool canMove = true;

	//ENEMY TYPES: "common", "carnivore", "blind", "mutated"
	string enemyType;

	// Use this for initialization
	void Start () {
		characterObject = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (canMove && Vector3.Distance (transform.position, characterObject.transform.position) <= TopDownCharacterController.GetNoise ()*50f) {
						meatToFollow = GameObject.FindGameObjectWithTag ("meat");
						flareToFollow = GameObject.FindGameObjectWithTag ("flare");
						if (characterObject != null && flareToFollow == null && (meatToFollow == null || enemyType != "carnivore")) {
								transform.position = Vector3.MoveTowards (transform.position, characterObject.transform.position, 12 * Time.deltaTime);
						} else if (characterObject != null && flareToFollow != null) {
								transform.position = Vector3.MoveTowards (transform.position, flareToFollow.transform.position, 12 * Time.deltaTime);
						} else if (characterObject != null && enemyType == "carnivore" && meatToFollow != null) {
								transform.position = Vector3.MoveTowards (transform.position, meatToFollow.transform.position, 12 * Time.deltaTime);
						}
				}

		}

	public string GetEnemyType()
	{
		return enemyType;
	}

	public void SetCanMove(bool move)
	{
		canMove = move;
	}
}

