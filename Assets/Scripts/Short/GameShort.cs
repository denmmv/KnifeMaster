using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShort : MonoBehaviour
{
    [SerializeField] private GameObject _logPlaceGO;
    [SerializeField] private Animator _logAnimator;
public void MainMenuOut()
    {
        _logPlaceGO.SetActive(true);
        _logAnimator.Play("LogSpawn");
    }
public void MainMenuIn()
    {
        _logPlaceGO.SetActive(false);
    }
}
