using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class MissileLauncher : MonoBehaviour {

	//public enum ReInstateState { Wait , Ready };


	[System.Serializable]
	public class Weapon{
		public string name;
		public Transform weaponPrefab;


		public float reInstateTime;
		public int ReInstateTime
		{
			get { return (int)reInstateTime; }
		}

		public Transform firePoint;
		public string button;


		public GameObject reInstateStatusIndicator;


		public float reInstateCountdown = 0f;
		public int ReInstateCountdown
		{
			get { return (int)reInstateCountdown; }
		}




}
	public Weapon[] weapon;

	private PlayerStats playerStats;






	// Use this for initialization
	void Start () {

		playerStats = PlayerStats.instance;
		
		for(int i = 0; i < weapon.Length; i++){
			



		weapon[i].reInstateCountdown = weapon[i].reInstateTime;
			weapon [i].reInstateStatusIndicator.GetComponent<Image>().enabled = false;

		
	

		}

	}

	void Update()
	{
		for(int i = 0; i < weapon.Length; i++){


			weapon [i].reInstateCountdown -= Time.deltaTime;

			if (playerStats.levelM[i] > 0) {

				if (weapon [i].reInstateCountdown <= 0) {
					weapon [i].reInstateStatusIndicator.GetComponent<Image> ().enabled = true;

					if (CrossPlatformInputManager.GetButton (weapon [i].button)) {

						Fire (weapon [i]);
						Debug.Log (weapon [i].name);


						weapon [i].reInstateCountdown = weapon [i].reInstateTime;
						weapon [i].reInstateStatusIndicator.GetComponent<Image> ().enabled = false;
					}

				}
			}


		}


		/*if (weapon [0].reInstateCountdown <= 0) {
			weapon [0].reInstateStatusIndicator.GetComponent<Image>().enabled = true;

		if (CrossPlatformInputManager.GetButton ("Fire1")) {
			
				Fire (weapon [0]);
				Debug.Log ("fire1");
				weapon[0].reInstateCountdown = weapon[0].reInstateTime;
				weapon [0].reInstateStatusIndicator.GetComponent<Image>().enabled = false;
			}

		}

		if (weapon [1].reInstateCountdown <= 0) {
			weapon [1].reInstateStatusIndicator.GetComponent<Image>().enabled = true;
		if (CrossPlatformInputManager.GetButton ("Fire2")) {
			
			Fire (weapon [1]);
			Debug.Log ("fire2");
				weapon[1].reInstateCountdown = weapon[1].reInstateTime;
				weapon [1].reInstateStatusIndicator.GetComponent<Image>().enabled = false;
			}

		}*/
	}
	
	// Update is called once per frame


	public void Fire(Weapon _weapon){
		
	


	
			GameObject clone = Instantiate (_weapon.weaponPrefab.transform.gameObject, _weapon.firePoint.position, _weapon.firePoint.rotation) as GameObject;




	}



			

		
}
