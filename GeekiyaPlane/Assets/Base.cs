using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	public int maxHealth = 1000;
	private int _curHealth;

	public int curHealth
	{
		get { return _curHealth;}
		set {_curHealth = Mathf.Clamp (value, 0, maxHealth);}
	}

	public float healthRegenRate = 1f;

	public int armor = 5;

	public GameObject explosion;

	[SerializeField]
	private StatusIndicator statusIndicator;

	public Base _base;

	// Use this for initialization
	void Start () {


		curHealth = maxHealth;

		statusIndicator.SetHealth (curHealth, maxHealth);

		InvokeRepeating ("RegenHealth", 1f/healthRegenRate, 1f/healthRegenRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RegenHealth()
	{
		curHealth += 1;

		statusIndicator.SetHealth (curHealth, maxHealth);

	}

	public void Damage(int damage) {
		
	
		damage -= armor;
		curHealth -= damage;


		statusIndicator.SetHealth (curHealth, maxHealth);


		if (curHealth <= 0) {

			GameMaster.KillBase(this);
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}






}
