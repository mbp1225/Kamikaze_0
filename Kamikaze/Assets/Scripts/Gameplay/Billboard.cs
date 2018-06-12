using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	void Update ()
	{
		//transform.LookAt(Camera.main.transform.position);
		//transform.localEulerAngles = new Vector3(-30, transform.localEulerAngles.y, 0);
		transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
	}
}
