using UnityEngine;
using System.Collections;

public class DestroyAircraft : MonoBehaviour {

	public GameObject explosion;


	void OnTriggerEnter(Collider impactObject)
	{
		
		if (impactObject.tag == "SolidGround" ) {

			Debug.LogError ("Hey onTriggerEntered!");


			Instantiate (explosion, transform.position, transform.rotation);
		    Destroy (transform.parent.parent.gameObject);


		}


	}

} 