using System.Collections;
using UnityEngine;

/// <summary>
/// Text reaction.
/// 
/// </summary>

public class TextReaction : Reaction {

	// Text of ReactionMessage
	public string message;
	// Color of ReactionMessage
	public Color textColor = Color.white;
	// Delay before Reaction
	public float delay;

	// Reference to the TextManager
	private TextManager textManager;


	// Specific Initialization of TextManager
	protected override void SpecificInit () {
		// Find the TextManager
		textManager = FindObjectOfType <TextManager> ();
	}


	// Function if Immediate Reaction is Called
	protected override void ImmediateReaction () {
		// Display the Message in TextManager
		textManager.DisplayMessage (message, textColor, delay);
	}
}