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

        float yOffset = 0;

        foreach (var item in targetText.textInfo.characterInfo)
        {
            int index = item.vertexIndex;

            colours[index].a = 0f;
            colours[index+1].a = 0f;
            colours[index+2].a = 0f;
            colours[index+3].a = 0f;


            if(item.character == ' ') {
                yOffset = 0; //Mathf.Sin(item.index) * 50;
            } else {
                vertices[index].y += yOffset;
                vertices[index+1].y += yOffset;
                vertices[index+2].y += yOffset;
                vertices[index+3].y += yOffset;
            }
        }
        
        mesh.vertices = vertices;
        mesh.colors = colours;
        targetText.canvasRenderer.SetMesh(mesh);
    }

    private void OnEnable() {
        OnNewText.Callback += OnNewTextCallback;  
        UnityEngine.InputSystem.Keyboard.current.onTextInput += OnKeyPress;
    }
    private void OnDisable() {
        OnNewText.Callback -= OnNewTextCallback;    
        UnityEngine.InputSystem.Keyboard.current.onTextInput -= OnKeyPress;
    }

    private void ProgressCursor()
    {
        var item = targetText.textInfo.characterInfo[cursor];
        int index = item.vertexIndex;

        colours[index].a = 0f;
        colours[index+1].a = 0f;
        colours[index+2].a = 0f;
        colours[index+3].a = 0f;

        var right = Vector3.right;

        item = targetText.textInfo.characterInfo[cursor];
        index = item.vertexIndex;
        
        float topLeftX = item.topRight.x - item.topLeft.x;

        for (var i = cursor + 1; i < textLength; i++)
        {   
            item = targetText.textInfo.characterInfo[i];
            index = item.vertexIndex;

            vertices[index].x -= topLeftX;
            vertices[index+1].x -= topLeftX;
            vertices[index+2].x -= topLeftX;
            vertices[index+3].x -= topLeftX;
        }

        cursor++;
    }
    private void OnKeyPress(char key) 
    {
        if(
            char.ToLower(targetText.text[cursor]) == char.ToLower(key) || 
            (targetText.text[cursor] == ' ' && key != ' ' && targetText.text[cursor+1] == char.ToLower(key))
        ) {
            //targetText.text = targetText.text.Substring(1);
            if(
                (targetText.text[cursor] == ' ' && key != ' ' && targetText.text[cursor+1] == char.ToLower(key))
            ) {
                ProgressCursor();
            }
            ProgressCursor();
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

        for (int w = cursor; w < exposedRange; w++)
        {
            int index = targetText.textInfo.characterInfo[w].vertexIndex;
            vertices[index].x -= Time.deltaTime * 100;
            vertices[index+1].x -= Time.deltaTime * 100;
            vertices[index+2].x -= Time.deltaTime * 100;
            vertices[index+3].x -= Time.deltaTime * 100;

        }

        mesh.vertices = vertices;
        mesh.colors = colours;
        targetText.canvasRenderer.SetMesh(mesh);
    }
}
