using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnidePlaceShort : MonoBehaviour
{
    [SerializeField] private Animation _knifePlace;
    public UnityEvent ActivateKnifesEvent;
    private Coroutine _knifePlaceOut;
    public void KnifePlaceOut()
    {
        _knifePlaceOut = StartCoroutine(KnifePlaceOutDelay(0f));
    }
    public void KnifePlaceIn()
    {
        _knifePlace.Play("KnifePlaceIn");
    }
    public void KnifePlaceOutWithDelay()
    {
        _knifePlaceOut = StartCoroutine(KnifePlaceOutDelay(1f));
    }
    IEnumerator KnifePlaceOutDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        _knifePlace.Play("KnifePlaceOut");
    }
    public void ActivateKnifes()
    {
        ActivateKnifesEvent.Invoke();
    }
}
