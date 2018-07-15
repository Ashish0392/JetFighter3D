using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile01 : MonoBehaviour {

	public string searchTag;
	private GameObject closetMissle;
	private Transform target;
	public GameObject missileExpObject;

	void Start()
	{
		closetMissle = FindClosestEnemy ();

		if (closetMissle)
			target = closetMissle.transform;
	}

	void Update()
	{
		transform.LookAt (target);
		transform.Translate (Vector3.forward * 1.0f * Time.deltaTime);
	}

	GameObject FindClosestEnemy()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag (searchTag);

		GameObject closest = null;

		float distance = Mathf.Infinity;

		Vector3 position = transform.position;

		foreach (GameObject go in gos) {

			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}

		return closest;
	}

	void OnTriggerEnter(Collider otherObject)
	{
		if (otherObject.tag == "Enemy" || otherObject.tag == "SolidGround" ) {

			Instantiate (missileExpObject, this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
		}
	}
}
