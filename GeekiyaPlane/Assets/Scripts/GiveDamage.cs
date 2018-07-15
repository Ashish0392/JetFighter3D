using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour {

	//public string whomeToDamage = "Enemy";
	private int damage = 10;
	public int Damage{
		set{ damage = value;}
		get{ return damage;}
	}





	void OnTriggerEnter(Collider col)
	{
		

	

		if (col.tag == "Enemy") {
			Enemy enemy = new Enemy ();
			//Debug.LogError ("enemyhit");
			enemy = col.GetComponentInParent<Enemy> ();
			enemy.DamageEnemy (damage);

			
		}
		if (col.tag == "Player") {
			Player player = new Player ();
			//Debug.LogError ("playerhit");
			player = col.GetComponentInParent<Player> ();

			player.DamagePlayer (damage);


		}

		if (col.tag == "Base") {
			Base _base = new Base ();
			//Debug.LogError ("playerhit");
			_base = col.GetComponentInParent<Base> ();

			_base.Damage (damage);


		}

	}
}
