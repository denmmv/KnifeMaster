using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AppleLogic : MonoBehaviour
{
    public static UnityEvent AppleBreakEvent = new UnityEvent();
    [SerializeField] private GameObject _appleGO;
    [SerializeField] private Animator _appleAnimator;
    [SerializeField] private Rigidbody2D _appleRB;
    private Coroutine _appleBreak;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Knife")
        {
            _appleBreak = StartCoroutine(AppleBreak());
        }
    }
    IEnumerator AppleBreak()
    {
        Handheld.Vibrate();
        _appleRB.constraints = RigidbodyConstraints2D.None;
        _appleGO.transform.parent = null;
        _appleAnimator.Play("AppleBreak");
        _appleRB.AddForce(_appleRB.position * 500f);
        _appleRB.AddForce((_appleGO.transform.position - new Vector3(52.6f, 4, 0)) *20f);
        _appleRB.AddTorque(Random.Range(15f, 35f));
        AppleBreakEvent.Invoke();
        yield return new WaitForSecondsRealtime(1f);
        Destroy(_appleGO);
    }
}
