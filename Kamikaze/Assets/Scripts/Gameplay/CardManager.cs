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
	[SerializeField] GameObject UICard;
	[SerializeField] TextMeshProUGUI cardName;
	[SerializeField] TextMeshProUGUI cardDescription;
	[SerializeField] TextMeshProUGUI cardCost;
	[SerializeField] TextMeshProUGUI cardValue;
	[SerializeField] Image cardImage;

	void Start ()
	{
		
	}

	void UpdateCard ()
	{

	}
}
