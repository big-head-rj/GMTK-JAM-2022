using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableMagnetic : ItemCollectableBase
{
    public Collider collider;
    public bool collect = false;
    public float lerpSpeed = 5;
    public float minDistance = 1f;

    protected override void Collect()
    {
        OnCollect();
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        collider.enabled = false;
        collect = true;
        PlayerController.Instance.MagneticOn(true);
    }

    private void Update()
    {
        if (collect)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItens();
                Destroy(gameObject);
            }
        }
    }
}
