using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDManager : MonoBehaviour
{
    public Rigidbody rigidbody;
    public AudioSource audioSource01;
    public AudioSource audioSource02;
    public List<AudioClip> audioClips;

    private void OnValidate()
    {
        //if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
    }

    [NaughtyAttributes.Button]
    public void PutGravity()
    {
        rigidbody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("StatueTrigger"))
        {
            audioSource01.Play();
        }

        else if (collision.transform.CompareTag("Player"))
        {
            PlayerController.Instance.Dead();
            audioSource02.Play();
        }

    }
}
