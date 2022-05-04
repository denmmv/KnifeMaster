using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnifeLogic : MonoBehaviour
{
    [SerializeField] private data _data;
    [SerializeField] private GameObject _knifeGO;
    [SerializeField] private Rigidbody2D _knifeRB;
    [SerializeField] private BoxCollider2D _knifeCOL;
    private float _timeToHit;
    private Coroutine _knifeCoroutine;
    private Coroutine _knifeLose;
    public static UnityEvent KnifeHitEvent = new UnityEvent();
    public static UnityEvent LoseEvent = new UnityEvent();
    private void Awake()
    {
        _timeToHit = _data.TimeToHit;
    }
    private void Start()
    {
        KnifeController.ThrowKnifeEvent.AddListener(ThrowKnife);
    }
    private void ThrowKnife()
    {
        KnifeController.ThrowKnifeEvent.RemoveListener(ThrowKnife);
        _knifeCoroutine = StartCoroutine(KnifeCoroutine());
    }
    IEnumerator KnifeCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _timeToHit)
        {
            _knifeGO.transform.position = Vector3.Lerp(_knifeGO.transform.position, new Vector2(0, 2.5f), elapsedTime / _timeToHit);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator KnifeLose()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(_knifeGO);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Log"&& _knifeGO.tag!="KnifeOut")
        {
            StopCoroutine(_knifeCoroutine);
            _knifeGO.transform.SetParent(collision.transform);
            KnifeHitEvent.Invoke();
            Handheld.Vibrate();
            Destroy(this);
        }
        if (collision.gameObject.tag == "KnifeIn")
        {
            LoseEvent.Invoke();
            _knifeGO.tag = "KnifeOut";
            _knifeCOL.isTrigger = true;
            StopAllCoroutines();
            _knifeRB.constraints = RigidbodyConstraints2D.None;
            _knifeGO.transform.position = _knifeGO.transform.position;
            _knifeRB.velocity = new Vector2(0, 0);
            _knifeRB.AddForce(new Vector3(1,0,0) * 5000f);
            _knifeRB.AddTorque(-300f);
            _knifeLose = StartCoroutine(KnifeLose());
        }
    }
}
