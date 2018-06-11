using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
	//[SerializeField] Transform UI;
	//[SerializeField] Card card;
	[SerializeField] float speed;
	[SerializeField] int borderSize;

    float hor, ver;

	void Start ()
	{
		
	}
	
	void Update ()
	{
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

		//Camera Position Mouse
		if (Input.mousePosition.x < borderSize) transform.position -= transform.right * speed * ((borderSize - Input.mousePosition.x)/borderSize * 1.0f) * Time.deltaTime;
		if (Input.mousePosition.x > Screen.width - borderSize) transform.position += transform.right * speed * ((Input.mousePosition.x - (Screen.width - borderSize))/borderSize * 1.0f) * Time.deltaTime;
		if (Input.mousePosition.y < borderSize) transform.position -= transform.forward * speed * ((borderSize - Input.mousePosition.y)/borderSize) * Time.deltaTime;
		if (Input.mousePosition.y > Screen.height - borderSize) transform.position += transform.forward * speed * ((Input.mousePosition.y - (Screen.height - borderSize))/borderSize * 1.0f) * Time.deltaTime;

        //Camera Position Keyboard
        if (hor < -0.1f) transform.position += transform.right * speed * hor * Time.deltaTime;
        if (hor > 0.1f) transform.position += transform.right * speed * hor * Time.deltaTime;
        if (ver < -0.1f) transform.position += transform.forward * speed * ver * Time.deltaTime;
        if (ver > 0.1f) transform.position += transform.forward * speed * ver * Time.deltaTime;

        //Camera Rotation
        if (Input.GetKeyDown(KeyCode.Q)) transform.DORotate(new Vector3(0, transform.eulerAngles.y + 90, 0), .5f);
		if (Input.GetKeyDown(KeyCode.E)) transform.DORotate(new Vector3(0, transform.eulerAngles.y - 90, 0), .5f);
		if (Input.GetKeyDown(KeyCode.R)) transform.DOMove(Vector3.zero, .5f);

		//Disconsider
		//if (Input.GetKeyDown(KeyCode.W)) UI.GetComponent<UIController>().ShowCard(card);
		//if (Input.GetKeyDown(KeyCode.S)) UI.GetComponent<UIController>().ShowCard();

	}
}
