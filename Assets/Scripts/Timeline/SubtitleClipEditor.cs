using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

[UnityEditor.CustomEditor(typeof(SubtitleClip))]
public class SubtitleClipEditor : UnityEditor.Editor {
    public override void OnInspectorGUI()
    {
        SubtitleClip clip = (SubtitleClip)this.target;

        DrawDefaultInspector();
 
        GUIStyle style = new GUIStyle ();
        style.richText = true;

        var text = clip.story.GetText().
                    Insert(clip.startPos + clip.length, "</b></color>").
                    Insert(clip.startPos, "<color=yellow><b>");

        var extraLength = "<color=yellow><b></b></color>".Length;

        var startPosition = Mathf.Clamp(clip.startPos - 100, 0, clip.startPos);
        var maxLen = text.Length > 118 ? 118 : text.Length;
        text = text.Substring(startPosition, maxLen);

        
        GUILayout.Label(
            text,
            style
        );

        if(GUILayout.Button("Click Button"))
        {

            clip.story.GetText().Substring(
                clip.startPos,
                clip.length
            ); 

            Debug.Log (clip.startPos);
        }        
    }        
}

#endif