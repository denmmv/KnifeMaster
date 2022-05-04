using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    public UnityEvent StartMainMenuEvent;
    public UnityEvent StopMainMenuEvent;
    public UnityEvent NextLevelEvent;
    public UnityEvent LoseLevelEvent;
    private void Start()
    {
        KnifeLogic.LoseEvent.AddListener(LoseGame);
        KnifeController.NextLevelEvent.AddListener(NextLevel);
    }
    private void LoseGame()
    {
        LoseLevelEvent.Invoke();
    }
    public void StartMainMenu()
    {
        StartMainMenuEvent.Invoke();
    }
    private void NextLevel()
    {
        NextLevelEvent.Invoke();
    }
    public void StopMainMenu()
    {
        StopMainMenuEvent.Invoke();
    }
}
