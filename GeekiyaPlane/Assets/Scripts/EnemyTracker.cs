using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour {

	public float Speed;
	public int LookSpeed;
	public float TimeTillTrack;
	public float Timer;
	public float DistanceTillStopLooking;
	public float CalculatedDistance;
	public Vector3 Target;
	public Quaternion targetRotation;
	public GameObject FoundTargetObject;
	public bool stopTurning;

	private GameObject closetMissle;
	public string searchTag;
	private Transform target;
	public float targetRange = 1000;

	// Use this for initialization
	void Start () {
		
		closetMissle = FindClosestEnemy ();

		if (closetMissle) {
			target = closetMissle.transform;

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer += Time.deltaTime;

		if (closetMissle == null) {
			closetMissle = FindClosestEnemy ();

		}


		if (closetMissle != null) {
			target = closetMissle.transform;
			float CalculatedDistance = Vector3.Distance (this.transform.position, target.position);
			//give the missile speed
			transform.Translate (0, 0, Speed / 100);

			//delay tracking for a certain amount of time...
			if (Timer > TimeTillTrack) {
				if (stopTurning == false) {
					transform.Translate (0, 0, Speed / 100);
					//look at the target object at speed
					//Debug.LogError("looking for us");
					targetRotation = Quaternion.LookRotation (target.position - transform.position);
					transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * LookSpeed);
				}
			}
			//set up instances that the missile will die...
			if (CalculatedDistance < DistanceTillStopLooking) {

				stopTurning = true;

			}
		}

		
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
			if(searchTag == "Player")
				if (curDistance < distance) {
					//Debug.LogError ("closest enemy found");
					closest = go;
					distance = curDistance;
				}

			if (searchTag == "Enemy") {
				if (Vector3.Dot (Camera.main.transform.forward, diff) > 0) {

					if (curDistance < distance) {
						//Debug.LogError ("closest enemy found");
						closest = go;
						distance = curDistance;
					}
				}
			}

		}

		return closest;
	}

}
