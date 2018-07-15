using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyself : MonoBehaviour {

	public float timeUntillDestruction;
	public Transform blast;

	// Use this for initialization
	void Start () {

		Invoke ("SelfDestruct", timeUntillDestruction);
		
	}


	void OnTriggerEnter(Collider col){
		if (col.tag == "Enemy" || col.tag == "SolidGround") {
			SelfDestruct ();
		}
	}

	void SelfDestruct()
	{
		if (blast != null) {
			Destroy (gameObject);
			Instantiate (blast, transform.position, transform.rotation);
		} else
			Destroy (gameObject);
	}
}
