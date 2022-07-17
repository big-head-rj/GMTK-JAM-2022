using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrap : MonoBehaviour
{
    public Transform trap;
    public ParticleSystem particleSystem;
    public Collider collider;
    public MeshRenderer meshRenderer;
    public List<MeshRenderer> meshRenderers;

    public float timeToDestroy = 3;

    private void OnValidate()
    {
        if (trap == null) trap = GetComponent<Transform>();
        //if (particleSystem == null) particleSystem = GetComponentInChildren<ParticleSystem>();
        if (collider == null) collider = GetComponent<Collider>();
        //if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        if (meshRenderers == null) return;
    }

    public void HideTrap()
    {
        if (meshRenderers == null)
        {
            meshRenderer.enabled = false;
            collider.enabled = false;
        }

        else if (meshRenderers != null)
        {
            for (int i = 0; i <= meshRenderers.Count; i++)
            {
                meshRenderer.enabled = false;
                collider.enabled = false;
            }
        }

        Invoke(nameof(DestroyTrap), timeToDestroy);

    }

    public void Destroytrap()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Dice"))
        {
            HideTrap();
            //particleSystem.Play();
        }

    }
}
