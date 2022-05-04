using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsNSaveController : MonoBehaviour
{
    [SerializeField] private Text _currentScore;
    [SerializeField] private Text _currentStage;
    [SerializeField] private Text _currentApples;
    [SerializeField] private Text _recordStage;
    [SerializeField] private Text _recordScore;
    private int _recordStageInt;
    private int _recordScoreInt;
    private int _currentScoreInt;
    private int _currentStageInt;
    private int _currentApplesInt;
    private void Start()
    {
        _currentScoreInt = 0;
        _currentStageInt = 1;
        _recordStageInt = PlayerPrefs.GetInt("recordStage");
        _recordScoreInt = PlayerPrefs.GetInt("recordScore");
        KnifeLogic.KnifeHitEvent.AddListener(KnifeHit);
        AppleLogic.AppleBreakEvent.AddListener(GotApple);
        _currentApplesInt = PlayerPrefs.GetInt("apples");
        _recordStage.text = "STAGE " + PlayerPrefs.GetInt("recordStage");
        _recordScore.text = "SCORE " + PlayerPrefs.GetInt("recordScore");
        _currentStage.text = "Stage " + _currentStageInt;
        _currentScore.text = _currentScoreInt.ToString();
        _currentApples.text = _currentApplesInt.ToString();
    }
    public void NextStage()
    {
        _currentStageInt++;
        _currentStage.text = "Stage " + _currentStageInt;
    }
    public void KnifeHit()
    {
        _currentScoreInt++;
        _currentScore.text = _currentScoreInt.ToString();
    }
    public void LoseStage()
    {
        if (_currentScoreInt > _recordScoreInt)
        {
            PlayerPrefs.SetInt("recordScore", _currentScoreInt);
            _recordScore.text = "SCORE " + PlayerPrefs.GetInt("recordScore");
        }
        if (_currentStageInt>_recordStageInt)
        {
            PlayerPrefs.SetInt("recordStage", _currentStageInt);
            _recordStage.text = "STAGE " + PlayerPrefs.GetInt("recordStage");
        }
    }
    public void StatsNull()
    {
        _currentStageInt = 1;
        _currentStage.text = "Stage " + _currentStageInt;
        _currentScoreInt = 0;
        _currentScore.text = _currentScoreInt.ToString();
    }
    public void GotApple()
    {
        _currentApplesInt++;
        _currentApples.text = _currentApplesInt.ToString();
        PlayerPrefs.SetInt("apples", _currentApplesInt);
    }
    private void PPrefs()
    {
        int _recordStageSave = 0;
        int _recordScoreSave = 0;
        int _currentApplesSave = 0;
        PlayerPrefs.SetInt("recordStage", _recordStageSave);
        PlayerPrefs.SetInt("recordScore", _recordScoreSave);
        PlayerPrefs.SetInt("apples", _currentApplesSave);
    }
}
