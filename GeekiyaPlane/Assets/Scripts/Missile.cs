using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float Speed;

	[System.Serializable]
	public class MissileStats
	{

		public int level;
	public int damage;
	

	public int aoeDamage;
	
		public void Init(){

			damage *= level;
			aoeDamage *= level - 1;
		}
	
	}

	public MissileStats stats = new MissileStats();

	public float Timer;

	//public Transform aoeDamager;

	public bool aoeDamage = false;
	public float aoeRange = 1000f;

	public GameObject Explosion;

	public int TimeTillExpire;

	private PlayerStats playerStats;






	
	void Start (){

		playerStats = PlayerStats.instance;

		if (this.tag == "NMissile") {
			stats.level = playerStats.levelM[0];
		}

		if (this.tag == "GMissile") {
			stats.level = playerStats.levelM[1];
		}

		stats.Init ();

		this.GetComponent<Rigidbody> ().velocity = transform.forward * Speed;


}
	
	void Update (){

		//set up the timer
		Timer += Time.deltaTime;

		if (Timer > TimeTillExpire) {
			DestroyThis ();
		}

			}


	void DestroyThis(){

		if (aoeDamage) {
			DamageNearbyEnemies ();
		}
		Destroy (this.gameObject);
		Instantiate(Explosion, transform.position, transform.rotation);

	


		//Debug.LogError ("Should be dead by now");
	}


	void OnTriggerEnter(Collider otherObject)
	{
		Debug.Log ("entered" + otherObject.tag);

		if (otherObject.tag == "SolidGround" ) {
			Debug.Log (otherObject.tag);
			DestroyThis();
		}

			if (otherObject.tag == "Player" ) {
				Debug.Log ("PlayerTrigger");
				Player enemy = new Player ();
				enemy = otherObject.GetComponentInParent<Player> ();
				enemy.DamagePlayer (stats.damage);
				DestroyThis ();


			}
			if (otherObject.tag == "Enemy" ) {
			
				Debug.Log ("EnemyTrigger");

				Enemy enemy = new Enemy ();
				enemy = otherObject.GetComponentInParent<Enemy> ();
				Debug.Log (stats.damage);
				enemy.DamageEnemy (stats.damage);
				DestroyThis();

			}







	}



	void DamageNearbyEnemies()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag ("Enemy");

		float aoeDistance = aoeRange;

		Vector3 position = transform.position;

		foreach (GameObject go in gos) {

			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude / 100f;;
			//Debug.Log (go.name + curDistance);
			if (curDistance < aoeDistance ) {
				//Debug.Log (curDistance);
				Enemy _enemy = new Enemy ();
				 _enemy = go.transform.gameObject.GetComponent<Enemy> ();
				_enemy.DamageEnemy (stats.aoeDamage);

				Debug.Log ("aoeDamage" + go.name + stats.aoeDamage);

			}

		

		}

	
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, aoeRange);
	}

}


