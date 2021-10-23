using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int mode = 0;
    [SerializeField] TMPro.TMP_Text targetText;

    [SerializeField] TMPro.TMP_Text errorText;
    [SerializeField] IntEvent OnNewText;

    [SerializeField] Story activeStory;

    [SerializeField] SharedInt playerHealth;

    [SerializeField] NoArgEvent OnWrongPress;

    [SerializeField] GameState gameState;


    Mesh mesh;
    Vector3[] vertices;
    Color[] colours;
    private List<float> wordSpit;

    private List<float> characterTimePressed;
    private float timeOverall;
    private int cursor = 1;
    private int wrong = 0;
    private List<int> wrongCharacters;
    private int exposedRange = 0;

    private int textLength;

    [SerializeField] PlayableDirector playableDirector;

    public void PauseTimeline()
    {
        playableDirector.Pause();
        
    }

    private void PopulateText(string text)
    {
        targetText.text = "." + text;
        cursor = 1;
        targetText.SetAllDirty();

        targetText.ClearMesh();
        targetText.ForceMeshUpdate(false,true);
        
        textLength = targetText.text.Length;

        wordSpit = new List<float>();
        wordSpit.Capacity = textLength;
        timeOverall = 0f;

        characterTimePressed = new List<float>();
        characterTimePressed.Capacity = textLength;
        wrongCharacters = new List<int>();

        mesh=targetText.mesh;
        vertices = mesh.vertices;
        colours = mesh.colors;

        for(var x = 0; x < colours.Length; x++)
        {
            colours[x].a = 0f;
        }

    }

    private void Awake() {
        
        if(mode == 0) {
            PopulateText("");
        }

        if(mode == 1)
        {
            PopulateText(activeStory.GetText());
        }

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


        /*
        for (var i = cursor + 1; i < textLength; i++)
        {   
            item = targetText.textInfo.characterInfo[i];
            index = item.vertexIndex;

            vertices[index].x -= topLeftX;
            vertices[index+1].x -= topLeftX;
            vertices[index+2].x -= topLeftX;
            vertices[index+3].x -= topLeftX;
        }
        */

        cursor++;
    }

    private void OnTextCursorError()
    {
        var item = targetText.textInfo.characterInfo[cursor];
        int index = item.vertexIndex;
        
        errorText.text += item.character.ToString();
        ProgressCursor();
        OnWrongPress.FireEvent();
    }
    private void OnKeyPress(char key) 
        {

        if(gameState.Paused) {
            gameState.Intro = false;
            gameState.Paused = false;
            return;
        }

        if(gameState.Paused) {
            return;
        }

        var tmpCursor = cursor;


        if(cursor >= targetText.text.Length) return;

        if(
            (
                targetText.text[cursor] == ' ' ||
                targetText.text[cursor] == '.'
            ) && targetText.text[cursor] == key
        ) {
            ProgressCursor();
            return;
        }

        while(
            (
                targetText.text[cursor] == ' ' ||
                targetText.text[cursor] == '.'
            )
        )
        {
            ProgressCursor();
        }


        if(char.ToLower(targetText.text[cursor]) == char.ToLower(key)) {
            ProgressCursor();
        } else {
            OnTextCursorError();
        }

    }

    private void UpdateWrongVertices()
    {
        int wrongCount = 0;
        var badColour = new Color(1f,0f,0f,1f);

        foreach(int wrongCursor in wrongCharacters) {

            var item = targetText.textInfo.characterInfo[wrongCursor];
            int index = item.vertexIndex;

            Vector3 tl = item.topLeft;
            Vector3 tr = item.topRight;
            Vector3 bl = item.bottomLeft;
            Vector3 br = item.bottomRight;

            var floatWidth = (tr.x - tl.x) / 8;
            var floatHeight = (tl.y - bl.y) / 4;

            var xMovement = 60;

            colours[index] = badColour;
            colours[index+1] = badColour;
            colours[index+2] = badColour;
            colours[index+3] = badColour;

            vertices[index] = new Vector3(
                -1000 + ((wrongCount * xMovement) + xMovement),
                -400,
                0
            );

            vertices[index+1] = new Vector3(
                -1000 + ((wrongCount * xMovement) + xMovement),
                -400 +floatHeight,
                0
            );

            vertices[index+2] = new Vector3(
                -1000 + ((wrongCount * xMovement)),
                -400 +floatHeight,
                0
            );

            vertices[index+3] = new Vector3(
                -1000 + ((wrongCount * xMovement)),
                -400,
                0
            );     

            wrongCount++;
        }   
    }

    private void OnNewTextCallback(int data)
    {
        playableDirector.Play();

        playerHealth.Value = Mathf.Max(playerHealth.Value + 10, 100);

        int position = ((data & 0xfff00) >> 8);
        int length = data & 0xff;


        if(mode == 0) {
            exposedRange = length;
            PopulateText(
                activeStory.GetText().Substring(position, length)
            );
        }

        if(mode == 1) {
            exposedRange = position + length;
            timeOverall -= 1f;
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
        if(gameState.Paused) return;

        if(playableDirector.state == PlayState.Paused && cursor >= (exposedRange)) {
            PopulateText("");
            playableDirector.Play();
            return;
        }

        targetText.ForceMeshUpdate();
        timeOverall += Time.deltaTime;

        mesh = targetText.mesh;
        vertices = mesh.vertices;
        colours = mesh.colors;

        for(int w = 0; w < cursor; w++) 
        {
            int index = targetText.textInfo.characterInfo[w].vertexIndex;
            colours[index].a = 0;
            colours[index+1].a = 0;
            colours[index+2].a = 0;
            colours[index+3].a = 0;
        }

        for (int w = cursor; w < exposedRange; w++)
        {
            int index = targetText.textInfo.characterInfo[w].vertexIndex;
            if(!targetText.textInfo.characterInfo[w].isVisible) continue;

            vertices[index].x -= timeOverall * 300;
            vertices[index+1].x -=  timeOverall  * 300;
            vertices[index+2].x -=  timeOverall  * 300;
            vertices[index+3].x -=  timeOverall  * 300;
        }

        mesh.vertices = vertices;
        mesh.colors = colours;
        targetText.canvasRenderer.SetMesh(mesh);
    }
}
