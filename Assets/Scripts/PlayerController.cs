using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rigidbody;
    public float speed = 5;

    [Header("Bounds")]
    public float bounds;

    private void OnValidate()
    {
        if (rigidbody == null) rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;

        float horizontalInputs = Input.GetAxis("Horizontal");
        float verticalInputs = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInputs);
    }
}
