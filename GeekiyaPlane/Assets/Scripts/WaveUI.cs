using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveUI : MonoBehaviour {

	[SerializeField]
	WaveSpawner spawner;

	//[SerializeField]
	//Animator waveAnimator;
	[SerializeField]
	Transform cui;
	[SerializeField]
	Transform iui;

	[SerializeField]
	Text waveCountdownText;

	[SerializeField]
	Text waveCountText;

	private WaveSpawner.SpawnState previousState;



	// Use this for initialization
	void Start () {

		cui = GameObject.FindGameObjectWithTag ("CountdownUI").transform;
		iui = GameObject.FindGameObjectWithTag ("IncomingUI").transform;
		 
		if (spawner == null)
		{
			Debug.LogError("No spawner referenced!");
			this.enabled = false;
		}
		/*if (waveAnimator == null)
		{
			Debug.LogError("No waveAnimator referenced!");
			this.enabled = false;
		}*/

		if (waveCountdownText == null)
		{
			Debug.LogError("No waveCountdownText referenced!");
			this.enabled = false;
		}
		if (waveCountText == null)
		{
			Debug.LogError("No waveCountText referenced!");
			this.enabled = false;
		}
	}

	
	// Update is called once per frame
	void Update () {
		//Debug.LogError (spawner.State);
		switch (spawner.State)
		{
			case WaveSpawner.SpawnState.COUNTING:
				UpdateCountingUI();
				break;
			case WaveSpawner.SpawnState.SPAWNING:
				UpdateSpawningUI();
				break;
		case WaveSpawner.SpawnState.WAITING:
			break;
		
        }

		previousState = spawner.State;


	}

	void UpdateCountingUI ()
	{
		
		if (previousState != WaveSpawner.SpawnState.COUNTING)
		{
			cui.gameObject.SetActive (true);
			iui.gameObject.SetActive (false);

			//waveAnimator.SetBool("WaveIncoming", false);
			//waveAnimator.SetBool("WaveCountdown", true);
			//Debug.Log("COUNTING");
		}
		waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
		cui.gameObject.SetActive (true);
	}

	void UpdateSpawningUI()
	{
		if (previousState != WaveSpawner.SpawnState.SPAWNING)
		{
			cui.gameObject.SetActive (false);
			iui.gameObject.SetActive (true);

			//waveAnimator.SetBool("WaveCountdown", false);
			//waveAnimator.SetBool("WaveIncoming", true);

			waveCountText.text = spawner.NextWave.ToString();

			//Debug.Log("SPAWNING");
		}
		StartCoroutine (SetAllToFalse());


	}

	IEnumerator SetAllToFalse(){
		yield return new WaitForSecondsRealtime (5);
		cui.gameObject.SetActive (false);
		iui.gameObject.SetActive (false);
		
	}
}
