using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class ControlRigUI : MonoBehaviour {




	public int m1 = 0, m2 = 1;

	void Start()
	{
		
		//control = GameObject.FindGameObjectWithTag ("Player").GetComponentInParent<AeroplaneUserControl2Axis> ();
	}

	public void M1()
	{
		
		Debug.Log ("fire m1 called");
	}

	public void M2()
	{
		
		Debug.Log ("fire m2 called");
	}


}
