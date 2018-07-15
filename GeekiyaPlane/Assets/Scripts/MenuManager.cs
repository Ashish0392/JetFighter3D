using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	private Text highScoreCounter;

	//[SerializeField]
	//private Text PlayerName;


	void Start()
	{
		
		highScoreCounter.text = "HighScore    " + PlayerPrefs.GetInt ("HighScore", 0).ToString ();
		//PlayerName.text = "" + PlayerPrefs.GetString ("NickName", "");
	}

	public void StartGame ()
	{
		
	



		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Debug.Log("WE QUIT THE GAME!");
		Application.Quit();
	}



}
