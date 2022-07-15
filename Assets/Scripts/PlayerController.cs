using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Header("Movement")]
    public float speed = 5;
    public float jumpForce = 5;

    [Header("Bounds")]
    private float range = 5;

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
        Movement();
        Jump();
    }

    public void Movement()
    {
        //Move Forward
        transform.position += Vector3.forward * speed * Time.deltaTime;

        //Move Sides
        float horizontalInputs = Input.GetAxis("Horizontal");
        float verticalInputs = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * -speed * Time.deltaTime * horizontalInputs);

        //Bound
        if (transform.position.x > range)
        {
            transform.position = new Vector3(range, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -range)
        {
            transform.position = new Vector3(-range, transform.position.y, transform.position.z);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector3.up * jumpForce;
        }
    }
}
