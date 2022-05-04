using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogObjectsController : MonoBehaviour
{
    [SerializeField] private data _data;
    [SerializeField] private GameObject _logGO;
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private float _radiusApple;
    [SerializeField] private float _radiusKnife;
    private int ChanceApple;
    private Coroutine _setParentCoroutine;
    private List<GameObject> _tempParent = new List<GameObject>();
    private Coroutine _spawnApples;
    private Coroutine _spawnKnifes;
    private void Awake()
    {
        ChanceApple = _data.ChanceApple;
    }
    public void SpawnObjects()
    {
        _spawnApples = StartCoroutine(SpawnApples(0));
        _spawnKnifes = StartCoroutine(SpawnKnifes(0));
        _setParentCoroutine = StartCoroutine(SetParentCoroutine(1.2f));
    }
    public void SpawnObjectsWithDelay()
    {
        _spawnApples = StartCoroutine(SpawnApples(0.9f));
        _spawnKnifes = StartCoroutine(SpawnKnifes(0.9f));
        _setParentCoroutine = StartCoroutine(SetParentCoroutine(2f));
    }
    IEnumerator SpawnApples(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        if (Random.Range(0, 100) < ChanceApple)
        {
            int ang = Random.Range(0, 359);
            Vector3 posApple;
            posApple.x = _logGO.transform.position.x + _radiusApple * Mathf.Sin(ang * Mathf.Deg2Rad);
            posApple.y = _logGO.transform.position.y + _radiusApple * Mathf.Cos(ang * Mathf.Deg2Rad);
            posApple.z = _logGO.transform.position.z;
            Quaternion _tempQuaternion = Quaternion.Euler(0, 0, -ang);
            GameObject x;
            x = Instantiate(_applePrefab, posApple, _tempQuaternion) ;
            _tempParent.Add(x);
        }

    }
    IEnumerator SpawnKnifes(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        int r = Random.Range(1, 4);
        for (int i = 0; i < r; i++)
        {
            int ang = Random.Range(0, 360);
            Vector3 posKnife;
            posKnife.x = _logGO.transform.position.x + _radiusKnife * Mathf.Sin(ang * Mathf.Deg2Rad);
            posKnife.y = _logGO.transform.position.y + _radiusKnife * Mathf.Cos(ang * Mathf.Deg2Rad);
            posKnife.z = 0;
            Quaternion _tempQuaternion = Quaternion.Euler(0, 0, -ang + 180);
            GameObject x = Instantiate(_knifePrefab, posKnife, _tempQuaternion);
            _tempParent.Add(x);
        }

    }
    IEnumerator SetParentCoroutine(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        foreach(GameObject child in _tempParent)
        {
            child.transform.SetParent(_logGO.transform);
        }
        _tempParent.Clear();
        StopCoroutine(_setParentCoroutine);
    }
}
