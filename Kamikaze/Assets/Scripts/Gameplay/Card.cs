using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card")]
public class Card : ScriptableObject
{
	[Header("Generic Attributes")]
	public string cardName;
	public Sprite cardImage;
	public int cardCost;
	public string cardDescription;

	public enum cardType {Unit, Structure};
	public cardType Type;

	[Space(10)]

	[Header("Unit Attributes")]

	public int unitHealth;
	public int moveDistance;
	public int attack;

	[Space(10)]

	[Header("Structure Attributes")]

	public int structureHealth;
	public int influenceRadius;

}
