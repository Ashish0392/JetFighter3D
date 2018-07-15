using UnityEngine;
using UnityEngine.UI;


public class EnemyIndicator : MonoBehaviour {


	[SerializeField]
	private Image image;
	[SerializeField]
	private Image trackImage;
	[SerializeField]
	private Image arrowImage;
	[SerializeField]
	private Image healthBar;
	//private Vector3 ZaxisNull = new Vector3(1f, 1f, 0f);


	void Start()
	{
		

		if (image == null)
		{
			Debug.LogError("Enemy INDICATOR: No Image object referenced!");
		}


	}


	void Update()
	{
		Vector3 namePos = Camera.main.WorldToScreenPoint (this.transform.position);
		Vector3 diff = Camera.main.transform.position - this.transform.position;
		float curDistance = diff.sqrMagnitude;


	


		//namePos *= ZaxisNull;



		if (namePos != null) {
			

			if (namePos.z > 0 && namePos.x < Screen.width && namePos.y < Screen.height) {
				


				//image.transform.GetComponent<Image> ().enabled = true;
				healthBar.transform.GetComponent<Image> ().enabled = true;
				//image.transform.position = namePos;
				healthBar.transform.position = namePos;



				//Debug.LogError (curDistance);
				//Debug.LogError("yes");
				trackImage.transform.position = namePos;
				trackImage.transform.GetComponent<Image> ().enabled = true;

			



			

			} 
			else if(namePos.z < 0) {

				namePos *= -1;

				Vector3 screenCenter = new Vector3 (Screen.width, Screen.width, 0)/2;


				namePos -= screenCenter;

				float angle = Mathf.Atan2 (namePos.y, namePos.x);
				angle -= 90 * Mathf.Deg2Rad;

				float cos = Mathf.Cos (angle);
				float sin = Mathf.Sin (angle);

				namePos = screenCenter + new Vector3 (sin * 150, cos * 150, 0);

				float m = cos / sin;

				Vector3 screenBounds = screenCenter * 0.9f;

				if (cos > 0) {
					namePos = new Vector3 (screenBounds.y / m, screenBounds.y, 0);
				} else {
					namePos = new Vector3 (-screenBounds.y / m, -screenBounds.y, 0);
				}

				if (namePos.x > screenBounds.x) {
					namePos = new Vector3 (screenBounds.x, screenBounds.x * m, 0);
				}else if(namePos.x < -screenBounds.x){

					namePos = new Vector3 (-screenBounds.x, -screenBounds.x * m, 0);
				}

				namePos += screenCenter;

				arrowImage.transform.position = namePos;
				arrowImage.transform.localRotation = Quaternion.Euler (0,0,-angle*Mathf.Rad2Deg);
				arrowImage.transform.GetComponent<Image> ().enabled = true;

				
			}
		}
	}
}
