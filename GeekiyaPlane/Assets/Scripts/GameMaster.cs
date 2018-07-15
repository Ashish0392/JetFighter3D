using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	[SerializeField]
	private int maxLifes = 2;

	private static int _remainingLives = 3;
	public static int RemainingLives
	{
		get{ return _remainingLives;}
		set{ _remainingLives = value;}
	}

	public int money = 100;
	public static int highScore = 0;
	public int score = 0;
	public float scoreRate = 1.2f;


	public Transform gameOverUI;



	public string text;

	[SerializeField]
	private GameObject upgradeMenu;

	[SerializeField]
	private WaveSpawner wavespawner;

	public string upgradeButton;

	public PlayerStats playerStats;

	public delegate void UpgradeMenuCallback(bool active);
	public UpgradeMenuCallback onToggleUpgradeMenu;

	[SerializeField]
	private Text moneyCounter;

	[SerializeField]
	private Text scoreCounter;
	[SerializeField]
	private Text scoreGameOverCounter;

	[SerializeField]
	private Text highScoreCounter;

	public Transform playerPrefab;
	public Transform playerSpawnPoint;
	public Transform respawnPrefab;
	public Transform respawnPoint;
	public int spawnDelay = 3;
	public bool revived = false;


	void Awake(){

		SpawnPlayer ();

		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();

		}
		if (gameOverUI == null) {
			gameOverUI = GameObject.FindGameObjectWithTag ("GameOverUI").GetComponent<Transform>();

		}


	}


	void Start(){

		InvokeRepeating ("IncreaseScore", 1f/gm.scoreRate, 1f/gm.scoreRate);

		_remainingLives = maxLifes;
		upgradeMenu.SetActive (false);
		gm.revived = false;

		moneyCounter.text = "Money :" + gm.money.ToString ();
		gm.scoreCounter.text = "Score:" + gm.score.ToString ();
		gm.highScoreCounter.text = "HighScore" + PlayerPrefs.GetInt ("HighScore" , 0).ToString();


	}

	void FixedUpdate(){

		gm.moneyCounter.text = "Money :" + gm.money.ToString ();

		/*if (CrossPlatformInputManager.GetButton (upgradeButton)) {

			ToggleUpgradeMenu ();
		}*/
	}

	public void IncreaseScore()
	{
		gm.score += 1;

		gm.scoreCounter.text = "Score:" + gm.score.ToString ();

	}


	public void ToggleUpgradeMenu(){

		upgradeMenu.SetActive ( !upgradeMenu.activeSelf );
		//wavespawner.enabled = !upgradeMenu.activeSelf;
		onToggleUpgradeMenu.Invoke (upgradeMenu.activeSelf);
	}


	public void EndGame(){
	//Debug.Log("GameOver");
	
		gm.CancelInvoke ("IncreaseScore");
	

		if (gm.score > PlayerPrefs.GetInt("HighScore", 0)) {


			PlayerPrefs.SetInt("HighScore", gm.score);
			gm.highScoreCounter.text = " HighScore : " + gm.score.ToString();

		
		}


		gm.scoreGameOverCounter.text = "YourScore:" + gm.score.ToString ();

		gameOverUI.gameObject.SetActive (true);

	}



	public IEnumerator RespawnPlayer()
	{
		yield return new WaitForSeconds (spawnDelay);
		SpawnPlayer ();

		//Transform clone = Instantiate (respawnPrefab, respawnPoint.position, respawnPoint.rotation);
		//Instantiate (playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

		//Destroy (clone.gameObject, 5f);
	}

	public void SpawnPlayer(){
		

		Transform clone = Instantiate (respawnPrefab, respawnPoint.position, respawnPoint.rotation);
		Instantiate (playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
		Destroy (clone.gameObject, 5f);

	
		
	}

	public static void KillPlayer (Player player) {



		Destroy (player.gameObject);
		_remainingLives -= 1;

		if (_remainingLives <= 0) {
			gm.EndGame ();
			
		} else {

			gm.StartCoroutine(gm.RespawnPlayer ());
		}


	}

	public static void KillEnemy(Enemy enemy)
	{
		//Debug.LogError ("reached KillEnemy GM");
		//Destroy (enemy.gameObject);

		gm._KillEnemy (enemy);
		gm.money += enemy.moneyDrop;
		gm.score += enemy.scoreDrop;
		gm.moneyCounter.text = "Money :" + gm.money.ToString ();
		gm.scoreCounter.text = "Score:" + gm.score.ToString ();

	}

	public void _KillEnemy(Enemy enemy){

		GameObject.Destroy (enemy.gameObject);
	}


	public static void KillBase(Base _base)
	{
		GameObject.Destroy (_base.gameObject);

		gm.revived = true;


		if (_remainingLives > 0) {
			_remainingLives = 0;

		}

		gm.EndGame ();





	}




}