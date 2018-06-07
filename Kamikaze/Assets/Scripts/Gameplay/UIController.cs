using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

	[Header("UI Card")]
	//UI Card Attributes
	//The card that shows up when you hover over a token.
	[SerializeField] GameObject UICard;
	[SerializeField] TextMeshProUGUI cardName;
	[SerializeField] TextMeshProUGUI cardDescription;
	[SerializeField] TextMeshProUGUI cardCost;
	[SerializeField] TextMeshProUGUI cardValue;
	[SerializeField] Image cardImage;


	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	public void ShowCard (Card card)
	{
		UICard.transform.localPosition = new Vector3(Input.mousePosition.x - Screen.width/2 + Screen.width/5, Input.mousePosition.y - Screen.height/2, 0);

		cardName.text = card.cardName;
		cardDescription.text = card.cardDescription;
		cardCost.text = card.cardCost.ToString();
		cardImage.sprite = card.cardImage;

		switch (card.Type)
		{	
			case Card.cardType.Unit:
				cardValue.text = "Attack: " + card.attack;
				break;

			case Card.cardType.Structure:
				cardValue.text = "Radius: " + card.influenceRadius;
				break;
		}
		
		UICard.SetActive(true);
	}

	public void ShowCard ()
	{
		UICard.SetActive(false);
	}



}
