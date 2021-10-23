using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
   [SerializeField] GameState gameState;
   public void StartGame()
   {
       gameState.Paused = true;
       gameState.Intro = true;
       UnityEngine.SceneManagement.SceneManager.LoadScene(
           "SampleScene"
       );
   }
}
