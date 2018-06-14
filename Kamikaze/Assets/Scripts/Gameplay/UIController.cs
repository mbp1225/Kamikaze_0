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

    [SerializeField] TextMeshProUGUI energyP1;

    [Space(10)]

    [Header("Player 2")]

    [SerializeField] Transform p2;
    [SerializeField] Transform handP2;

    [SerializeField] TextMeshProUGUI energyP2;

    [Space(10)]

    [Header("Command Help")]
    [SerializeField] Transform commandUI;
    [SerializeField] Transform attackUI;
    [SerializeField] Transform moveUI;

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
        print("Showing card");
        if  (Input.mousePosition.x < Screen.width/2) UICard.transform.localPosition = new Vector3(Input.mousePosition.x - Screen.width/2 + Screen.width/5, Input.mousePosition.y - Screen.height/2, 0);
        else UICard.transform.localPosition = new Vector3(Input.mousePosition.x - Screen.width/2 - Screen.width/5, Input.mousePosition.y - Screen.height/2, 0);

        UICard.GetComponent<CardManager>().UpdateCard(card);

        UICard.SetActive(true);
	}

    public void ShowCommandUI (bool canMove, bool canAttack)
	{
        print("Showing Command UI");
        if  (Input.mousePosition.y < Screen.height/2) commandUI.transform.localPosition = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2 - Screen.height/11, 0);
        else commandUI.transform.localPosition = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2 - Screen.height/11, 0);
        
        if (canMove) moveUI.gameObject.GetComponent<Image>().color = Color.white;
        else moveUI.gameObject.GetComponent<Image>().color = Color.grey;

        if (canAttack) attackUI.gameObject.GetComponent<Image>().color = Color.white;
        else attackUI.gameObject.GetComponent<Image>().color = Color.grey;
        
        commandUI.gameObject.SetActive(true);
	}

    public void HideCommandUI()
    {
        commandUI.gameObject.SetActive(false);
    }

	public void ShowCard ()
	{
		UICard.SetActive(false);
	}

    public void SwitchUI ()
    {
        if (gc.currentPlayer == 1)
        {
            p1.gameObject.SetActive(true);
            p2.gameObject.SetActive(false);
        }
        else if (gc.currentPlayer == 2)
        {
            p2.gameObject.SetActive(true);
            p1.gameObject.SetActive(false);

            gc.ResetTokens();
        }


    }

    public void UpdateEnergy()
    {
        energyP1.text = gc.player1.currentEnergy.ToString();
        energyP2.text = gc.player2.currentEnergy.ToString();
    }

    public void UpdateHand(int player)
    {
        if (player == 1)
        {
            foreach (Transform card in handP1.transform) Destroy(card.gameObject);

            foreach (Card card in gc.player1.hand)
            {
                Instantiate(handCard, handP1);
                handP1.GetChild(handP1.childCount - 1).GetComponent<CardManager>().UpdateCard(card);
            }

            energyP1.text = gc.player1.currentEnergy.ToString();

        }
        
        if (player == 2)
        {
            foreach (Transform card in handP2.transform) Destroy(card.gameObject);

            foreach (Card card in gc.player2.hand)
            {
                Instantiate(handCard, handP2);
                handP2.GetChild(handP2.childCount - 1).GetComponent<CardManager>().UpdateCard(card);
            }

            energyP2.text = gc.player2.currentEnergy.ToString();

        }
        
    }



}
