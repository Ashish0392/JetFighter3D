using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Aeroplane;

[System.Serializable]
public class Player : MonoBehaviour {

	//public Collider hitCollider;
	public GameObject explosion;
	public GameObject damageUI;


	public Transform controlRig;

		

	[SerializeField]
	private StatusIndicator statusIndicator;

	[SerializeField]
	private Text altiMeter;

	private PlayerStats stats;

	private Player player;

	private AeroplaneController m_Plane;

	private GameObject _base;



	void Start()
	{
		stats = PlayerStats.instance;

		stats.curHealth = stats.maxHealth;

		_base =  GameObject.FindGameObjectWithTag ("Base");



		m_Plane = GetComponent<AeroplaneController> ();

		InvokeRepeating ("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);


		if (statusIndicator != null) {
			statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);
		}
		damageUI.GetComponent<Image>().enabled = false;

		GameMaster.gm.onToggleUpgradeMenu += onUpgradeMenuToggle; 




	}

	void Update(){

		altiMeter.text =" Altitude : " + Mathf.Round(m_Plane.Altitude).ToString ();

		if (m_Plane.Altitude < 0) {
			DamagePlayer (999);
		}

	}

	void FixedUpdate()
	{
		if (_base == null)
			DamagePlayer (9999);

	}



	void onUpgradeMenuToggle(bool active){
		
		 this.GetComponentInParent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition|RigidbodyConstraints.FreezeRotation;
		//this.GetComponentInChildren<Canvas> ().enabled = !active;
		controlRig.gameObject.GetComponent<Canvas>().enabled = !active;
		if (!active) {


			this.GetComponentInParent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}

	void OnDestroy()
	{

		GameMaster.gm.onToggleUpgradeMenu -= onUpgradeMenuToggle;
	}


	void RegenHealth()
	{
		stats.curHealth += 1;

		statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);

	}



	public void DamagePlayer (int damage) {
		//Debug.LogError ("Player Damaged");
		damageUI.GetComponent<Image>().enabled = true;
		damage -= stats.armor;
		stats.curHealth -= damage;


			statusIndicator.SetHealth (stats.curHealth, stats.maxHealth);
	

		if (stats.curHealth <= 0) {
			
			GameMaster.KillPlayer(this);
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}

	void OnTriggerEnter(Collider impactObject)
	{
		
		
		if (impactObject.tag == "SolidGround" ) {

			//Debug.LogError ("Hey onTriggerEntered!");


			//Instantiate (explosion, transform.position, transform.rotation);
			DamagePlayer (99999);



		}

		if (impactObject.tag == "Enemy") {

			Enemy enemy = new Enemy ();
			enemy = impactObject.GetComponentInParent<Enemy> ();
			enemy.DamageEnemy (99999);

			DamagePlayer (99999);

		}


	}

	void OnTriggerExit(Collider impactObject)
	{


		if (impactObject.tag == "OutOfBound" ) {

			//Debug.LogError ("Hey onTriggerExit!");


			//Instantiate (explosion, transform.position, transform.rotation);
			DamagePlayer (99999);



		}


	}

}
