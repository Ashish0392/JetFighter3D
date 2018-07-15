using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class turnColliderOn : MonoBehaviour {

	public float afterSeconds;

	// Use this for initialization
	void Start () {
		Invoke ("Trigger", afterSeconds);
	}
	
	void Trigger()
	{
	
		transform.GetComponent<Collider> ().enabled = true ;
		transform.GetComponentInChildren<Collider> ().enabled = true;

	}
}
