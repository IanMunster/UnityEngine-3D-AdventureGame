using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Text manager.
/// 
/// </summary>

public class TextManager : MonoBehaviour {

	// Struct Encapsulates Message that are sent for Organising
	public struct Instruction {
		// Body of Message
		public string message;
		// Color of Message
		public Color textColor;
		// Time Message should be Displayed (Based on TimeTrigger and Delay)
		public float startTime;
	}


	// Reference to UI Text Component
	public Text text;
	// Display for Each Character in Message
	public float displayTimePerCharacter = 0.1f;
	// Additional Time to Display Message
	public float additionalDisplayTime = 0.5f;

	// Collection of Instructions order by StartTime
	private List<Instruction> instructions = new List<Instruction> ();
	// Time before Message is Cleared
	private float clearTime;


	// Update is Called every Frame
	private void Update () {
		// If Instuctions Found and Time is beyond StartTime of First Message
		if (instructions.Count > 0 && Time.time >= instructions[0].startTime) {
			// Set UIText to display Message
			text.text = instructions[0].message;
			// Set Message Color
			text.color = instructions[0].textColor;
			// Then Remove Instruction
			instructions.RemoveAt (0);
		// Otherwise if Time is Beyond ClearTime
		} else if (Time.time >= clearTime) {
			// Clear UIText
			text.text = string.Empty;
		}
	}


	// Function Called from TextReaction (display Message to Screen)
	public void DisplayMessage (string message, Color textColor, float delay) {
		// Time when Message start Displaying (CurrentTime offset by Delay)
		float startTime = Time.time + delay;
		// Calculate Display Duration (based on Number of Character in Message + additionalTime)
		float displayDuration = message.Length * displayTimePerCharacter + additionalDisplayTime;
		// Create new ClearTime (based on startTime + total DisplayDuration)
		float newClearTime = startTime + displayDuration;

		// If After ClearTime
		if (newClearTime > clearTime) {
			// Replace Old ClearTime with NewClearTime
			clearTime = newClearTime;
		}

		// Create a NewInstruction (with all attributes set to given)
		Instruction newInstruction = new Instruction {
			message = message,
			textColor = textColor,
			startTime = startTime
		};

		// Add NewInstruction to InstructionCollection
		instructions.Add (newInstruction);
		// Sort the InstructionCollection (order in StartTime)
		SortInstructions ();
	}



	// Function to Sort Instructions on StartTime order
	private void SortInstructions () {
		// Go through all Instructions
		for (int i = 0; i < instructions.Count; i++) {
			// Create Bool to check if Reordering has been done
			bool swapped = false;
			// For each instruction, go through each instruction
			for (int j = 0; j < instructions.Count; j++) {
				// Compare Instruction from OuterLoop with this Instruction; if OuterLoop has LaterStartTime
				if (instructions[i].startTime > instructions[j].startTime) {
					// Swap Positions of Outer&InnerLoop Instructions 
					Instruction temp = instructions[i];
					instructions [i] = instructions [j];
					instructions [j] = temp;
					// Set Swapped true
					swapped = true;
				}
			}

			// If for a SingleInstruction, all other are Later
			if (!swapped) {
				// Correctly Ordered
				break;
			}
		}
	}
}
