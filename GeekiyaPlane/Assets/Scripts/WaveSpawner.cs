using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	public enum SpawnState { SPAWNING, WAITING, COUNTING };

	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy01;
		public Transform enemy02;
		public Transform enemy03;
		public int enemy01Count;
		public int enemy02Count;
		public int enemy03Count;
		public float rate;
	}

	public Wave wave;

	private int waveCount = 0;

	public int NextWave
	{
		get { return waveCount + 1; }
	}

	public Transform[] spawnPoints;

	public float timeBetweenWaves = 5f;
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private bool menuBool = false;
	private float searchCountdown = 1f;

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

	void Start()
	{
		if (spawnPoints.Length == 0)
		{
			Debug.LogError("No spawn points referenced.");
		}

		waveCountdown = timeBetweenWaves;

		GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;
	}

	void Update()
	{
		if (!menuBool) {
			
			if (state == SpawnState.WAITING) {
				//Debug.Log ("waiting");
				//Debug.Log ("Enemy" + EnemyIsAlive());
				if (!EnemyIsAlive ()) {

					WaveCompleted ();
				} else {
					return;
				}
			}

			if (WaveCountdown <= 0) {
				if (state != SpawnState.SPAWNING) {
				
					StartCoroutine (SpawnWave (wave));
				}
			} else {
				waveCountdown -= Time.deltaTime;
				//Debug.Log (WaveCountdown);
			}
		}
	}

	void OnUpgradeMenuToggle (bool active)
	{
		//handle what happens when upgradeMenu toggled

		menuBool = active;

	}


	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		waveCount += 1;

	}

	bool EnemyIsAlive()
	{

		searchCountdown -= Time.deltaTime;



		if (searchCountdown <= 0f)
		{
			//Debug.Log (searchCountdown);
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null)
			{

				return false;
			}

		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		//		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;
		//_wave.enemy01.gameObject.GetComponent<Enemy> ().UpgradeEnemy (waveCount);


		for (int i = 0; i < _wave.enemy01Count; i++)
		{
			SpawnEnemy(_wave.enemy01);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}

		for (int i = 0; i < _wave.enemy02Count; i++)
		{
			SpawnEnemy(_wave.enemy02);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}

		for (int i = 0; i < _wave.enemy03Count; i++)
		{
			SpawnEnemy(_wave.enemy03);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		//Debug.Log("Spawning Enemy: " + _enemy.name);



		Transform _sp = spawnPoints[ Random.Range (0, spawnPoints.Length) ];
		Instantiate(_enemy, _sp.position, _sp.rotation);

	}

	void OnDestroy()
	{

		GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
	}
}
