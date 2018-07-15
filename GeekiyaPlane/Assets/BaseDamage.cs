using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamage : MonoBehaviour {

	private Base _base;
	// Use this for initialization
	void Start () {

		_base = GetComponentInParent<Base>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider col){

		Debug.Log (col.tag);
		if (col.tag == "Enemy") {

			Enemy enemy = new Enemy ();



			enemy = col.GetComponentInParent<Enemy> ();

			_base.Damage (enemy.buildingDamage);
			Debug.Log (enemy.buildingDamage);
			enemy.moneyDrop = 0;
			enemy.scoreDrop = 0;
			enemy.DamageEnemy (1000);


		}

		if (col.tag == "Player") {

			Player enemy = new Player ();


			_base.Damage (500);
			enemy = col.GetComponentInParent<Player> ();


			enemy.DamagePlayer (1000);


		}


	}
}
