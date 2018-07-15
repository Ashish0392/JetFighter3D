using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats : MonoBehaviour {
	public static PlayerStats instance;

	public int level = 1;
	public int exp = 1;
	public int maxHealth = 100;
	public int armor = 5;

	public float healthRegenRate = 5f;

	public int[] levelM = { 1 , 0 };



	private int _curHealth;
	public int curHealth
	{
		get { return _curHealth;}
		set {_curHealth = Mathf.Clamp (value, 0, maxHealth);}
	}

	 void Awake()
	{
		if (instance == null) {

			instance = this;
		}

		curHealth = maxHealth;
	}
}
