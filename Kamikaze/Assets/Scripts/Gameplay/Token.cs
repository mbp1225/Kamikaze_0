using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Token : MonoBehaviour
{
	//Token Attributes
	Transform mesh; //Has to be the first child (index 0).
	Collider col;

	[SerializeField] int health;

	public bool canMove = true;
	public bool canAttack = true;

	LineRenderer line;

	public int owner;

	bool isMoving = false;
	bool isAttacking = false;

	//References
	[SerializeField] Transform UI;
	[SerializeField] Card card;

	GameController gc;


	void Start ()
	{
        UI = GameObject.FindGameObjectWithTag("UIController").transform;
		gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

		mesh = transform.GetChild(0);
		col = transform.GetComponent<Collider>();
		line = GetComponent<LineRenderer>();
		line.enabled = false;
	}

	public void SetPlayer(int n)
	{
		if (n == 1)
		{
			owner = 1;
			transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.blue;
		}
		if (n == 2)
		{
			owner = 2;
			transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}
	
	void Update ()
	{
		//Movement
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

			canMove = false;
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

		//Attacking
		if(Input.GetMouseButtonDown(1) && isAttacking)
		{
			//print("1");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Token" && hit.transform.GetComponent<Token>().owner != owner)
			{
				print("Hit a Token");
				transform.LookAt(hit.transform.position);
				if (Vector3.Distance(transform.position, hit.transform.position) < 2) hit.transform.GetComponent<Token>().TakeDamage(card.attack);
				//else transform.DOMove(transform.position + transform.forward.normalized * 3, .25f);
				//print("2");
				isAttacking = false;
				//line.enabled = false;
				//transform.DOMove(hit.point, .25f);
			}
			else isAttacking = false;
			line.enabled = false;

			canAttack = false;
		}
		
		if(isAttacking)
		{
			line.SetPosition(0, transform.position + Vector3.up * .25f);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Token" && hit.transform.GetComponent<Token>().owner != owner)
			{
				line.enabled = true;
				transform.LookAt(hit.transform.position);
				line.SetPosition(1, hit.transform.position + Vector3.up * .25f);
			}
			if (Physics.Raycast(ray, out hit) && hit.transform.name == "Field")
			{
				line.enabled = true;
				transform.LookAt(hit.point);
				if (Vector3.Distance(transform.position, hit.point) < 2) line.SetPosition(1, transform.position + transform.forward.normalized * (Vector3.Distance(transform.position, hit.point)) + Vector3.up * .25f);
				else line.SetPosition(1, transform.position + transform.forward.normalized * 2 + Vector3.up * .25f);
			}
			else line.enabled = false;
		}
		
	}

    public void SetCard(Card newCard)
    {
        card = newCard;
		if (newCard.Type == Card.cardType.Unit) health = newCard.unitHealth;
		else if (newCard.Type == Card.cardType.Structure) health = newCard.structureHealth;
    }

	public void Reset()
	{
		canAttack = true;
		canMove = true;
	}

	public void TakeDamage(int damage)
	{
		print("Hit");
		health -= damage;
		if (health <= 0) Die();
	}

	void Die()
	{
		Destroy(gameObject);
	}

	IEnumerator Move()
	{
		line.startColor = Color.green;
		line.endColor = Color.green;
		//print("Started Movement");
		yield return new WaitForSeconds(.05f);
		isMoving = true;
		line.enabled = true;
	}

	IEnumerator Attack()
	{
		line.startColor = Color.red;
		line.endColor = Color.red;
		print("Started Attack");
		yield return new WaitForSeconds(.05f);
		isAttacking = true;
		line.enabled = true;
	}

	void OnMouseEnter()
	{
		print("Over token");
		UI.GetComponent<UIController>().ShowCard(card);
		if (gc.currentPlayer == owner) UI.GetComponent<UIController>().ShowCommandUI(canMove, canAttack);
	}

	void OnMouseExit()
	{
		UI.GetComponent<UIController>().ShowCard();
		UI.GetComponent<UIController>().HideCommandUI();
	}

	/*
	void OnMouseDown()
	{
		UI.GetComponent<UIController>().ShowCard();
		if (gc.currentPlayer == owner && !isMoving)
		{
			if (Input.GetMouseButtonDown(0)) StartCoroutine(Move());
			else if (Input.GetMouseButtonDown(1)) StartCoroutine(Attack());
		}
		else return;
	}
	*/

	void OnMouseOver()
	{
		if (gc.currentPlayer == owner && !isMoving)
		{
			if (Input.GetMouseButtonDown(0) && canMove) StartCoroutine(Move());
			else if (Input.GetMouseButtonDown(1) && canAttack) StartCoroutine(Attack());

			//UI.GetComponent<UIController>().ShowCard();
		}
	}
}
