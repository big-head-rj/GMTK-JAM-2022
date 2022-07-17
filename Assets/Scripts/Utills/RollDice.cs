using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;

public class RollDice : Singleton<RollDice>
{
    public Rigidbody rigidbody;
    public AudioSource audioSource;

    public float speedRoll = 3;
    public bool canRoll = false;
    public float startSFXDelay = 3;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canRoll)
        {
            //rigidbody.AddForce(Vector3.forward * 15 * Time.deltaTime);
            rigidbody.transform.position += Vector3.forward * 5 * Time.deltaTime;
            transform.Rotate(speedRoll, 0.0f, 0.0f);
            //Testando Github Guto
        }
    }

    public void CallDiceSFX()
    {
        Invoke(nameof(PlaySFX), startSFXDelay);
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }
}
