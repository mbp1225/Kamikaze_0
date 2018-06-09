using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameController gc;

	[Header("UI Card")]
	//UI Card Attributes
	//The card that shows up when you hover over a token.
	[SerializeField] GameObject UICard;
    [SerializeField] Transform handCard;

    [Space(10)]

    [Header("Player 1")]
    [SerializeField] Transform handP1;



	void Start ()
	{
        
	}
	
	void Update ()
	{
		
	}

	public void ShowCard (Card card)
	{
		UICard.transform.localPosition = new Vector3(Input.mousePosition.x - Screen.width/2 + Screen.width/5, Input.mousePosition.y - Screen.height/2, 0);

        UICard.GetComponent<CardManager>().UpdateCard(card);

        UICard.SetActive(true);
	}

	public void ShowCard ()
	{
		UICard.SetActive(false);
	}

    public void UpdateHand(int player)
    {
        if (player == 1)
        {
            foreach (Transform card in handP1) Destroy(card);

            for (int i = 0; i < GameController.handSize; i++)
            {
                Instantiate(handCard, handP1);
                handP1.GetChild(i).GetComponent<CardManager>().UpdateCard(gc.player1.hand[i]);
            }

        }
    }



}
