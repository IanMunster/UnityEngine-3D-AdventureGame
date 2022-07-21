using UnityEditor;

/// <summary>
/// PickedUpItem reaction editor.
/// 
/// </summary>

[CustomEditor ( typeof (PickedUpItemReaction) )]
public class PickedUpItemReactionEditor : ReactionEditor {
	// 
	protected override string GetFoldoutLabel () {
		// 
		return "Picked Up Item Reaction";
	}
}