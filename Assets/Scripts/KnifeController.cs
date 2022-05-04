using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KnifeController : MonoBehaviour
{
    [SerializeField] private data _data;
    [SerializeField] private GameObject _knifePrefabGO;
    [SerializeField] private GameObject[] _knifesUI;
    [SerializeField] private Animation _knifeUIAnim;
    public static UnityEvent ThrowKnifeEvent = new UnityEvent();
    public static UnityEvent NextLevelEvent = new UnityEvent();
    private List<GameObject> _knifesUITemp = new List<GameObject>();
    public List<GameObject> _knifesTemp = new List<GameObject>();
    private float _knifeDelayToThrow;
    private bool _knifeEnableToThrow = false;
    private bool _knifeReadyToThrow = true;
    private int _knifeCountToWin;
    private int _knifeCountHit;
    private Coroutine _spawnKnife;
    private void Awake()
    {
        _knifeDelayToThrow = _data.KnifeDelay;
        _knifeCountHit = 0;
        KnifeLogic.KnifeHitEvent.AddListener(KnifeCounterCheckForWin);
        _knifesTemp.Add(GameObject.FindGameObjectWithTag("Knife"));
    }
    private void Update()
    {
        if (!_knifeEnableToThrow)
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _knifeReadyToThrow&&_knifeCountHit!=_knifeCountToWin)
            {
                ThrowKnife();
            }
        }
    }
    public void LoseLevel()
    {
        StopCoroutine(_spawnKnife);
        _knifeUIAnim.Play("KnifeCounterOut");
        _knifeReadyToThrow = false;
        _knifeEnableToThrow = false;
        _knifeCountHit = 0;
        foreach (GameObject Knife in _knifesTemp)
        {
            Destroy(Knife);
        }
        _knifesTemp.Clear();
    }
    public void ActivateKnifes()
    {
        _knifeEnableToThrow = true;
        _knifeReadyToThrow = true;
        if (_knifesTemp[0].activeSelf == false )
        {
            _knifesTemp[0].SetActive(true);
        }
    }
    public void KnifeCounter()
    {
        foreach (GameObject knife in _knifesUI)
        {
            knife.transform.GetChild(0).gameObject.SetActive(true);
            knife.SetActive(false);
        }
        _knifeCountToWin = Random.Range(3, 11);
        for(int x = 0; x < _knifeCountToWin; x++)
        {
            _knifesUI[x].SetActive(true);
            _knifesUITemp.Add(_knifesUI[x]);
        }
        while (_knifesTemp.Count < _knifeCountToWin)
        {
            GameObject z = Instantiate(_knifePrefabGO, this.transform);
            _knifesTemp.Add(z);
            z.SetActive(false);
        }
        _knifeUIAnim.Play("KnifeCounterIn");
    }
    public void FirstKnife()
    {
        GameObject z = Instantiate(_knifePrefabGO, this.transform);
        _knifesTemp.Add(z);
    }
    private void ThrowKnife()
    {
        _knifeReadyToThrow = false;
        _knifesTemp.Remove(_knifesTemp[0]);
        ThrowKnifeEvent.Invoke();
        _knifesUITemp[_knifesUITemp.Count - _knifeCountHit - 1].transform.GetChild(0).gameObject.SetActive(false);
        _knifeCountHit++;
        if (_knifeCountHit < _knifeCountToWin) { _spawnKnife = StartCoroutine(SpawnKnife()); }        
    }
    private void KnifeCounterCheckForWin()
    {
        if (_knifeCountHit == _knifeCountToWin) { NextLevelEvent.Invoke(); _knifeCountHit = 0; }
    }
    IEnumerator SpawnKnife()
    {
        yield return new WaitForSeconds(_knifeDelayToThrow);
        _knifesTemp[0].SetActive(true);
        StopCoroutine(_spawnKnife);
        _knifeReadyToThrow = true;
    }
}
