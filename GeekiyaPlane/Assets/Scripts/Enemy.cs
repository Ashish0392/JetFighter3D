using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject explosion;

	public WaveSpawner spawner;


	[System.Serializable]
	public class EnemyStats {
		public int maxHealth = 100;
		public float healthRegenRate = 5f;
		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth;}
			set {_curHealth = Mathf.Clamp (value, 0, maxHealth);}
		}

		public void Init()
		{
			curHealth = maxHealth;
		}
	}
	
	public EnemyStats stats = new EnemyStats();

	public int moneyDrop = 50;

	public int scoreDrop = 20;
	public int scoreDropIncrement = 2;

	public int healthIncrement = 10;

	public int buildingDamage = 100; 

	[SerializeField]
	private StatusIndicator statusIndicator;

	void Start()
	{
		
		stats.Init ();

		spawner = GameMaster.gm.GetComponent<WaveSpawner> ();

		if (statusIndicator != null) {
			statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);
		}

		GameMaster.gm.onToggleUpgradeMenu += onUpgradeMenuToggle; 

		InvokeRepeating ("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);

		this.UpgradeEnemy (spawner.NextWave - 1);

	}

	void onUpgradeMenuToggle(bool active){

		this.GetComponentInParent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition|RigidbodyConstraints.FreezeRotation;

		if (!active) {

			this.GetComponentInParent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}

	void OnDestroy()
	{

		GameMaster.gm.onToggleUpgradeMenu -= onUpgradeMenuToggle;
	}


	public void DamageEnemy (int damage) {
		Debug.Log("Damage done" + damage);
		stats.curHealth -= damage;
		//Debug.Log("curhealth" + stats.curHealth);
		if (statusIndicator != null) {
			statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);
		}

		if (stats.curHealth <= 0)
		{
			//Debug.LogError ("EnemyKilled");
			Instantiate (explosion, transform.position, transform.rotation);
			GameMaster.KillEnemy(this);

		}
	}

	void RegenHealth()
	{
		stats.curHealth += 1;

		statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);

	}

	public void UpgradeEnemy(int waveCount){

		Debug.LogError (waveCount);

		Debug.LogError (stats.maxHealth);

		for(int i = 0; i < waveCount; i++)
		{
			
		scoreDrop += scoreDropIncrement;
		stats.maxHealth += healthIncrement;

		}
		Debug.LogError (stats.maxHealth);
	}
}
