using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Token : MonoBehaviour
{
	//Token Attributes
	Transform mesh; //Has to be the first child (index 0).
	Collider col;

	LineRenderer line;

	bool isMoving = false;

	//References
	[SerializeField] Transform UI;
	[SerializeField] Card card;


	void Start ()
	{
        UI = GameObject.FindGameObjectWithTag("UIController").transform;

		mesh = transform.GetChild(0);
		col = transform.GetComponent<Collider>();
		line = GetComponent<LineRenderer>();
		line.enabled = false;
	}
	
	void Update ()
	{
		//Debug.DrawRay(Input.mousePosition, cam.forward);
		if(Input.GetMouseButtonDown(0) && isMoving)
		{
			//print("1");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit) && hit.transform.name == "Field")
			{
				transform.LookAt(hit.point);
				if (Vector3.Distance(transform.position, hit.point) < card.moveDistance) transform.DOMove(hit.point, .25f);
				else transform.DOMove(transform.position + transform.forward.normalized * 3, .25f);
				//print("2");
				isMoving = false;
				line.enabled = false;
				//transform.DOMove(hit.point, .25f);
			}
			else isMoving = false;
			line.enabled = false;
		}

		if(isMoving)
		{
			line.SetPosition(0, transform.position + Vector3.up * .25f);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit) && hit.transform.name == "Field")
			{
				line.enabled = true;
				transform.LookAt(hit.point);
				if (Vector3.Distance(transform.position, hit.point) < card.moveDistance) line.SetPosition(1, transform.position + transform.forward.normalized * (Vector3.Distance(transform.position, hit.point)) + Vector3.up * .25f);
				else line.SetPosition(1, transform.position + transform.forward.normalized * 3  + Vector3.up * .25f);
			}
			else line.enabled = false;
		}
	}

    public void SetCard(Card newCard)
    {
        card = newCard;
    }

	IEnumerator Move()
	{
		//print("Started Movement");
		yield return new WaitForSeconds(.05f);
		isMoving = true;
		line.enabled = true;
	}

	void OnMouseEnter()
	{
		UI.GetComponent<UIController>().ShowCard(card);
	}

	void OnMouseExit()
	{
		UI.GetComponent<UIController>().ShowCard();
	}

	void OnMouseDown()
	{
		UI.GetComponent<UIController>().ShowCard();
		StartCoroutine(Move());
	}
}
