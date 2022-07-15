using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;

    [Header("Movement")]
    public float speed = 5;
    float _currSpeed;
    public float jumpForce = 5;

    [Header("Turbo")]
    public float turboSpeed;
    public int maxTurbos = 3;
    [SerializeField] int _currTurbo;
    public float turboTime;
    public bool _isJumping = false;

    [Header("Bounds")]
    private float range = 5;

    private void OnValidate()
    {
        if (rigidbody == null) rigidbody = GetComponentInChildren<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currSpeed = speed;
        _currTurbo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        if (Input.GetKeyDown(KeyCode.S)) TurboPlayer();
    }

    public void Movement()
    {
        //Move Forward
        transform.position += Vector3.forward * speed * Time.deltaTime;

        //Move Sides
        float horizontalInputs = Input.GetAxis("Horizontal");
        float verticalInputs = Input.GetAxis("Vertical");

        if(_isJumping == false) transform.Translate(Vector3.right * -speed * Time.deltaTime * horizontalInputs);

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
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            rigidbody.velocity = Vector3.up * jumpForce;
            _isJumping = true;
            Invoke(nameof(NotJumping), 1);
        }
    }

    public void NotJumping()
    {
        _isJumping = false;
    }

    public void TurboPlayer()
    {
        if (_currTurbo < maxTurbos)
        {
            StartCoroutine(TurboCoroutine());
            Debug.Log("Turbo");
        }
        else Debug.Log("Without turbo");
    }

    public IEnumerator TurboCoroutine()
    {
        speed = turboSpeed;
        yield return new WaitForSeconds(turboTime);
        speed = _currSpeed;
        _currTurbo++;
        StopCoroutine(TurboCoroutine());
    }
}
