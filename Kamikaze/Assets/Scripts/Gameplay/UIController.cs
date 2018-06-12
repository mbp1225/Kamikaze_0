using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    
    [SerializeField] Transform p1;
    [SerializeField] Transform handP1;

    [Space(10)]

    [Header("Player 2")]

    [SerializeField] Transform p2;
    [SerializeField] Transform handP2;

    [Space(10)]


    [Header("Turn Cover")]
    [SerializeField] Transform turnCover;
    [SerializeField] TextMeshProUGUI playerLabel;
    bool coverOn = true;

    [Header("Help Table")]
    [SerializeField] Transform helpTable;
    bool helpOn = false;



    void Start ()
	{
        
	}
	
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.F1)) ToggleHelp();
	}

    public void ToggleCover()
    {
        if (gc.currentPlayer == 1) playerLabel.text = "Player One";
        else if (gc.currentPlayer == 2) playerLabel.text = "Player Two";

        if (coverOn) turnCover.DOMoveY(Screen.height * 2, .5f);
        else turnCover.DOMoveY(Screen.height/2, .5f);

        coverOn = !coverOn;
    }

    public void ToggleHelp()
    {
        if (helpOn) helpTable.DOLocalMoveY(Screen.height - 190, .5f);
        else helpTable.DOLocalMoveY(Screen.height - 450, .5f);

        helpOn = !helpOn;
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

    public void SwitchUI ()
    {
        if (gc.currentPlayer == 1)
        {
            p1.gameObject.SetActive(true);//Quero que isso aqui demore
            p2.gameObject.SetActive(false);//E isso
        }
        else if (gc.currentPlayer == 2)
        {
            p2.gameObject.SetActive(true);//E isso
            p1.gameObject.SetActive(false);//E isso //Quebra não //Tá tudo vermelho
        }
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
        
        if (player == 2)
        {
            foreach (Transform card in handP2) Destroy(card);

            for (int i = 0; i < GameController.handSize; i++)
            {
                Instantiate(handCard, handP2);
                handP2.GetChild(i).GetComponent<CardManager>().UpdateCard(gc.player2.hand[i]);
            }

        }
    }



}
