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

	CameraController cam;

	void Start ()
	{
		SetupGame();
		cam = GetComponent<CameraController>();
	}
	
	void Update ()
	{
		if (Input.GetButtonDown("Jump"))
		{
			print(player1.hand.Count);

		}
	}

	void SetupGame()
	{
		//Setting atributes to initial values
		energy = 5;
		turn = 1;
		currentPlayer = 1;

		//Setting initial energy
		player1.currentEnergy = energy;
		player2.currentEnergy = energy;

		//Empty hands
		player1.hand = new List<Card>(5);
		player2.hand = new List<Card>(5);

		//Empty decks
		player1.deck = new List<Card>(30);
		player2.deck = new List<Card>(30);

		//Empty tokens
		player1.tokens = new List<Transform>();
		player2.tokens = new List<Transform>();

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
			energy++;
			player1.currentEnergy = energy;
			player2.currentEnergy = energy;

			PickCard(1);
			PickCard(2);

            currentPlayer = 1;
			Invoke("CallSwitchUI", 0.5f);
            //Everything else
        }
    }

	public void ResetTokens()

	{
		foreach (Transform token in player1.tokens) token.GetComponent<Token>().Reset();
		foreach (Transform token in player2.tokens) token.GetComponent<Token>().Reset();
	}

	void CallSwitchUI()
	{
		UI.GetComponent<UIController>().SwitchUI();
		UI.GetComponent<UIController>().UpdateEnergy();

		UI.GetComponent<UIController>().UpdateHand(1);
		UI.GetComponent<UIController>().UpdateHand(2);

		cam.ResetRotation(currentPlayer);
	}

    public void PlayCard(Card card)
    {
		if (currentPlayer == 1) player1.currentEnergy -= card.cardCost;
		else if (currentPlayer == 2) player2.currentEnergy -= card.cardCost;
        StartCoroutine(PositionToken(card));
    }

	public void PickCard(int player)
	{
		if (player == 1 && player1.hand.Count < handSize)
		{
			player1.hand.Add(player1.deck[1]);
			player1.deck.RemoveAt(1);
		}
		else if (player == 2 && player2.hand.Count < handSize)
		{
			player2.hand.Add(player2.deck[1]);
			player2.deck.RemoveAt(1);
		}
	}

    IEnumerator PositionToken(Card currentCard)
    {
        //print("Positioning token");
        while (!Input.GetMouseButtonDown(0)) yield return null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.name == "Field")
        {
            GameObject newToken = Instantiate(token, hit.point, Quaternion.identity, null);
            newToken.GetComponent<Token>().SetCard(currentCard);
			newToken.GetComponent<Token>().SetPlayer(currentPlayer);
			if (currentPlayer == 1) player1.tokens.Add(newToken.transform);
			else if (currentPlayer == 2) player1.tokens.Add(newToken.transform);
            //print("Token positioned");
        }

		if (currentPlayer == 1)
		{
			for (int i = 0; i < player1.hand.Count; i++)
			{
				if (player1.hand[i] == currentCard)
				{
					player1.hand.RemoveAt(i);
					break;
				}
			}
		}
		else if (currentPlayer == 2)
		{
			for (int i = 0; i < player2.hand.Count; i++)
			{
				if (player2.hand[i] == currentCard)
				{
					player2.hand.RemoveAt(i);
					break;
				}
			}
		}

		UI.GetComponent<UIController>().UpdateEnergy();
    }
}
