using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanelShort : MonoBehaviour
{
    [SerializeField] private GameObject _deadPanel;
    [SerializeField] private Animation _deadPanelAnim;
    public void DeadPanelIn()
    {
        _deadPanel.SetActive(true); 
        _deadPanelAnim.Play("DeadPanelIn");
    }
    public void DeadPanelOut()
    {
        _deadPanelAnim.Play("DeadPanelOut");
        _deadPanel.SetActive(false);
    }
}
