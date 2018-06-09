using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
	//Card scriptable object
	[SerializeField] Card card;

	//Fields
	[SerializeField] TextMeshProUGUI cardName;
	[SerializeField] TextMeshProUGUI cardDescription;
	[SerializeField] TextMeshProUGUI cardCost;
	[SerializeField] TextMeshProUGUI cardValue;
	[SerializeField] Image cardImage;

    [SerializeField] GameController gc;

	void Start ()
	{
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        UpdateCard();	
	}

	void UpdateCard ()
	{
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
    }

    public void UpdateCard(Card newCard)
    {
        card = newCard;

        UpdateCard();
    }

    public void Select()
    {
        print("Clicked " + card.name);
        gc.PlayCard(card);
        Destroy(gameObject);
    }
}
