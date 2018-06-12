using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public struct Player
	{
		public List<Card> hand;
		public List<Card> deck;
		public List<Transform> tokens;
		public int currentEnergy;
		
	}

	//Game Attributes
	[SerializeField] Card[] cards = new Card[5];
	int energy = 5;
	int turn = 1;
	public int currentPlayer = 1;

	static int deckSize = 30;
	public static int handSize = 5;

	public Player player1, player2;

	//References
	[SerializeField] Transform UI;
    [SerializeField] GameObject token;

	void Start ()
	{
		SetupGame();

	}
	
	void Update ()
	{
		
	}

	void SetupGame()
	{
		//Setting atributes to initial values
		energy = 5;
		turn = 1;
		currentPlayer = 1;

		//Empty hands
		player1.hand = new List<Card>(5);
		player2.hand = new List<Card>(5);

		//Empty decks
		player1.deck = new List<Card>(30);
		player2.deck = new List<Card>(30);

		//Fill decks
		for (int i = 0; i < deckSize; i++)
		{
			player1.deck.Add(cards[Random.Range(1,cards.Length-1)]);
		}

		for (int i = 0; i < deckSize; i++)
		{
			player2.deck.Add(cards[Random.Range(1,cards.Length-1)]);
		}

		//Fill hands
		for (int i = 0; i < handSize; i++)
		{
			player1.hand.Add(player1.deck[1]);
			player1.deck.RemoveAt(1);
		}

		for (int i = 0; i < handSize; i++)
		{
			player2.hand.Add(player1.deck[1]);
			player2.deck.RemoveAt(1);
		}

        //Update UI
        UI.GetComponent<UIController>().UpdateHand(1);
		UI.GetComponent<UIController>().UpdateHand(2);
    }

    public void Turn()
    {
        if (currentPlayer == 1)
        {
            currentPlayer = 2;			
			Invoke("CallSwitchUI", 0.5f);
            //Everything else
        }
        else
        {
            currentPlayer = 1;
			Invoke("CallSwitchUI", 0.5f);
            //Everything else
        }
    }

	void CallSwitchUI()
	{
		UI.GetComponent<UIController>().SwitchUI();
	}

    public void PlayCard(Card card)
    {
        StartCoroutine(PositionToken(card));
    }

    IEnumerator PositionToken(Card currentCard)
    {
        //print("Positioning token");
        while (!Input.GetMouseButtonDown(0)) yield return null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.name == "Field")
        {
            //Give it a parent at some point;
            GameObject newToken = Instantiate(token, hit.point, Quaternion.identity, null);
            newToken.GetComponent<Token>().SetCard(currentCard);
			newToken.GetComponent<Token>().SetPlayer(currentPlayer);
            //print("Token positioned");
        }
    }
}
