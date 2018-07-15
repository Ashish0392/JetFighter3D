using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class GameOverUI : MonoBehaviour {

	private GameMaster _gm;


	public string videoID = "rewardedVideo";
	public string gameID;

	[SerializeField]
	private Transform videoButton;

	void OnEnable(){
		_gm = GameMaster.gm;
	

		if (_gm.revived) {

			videoButton.gameObject.SetActive (false);
		} else
			Advertisement.Initialize (gameID);

	}



	public void Quit ()
	{
		//Debug.Log("APPLICATION QUIT!");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		//Application.Quit();
	}

	public void Retry ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void AddVideo ()
	{

		ShowAd (videoID);



	}

	public void ShowAd(string video){

		if (Advertisement.IsReady ()) {
			Advertisement.Show (video, new ShowOptions(){resultCallback = HandleAdResult});
		}

	}
		
	public void HandleAdResult(ShowResult result){

		switch (result) {

		case ShowResult.Finished:
			Debug.Log ("reward player");
			RewardPlayer ();
			break;

		case ShowResult.Skipped:
			Debug.Log ("poora na dekha");
			break;

		case ShowResult.Failed:
			Debug.Log ("fail ad fail internet");
			break;

		}
	}


	public void RewardPlayer(){

		if (!_gm.revived) {
			_gm.InvokeRepeating ("IncreaseScore", 1f / _gm.scoreRate, 1f / _gm.scoreRate);
			_gm.StartCoroutine (_gm.RespawnPlayer ());
			_gm.revived = true;
			_gm.gameOverUI.gameObject.SetActive (false);

		}

	}

}