using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmashTrapManager : MonoBehaviour
{
    public Transform trap;
    public float dir;
    public float moveDuration;
    public Ease ease;

    private TimerHelper _timerHelper;

    private void OnValidate()
    {
        if (_timerHelper == null) _timerHelper = GetComponentInParent<TimerHelper>();
        if (trap == null) trap = GetComponent<Transform>();
    }

    void Start()
    {
        StartCoroutine(SmashTrapCoroutine());
    }

    public void TrapClose()
    {
        transform.DOMoveX(1 * dir, moveDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    public IEnumerator SmashTrapCoroutine()
    {
        while (true)
        {
            TrapClose();
            yield return new WaitForSeconds(_timerHelper.randomTime);
        }
    }
}
