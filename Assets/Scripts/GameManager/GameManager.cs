using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;

    [Header("Buttons Animation")]
    public GameObject btnContainer;
    public Ease ease;
    public float timeBtnAnim;

    //[Header("Music Manager")]

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
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
        RollDice.Instance.CallDiceSFX();
    }

    public void RestartGame(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
