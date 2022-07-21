using UnityEditor;

/// <summary>
/// Scene reaction editor.
/// 
/// </summary>

[CustomEditor ( typeof (SceneReaction) )]
public class SceneReactionEditor : ReactionEditor {
	// 
	protected override string GetFoldoutLabel () {
		// 
		return "Scene Reaction";
	}
}