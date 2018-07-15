using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour {

	[SerializeField]
	private Text healthText;

	[SerializeField]
	private Text armorText;

	[SerializeField]
	private Text doomText;

	[SerializeField]
	private Text haunterText;

	[SerializeField]
	private Text moneyText;

	[SerializeField]
	private Text highScore;

	[SerializeField]
	private float healthMultiplier = 1.3f;

	[SerializeField]
	private float armorMultiplier = 1.3f;

	[SerializeField]
	private float costMultiplier = 1.4f;

	[SerializeField]
	private int[] upgradeCost;

	[SerializeField]
	private Text[] upgradeText;

	private GameMaster _gm;


	private PlayerStats stats;





	void OnEnable ()
	{ 
		_gm = GameMaster.gm;
		
		stats = PlayerStats.instance;
	
		UpdateValues();

		highScore.text = "HighScore" + PlayerPrefs.GetInt ("HighScore").ToString();
    }

	void UpdateValues ()
	{
		healthText.text = "HEALTH: " + stats.maxHealth.ToString();

		armorText.text = "Armor: " + stats.armor.ToString();

		doomText.text = "Doom Level:" + stats.levelM[0].ToString ();
		haunterText.text = "Haunter Level:" +stats.levelM[1].ToString ();
		moneyText.text = "Money:" + _gm.money.ToString ();




		for (int i = 0; i < upgradeCost.Length; i++) {


			upgradeText [i].text = "("+upgradeCost [i].ToString()+")";

		}

    }

	public void UpgradeHealth ()
	{
		if ( _gm.money >= upgradeCost[0]) {
			stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);

			_gm.money -= upgradeCost[0];
			UpdateCost (0);
			UpdateValues ();
		}
	}

	public void UpgradeArmor()
	{
		if ( _gm.money >= upgradeCost [1]) {
			stats.armor = (int)Mathf.Round (stats.armor * armorMultiplier);

			_gm.money -= upgradeCost[1];
			UpdateCost (1);
			UpdateValues();
		}

	}

	public void UpgradeDoom()
	{
		if ( _gm.money >= upgradeCost [2]) {
			stats.levelM[0] += 1; 
		
			_gm.money -= upgradeCost[2];
			UpdateCost (2);
			UpdateValues();
		}

	}

	public void UpgradeHaunter()
	{ 
		if ( _gm.money >= upgradeCost [3]) {

			stats.levelM[1] += 1; 

			_gm.money -= upgradeCost[3];
			UpdateCost (3);
			UpdateValues();
		}

	}

	public void LeaveGame(){

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	void UpdateCost(int i){

		upgradeCost [i] = (int)Mathf.Round(upgradeCost [i] * costMultiplier);

	}
}
