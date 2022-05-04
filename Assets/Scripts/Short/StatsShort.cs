using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsShort : MonoBehaviour
{
    [SerializeField] private Animation _statsAnimation;
    public void StatsIn()
    {
        _statsAnimation.Play("StatsIn");
    }
    public void StatsOut()
    {
        _statsAnimation.Play("StatsOut");
    }
    public void StatsHide()
    {
        _statsAnimation.Play("StatsHide");
    }
}
