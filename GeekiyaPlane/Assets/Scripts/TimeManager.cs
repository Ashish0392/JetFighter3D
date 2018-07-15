
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public float slowDownFactor = 0.05f;
	public float slowDownLength = 2f;

	public Button slow;


	void Update()
	{
		Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
		Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);


	}

	public void DoSlowMotion()
	{

		Time.timeScale = slowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * 0.02f;

	}

}
