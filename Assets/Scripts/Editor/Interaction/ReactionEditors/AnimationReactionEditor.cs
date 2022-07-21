using UnityEditor;

/// <summary>
/// Animation reaction editor.
/// 
/// </summary>

[CustomEditor ( typeof (AnimationReaction) )]
public class AnimationReactionEditor : ReactionEditor {
	// 
	protected override string GetFoldoutLabel () {
		// 
		return "Animation Reaction";
	}
}