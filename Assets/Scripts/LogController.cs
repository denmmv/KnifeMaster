using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LogController : MonoBehaviour
{
    [SerializeField] private data _data;
    [SerializeField] private Rigidbody2D _logRB;
    [SerializeField] private GameObject _logGO;
    [SerializeField] private CircleCollider2D _logCOL;
    [SerializeField] private Animator _logAnimator;
    [SerializeField] private Animation _logAnimation;
    [SerializeField] private GameObject _hitParticles;
    private Coroutine _logRotation;
    private float _logRotationSpeed;
    private Coroutine _nextLevel;
    public UnityEvent ActivateKnifesEvent;
    private void Start()
    {
        _logRotationSpeed = _data.LogRotationSpeed;
    }
    IEnumerator NextLevelCor()
    {
        _logCOL.isTrigger = true;
        List<GameObject> temp = new List<GameObject>();
        for (int i = 0; i < _logGO.transform.childCount; i++)
        {
            temp.Add(_logGO.transform.GetChild(i).gameObject);
        }
        _logGO.transform.DetachChildren();
        foreach (GameObject knife in temp)
        {
            Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(rb.position * 5000f);
            rb.AddTorque(Random.Range(10f, 30f));
        }
        Handheld.Vibrate();
        _logAnimator.Play("LogLevelComplete");
        yield return new WaitForSecondsRealtime(0.8f);
        _logAnimator.Play("LogSpawn");
        foreach (GameObject knife in temp)
        {
            Destroy(knife);
        }
        _logCOL.isTrigger = false;
    }
    public void ActivateKnifes()
    {
        ActivateKnifesEvent.Invoke();
    }
    public void NextLevel()
    {
        _nextLevel = StartCoroutine(NextLevelCor());
    }
    public void LoseLevel()
    {
        StopCoroutine(_logRotation);
        _logAnimator.Play("LogLoseLevel");
        List<GameObject> temp = new List<GameObject>();
        for (int i = 0; i < _logGO.transform.childCount; i++)
        {
            temp.Add(_logGO.transform.GetChild(i).gameObject);
        }
        foreach (GameObject obj in temp)
        {
            Destroy(obj);
        }
    }
    public void ActivateRotation()
    {
        _logRotation = StartCoroutine(LogRotation());
    }
    IEnumerator LogRotation()
    {
        while (true)
        {
            _logGO.transform.Rotate(new Vector3(0, 0, _logRotationSpeed));
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            collision.gameObject.tag = "KnifeIn";
            _logAnimation.Play("LogGotHit");
            Instantiate(_hitParticles, collision.transform.position, Quaternion.identity);
        }
    }
}
