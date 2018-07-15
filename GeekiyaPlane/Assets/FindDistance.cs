using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDistance : MonoBehaviour {

	public Transform secondpoint;

	void Start () {

		InvokeRepeating ("Distance", 1, 1);
	}
	

	void Update () {
		
	}

	void Distance()
	{

		Vector3 position = transform.position;

		Vector3 diff = secondpoint.transform.position - position;

		float distance = diff.sqrMagnitude/100f;

		Debug.LogError (distance);
		
	}
}
