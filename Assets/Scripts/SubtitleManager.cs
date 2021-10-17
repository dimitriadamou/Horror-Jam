using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMPro.TMP_Text targetText;
    [SerializeField] IntEvent OnNewText;

    [SerializeField] Story activeStory;

    [SerializeField] NoArgEvent OnWrongPress;

    Mesh mesh;
    Vector3[] vertices;
    Color[] colours;
    private List<float> wordSpit;

    private int cursor = 0;
    private int exposedRange = 0;

    private int textLength;

    private void Awake() {
        targetText.text = activeStory.GetText();
        targetText.ForceMeshUpdate(false,true);
        wordSpit = new List<float>();
        wordSpit.Capacity = targetText.text.Length;
        textLength = targetText.text.Length;

        mesh=targetText.mesh;
        vertices=mesh.vertices;
        colours = mesh.colors;

        foreach (var item in targetText.textInfo.characterInfo)
        {
            int index = item.vertexIndex;

            colours[index].a = 0f;
            colours[index+1].a = 0f;
            colours[index+2].a = 0f;
            colours[index+3].a = 0f;
        }


        Debug.Log(mesh.colors.Length);
        Debug.Log(mesh.colors.ToString());
        
    }

    private void OnEnable() {
        OnNewText.Callback += OnNewTextCallback;  
        UnityEngine.InputSystem.Keyboard.current.onTextInput += OnKeyPress;
    }

    private void OnDisable() {
        OnNewText.Callback -= OnNewTextCallback;    
        UnityEngine.InputSystem.Keyboard.current.onTextInput -= OnKeyPress;
    }

    private void OnKeyPress(char key) 
    {
        if(targetText.text[cursor] == key) {
            //targetText.text = targetText.text.Substring(1);
            var item = targetText.textInfo.characterInfo[cursor];
            int index = item.vertexIndex;

            colours[index].a = 0f;
            colours[index+1].a = 0f;
            colours[index+2].a = 0f;
            colours[index+3].a = 0f;

            var right = Vector3.right;

            for (var i = cursor; i < textLength; i++)
            {   
                item = targetText.textInfo.characterInfo[i];
                index = item.vertexIndex;


                vertices[index] -= right * 18;
                vertices[index+1] -= right * 18;
                vertices[index+2] -= right * 18;
                vertices[index+3] -= right * 18;
            }

            cursor++;
        } else {
            OnWrongPress.FireEvent();
        }
    }

    private void OnNewTextCallback(int data)
    {
        int position = (data & 0xfff00) >> 8;
        int length = data & 0xff;

        exposedRange = position + length;

        for (int w = position; w < exposedRange; w++)
        {
            var item = targetText.textInfo.characterInfo[w];
            int index = item.vertexIndex;

            colours[index].a = 1f;
            colours[index+1].a = 1f;
            colours[index+2].a = 1f;
            colours[index+3].a = 1f;
        }

    }

    // Update is called once per frame


    Vector3 MoveWords(int wordSpitIndex)
    {
        if(wordSpit[wordSpitIndex] <= 0) return Vector3.zero;

        var lerpT = Mathf.Clamp(wordSpit[wordSpitIndex] - Time.deltaTime * 5,0 , 1);
        wordSpit[wordSpitIndex] = lerpT;

        var y = Mathf.Lerp(
            0,
            -800,
            lerpT
        );

        var x = Mathf.Lerp(
            0,
            400,
            lerpT
        );


        return new Vector3(x, y, 0f);
    }

    void Update()
    {

        targetText.ForceMeshUpdate();
        mesh.vertices = vertices;
        mesh.colors = colours;
        targetText.canvasRenderer.SetMesh(mesh);
        /*
        if(wordIndexes == null) return;

        mesh = targetText.mesh;
        vertices = mesh.vertices;
        for (int w = 0; w < wordIndexes.Count; w++)
        {
            int wordIndex = wordIndexes[w];

            var offset = MoveWords(w);

            for (int i = 0; i < wordLengths[w]; i++)
            {
                TMPro.TMP_CharacterInfo c = targetText.textInfo.characterInfo[wordIndex+i];
                int index = c.vertexIndex;

                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
        }
        mesh.vertices = vertices;
        targetText.canvasRenderer.SetMesh(mesh);   
        */     
    }
}
