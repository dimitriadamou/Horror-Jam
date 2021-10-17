using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMPro.TMP_Text targetText;
    [SerializeField] StringEvent OnNewText;

    Mesh mesh;
    Vector3[] vertices;
    private List<int> wordIndexes;
    private List<int> wordLengths;

    private List<float> wordSpit;

    private void OnEnable() {
        OnNewText.Callback += OnNewTextCallback;  
    }

    private void OnDisable() {
        OnNewText.Callback -= OnNewTextCallback;    
    }

    private void OnNewTextCallback(string text)
    {
        targetText.text += text;
        targetText.ForceMeshUpdate();

        string fullText = targetText.text;
        wordIndexes = new List<int>{0};
        wordLengths = new List<int>();

        if(wordSpit == null) {
            wordSpit = new List<float>();
        }

        for(int index = fullText.IndexOf(' '); index > -1; index = fullText.IndexOf(' ', index + 1))
        {
            wordLengths.Add(index - wordIndexes[wordIndexes.Count - 1]);
            wordIndexes.Add(index + 1);
            wordSpit.Add(1f);
        }

        wordLengths.Add(fullText.Length - wordIndexes[wordIndexes.Count - 1]);
        wordSpit.Add(1f);
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
        if(wordIndexes == null) return;

        targetText.ForceMeshUpdate();
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
    }
}
