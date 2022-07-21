using UnityEngine;
using UnityEditor; // Required when using Editor-Components and Overrides

/// <summary>
/// All conditions editor.
/// 
/// </summary>

[CustomEditor ( typeof (AllConditions) )]
public class AllConditionsEditor : Editor {


	// Property for Accessing the Description for AllConditions (Used for Popups on the ConditionEditor)
	public static string[] AllConditionDescriptions {
		// Get the Descriptions Value
		get {
			// If the DescriptionsArray doens't Exist
			if (allConditionDescriptions == null) {
				// Set the Descriptions
				SetAllConditionDescriptions ();
			}
			// Otherwise return the Descriptions
			return allConditionDescriptions;
		}
		// Set the Descriptions to the DescriptionValue
		private set {
			allConditionDescriptions = value;
		}
	}


	// Stores Description of AllConditions
	private static string[] allConditionDescriptions;

	// All SubEditors to display Conditions
	private ConditionEditor[] conditionEditors;
	// Reference to Target
	private AllConditions allConditions;
	// Default Name of newCondition
	private string newConditionDescription = "New Condition";

	// Path where AllCondition.asset is Created
	private const string creationPath = "Assets/Resources/AllConditions.asset";
	// Constant value of ButtonWidth
	private const float buttonWidth = 30f;


	// Called when Script is Enabled
	private void OnEnable () {
		// Cache Reference to Target
		allConditions = (AllConditions)target;

		// If no Condition where Found
		if (allConditions.conditions == null) {
			// Create empty Condition
			allConditions.conditions = new Condition[0];
		}
		// If no ConditionEditors where found
		if (conditionEditors == null) {
			// Create the ConditionEditors
			CreateEditors ();
		}
	}


	// Called when Script is Disabled
	private void OnDisable () {
		// Go through all ConditionEditors
		for (int i = 0; i < conditionEditors.Length; i++) {
			// Destroy the ConditionEditors
			DestroyImmediate (conditionEditors[i]);
		}
		// Set ConditionEditors to Null
		conditionEditors = null;
	}


	// Set the Description of a Condition
	private static void SetAllConditionDescriptions () {
		// Create new Array with same Number of Elements as Conditions
		AllConditionDescriptions = new string[TryGetConditionsLength()];

		//Go through Array of ConditionDescriptions
		for (int i = 0; i < AllConditionDescriptions.Length; i++) {
			// Assign Description of Condition at same Index
			AllConditionDescriptions[i] = TryGetConditionAt(i).description;
		}
	}


	// Called when Inspector is Open (every Frame)
	public override void OnInspectorGUI () {
		// If Different Number of Editors to Conditions
		if (conditionEditors.Length != TryGetConditionsLength() ) {
			// Go through all Found Editors
			for (int i = 0; i < conditionEditors.Length; i++) {
				// Destroy all PreviousEditors (Found Editors)
				DestroyImmediate (conditionEditors[i]);
			}
			// Create new Editors
			CreateEditors ();
		}

		// Go through all the Conditions
		for (int i = 0; i < conditionEditors.Length; i++) {
			// Display all the Conditions
			conditionEditors[i].OnInspectorGUI ();
		}

		// If Condition where found
		if (TryGetConditionsLength () > 0) {
			// Add Space between Elements
			EditorGUILayout.Space ();
			EditorGUILayout.Space ();
		}

		// Start Horizontal Box for newConditions
		EditorGUILayout.BeginHorizontal ();

		// Get and Display Name of newCondition
		newConditionDescription = EditorGUILayout.TextField (GUIContent.none, newConditionDescription);

		//Display Button to Add newCondition to AllConditions
		if (GUILayout.Button ("+", GUILayout.Width (buttonWidth) ) ) {
			// Add newCondition to AllConditions
			AddCondition (newConditionDescription);
			// Reset newCondition Description
			newConditionDescription = "New Condition";
		}

		// End Horizontal Box for newConditions
		EditorGUILayout.EndHorizontal ();
	}


	// Function to Create Condition Editors
	private void CreateEditors () {
		// Create new Array for Editors (with same Lenght as ConditionArray)
		conditionEditors = new ConditionEditor[allConditions.conditions.Length];

		// Go through the EmptyArray
		for (int i = 0; i < conditionEditors.Length; i++) {
			// Create ConditionEditor
			conditionEditors[i] = CreateEditor (TryGetConditionAt (i) ) as ConditionEditor;
			// Set ConditionEditor to Correct Type
			conditionEditors[i].editorType = ConditionEditor.EditorType.AllConditionAsset;
		}
	}



	// Called when MenuItem is Selected
	[MenuItem("Assets/Create/AllConditions")]
	private static void CreateAllConditionsAsset () {
		// If AllCondition Found (already Created)
		if (AllConditions.Instance) {
			// Do Nothing
			return;
		}

		// Otherwise: Create Instance of AllConditions Object
		AllConditions instance = CreateInstance <AllConditions> ();
		// Make Asset for AllConditions
		AssetDatabase.CreateAsset (instance, creationPath);

		// Set a Singleton Instance
		AllConditions.Instance = instance;

		// Create new EmptyArray of Conditions
		instance.conditions = new Condition[0];
	}


	// Function to Add a Condition
	private void AddCondition (string description) {
		// If no AllConditions instance found
		if (!AllConditions.Instance) {
			// Display ErrorMessage
			Debug.Log ("AllConditions has not been Created yet.");
			// Stop Function
			return;
		}

		// Otherwise: Create a Condition based on the Description
		Condition newCondition = ConditionEditor.CreateCondition (description);
		// Set the Name of the newCondition
		newCondition.name = description;
		// Record all Operations on newCondition (so they can be Undone)
		Undo.RecordObject (newCondition, "Created new Condition");
		// Attach newCondition to AllCondition Asset
		AssetDatabase.AddObjectToAsset (newCondition, AllConditions.Instance);
		// Import Asset to recognize Joined Asset
		AssetDatabase.ImportAsset (AssetDatabase.GetAssetPath (newCondition) );
		// Add newCondition to AllConditions Array
		ArrayUtility.Add (ref AllConditions.Instance.conditions, newCondition);
		// Mark AllConditions as Dirty (Editor knows to SaveChanges on ProjectSave)
		EditorUtility.SetDirty (AllConditions.Instance);
		// Recreate ConditionDescriptions Array with newCondition
		SetAllConditionDescriptions ();
	}


	// 
	public static void RemoveCondition (Condition condition) {
		// If No AllConditions instance found
		if (!AllConditions.Instance) {
			// Display ErrorMessage
			Debug.Log ("AllConditions has not been Created yet.");
			// Stop Function
			return; 
		}

		// Record all Operations on AllConditions (so they can be Undone)
		Undo.RecordObject (AllConditions.Instance, "Removing condition");
		// Remove the Specified Condition from the AllConditions Array
		ArrayUtility.Remove (ref AllConditions.Instance.conditions, condition);
		// Destroy the Condition, including its Asset
		DestroyImmediate (condition, true);
		// Save the Asset to recognize the Change
		AssetDatabase.SaveAssets ();
		// Make AllConditions as Dirty (Editor knows to SaveChanges on ProjectSave)
		EditorUtility.SetDirty (AllConditions.Instance);
		// Recreate ConditionDescriptions Array without RemovedCondition
		SetAllConditionDescriptions ();
	}


	// Function to Find a Condition, returns the Index on wich Condition was found
	public static int TryGetConditionIndex (Condition condition) {
		// Go through all Conditions
		for (int i = 0; i < TryGetConditionsLength(); i++) {
			// If Condition was found
			if (TryGetConditionAt(i).hash == condition.hash) {
				// return the Index on wich the Condition was found
				return i;
			}
		}
		// Otherwise if Condition was Not Found, return -1
		return -1;
	}


	// Function to Find a Condition, returns the Condition at given Index
	public static Condition TryGetConditionAt (int index) {
		// Cache AllConditions Array
		Condition[] allConditions = AllConditions.Instance.conditions;

		// If AllConditions does not Exist
		if (allConditions == null || allConditions[0] == null) {
			// Return Empty
			return null;
		}

		// If given Index is out of Array (length)
		if (index >= allConditions.Length) {
			// Return first Condition 
			return allConditions [0];
		}
		// Otherwise return the Condition at given Index
		return allConditions[index];
	} 


	// Function to get Length of AllConditions Array
	public static int TryGetConditionsLength () {
		// If AllConditions does not Exist
		if (AllConditions.Instance.conditions == null) {
			// Return first of Index
			return 0;
		}
		// Otherwise Return the Length of the Array
		return AllConditions.Instance.conditions.Length;
	}

}
