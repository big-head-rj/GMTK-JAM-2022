using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [Header("Buttons Animation")]
    public GameObject btnContainer;
    public Ease ease;
    public float timeBtnAnim;

    // Start is called before the first frame update
    void Start()
    {
        AnimationButtons();
    }

    public void PlayCameraAnimation()
    {


        StartRun();
    }

    public void AnimationButtons()
    {
        btnContainer.transform.DOScale(0, timeBtnAnim).SetEase(ease).From();
    }

    [NaughtyAttributes.Button]
    public void StartRun()
    {
        PlayerController.Instance.canRun = true;
        RollDice.Instance.canRoll = true;
    }
}
