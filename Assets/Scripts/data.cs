using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Data", menuName = "Data", order = 51)]
public class data : ScriptableObject
{
    [SerializeField] private float _timeToHit;
    [SerializeField] private float _knifeDelay;
    [SerializeField] private float _logRotationSpeed;
    [SerializeField] private int _chanceApple;
    public float TimeToHit
    {
        get
        {
            return _timeToHit;
        }
    }
    public float KnifeDelay
    {
        get
        {
            return _knifeDelay;
        }
    }
    public float LogRotationSpeed
    {
        get
        {
            return _logRotationSpeed;
        }
    }
    public int ChanceApple
    {
        get
        {
            return _chanceApple;
        }
    }
}
