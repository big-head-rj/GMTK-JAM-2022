using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [NaughtyAttributes.Button]
    public void StartGame()
    {
        //PlayerController.Instance.animator.SetTrigger("Run");
    }
}
