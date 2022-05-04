using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuShort : MonoBehaviour
{
    [SerializeField] private Animation _mainMenu;
public void MainMenuOut()
    {
        _mainMenu.Play("MainMenuOut");
    }
public void MainMenuIn()
    {
        _mainMenu.Play("MainMenuIn");
    }
}
