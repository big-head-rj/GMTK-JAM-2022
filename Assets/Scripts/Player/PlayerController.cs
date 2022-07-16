using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody rigidbody;

    [Header("Movement")]
    public float runSpeed = 5;
    public float sideSpeed = 5;
    float _currSpeed;
    public float jumpForce = 5;

    [Header("Jump Animation")]
    public float scaleX = .9f;
    public float scaleY = 1.1f;

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
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currSpeed = runSpeed;
        _currTurbo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        if (Input.GetKeyDown(KeyCode.S)) TurboPlayer();
    }

    [NaughtyAttributes.Button]
    public void StopRun()
    {
        runSpeed = 0;
    }
    
    [NaughtyAttributes.Button]
    public void BackRun()
    {
        runSpeed = 5;
    }

    public void Movement()
    {
        //Move Forward
        transform.position += Vector3.forward * runSpeed * Time.deltaTime;

        //Move Sides
        float horizontalInputs = Input.GetAxis("Horizontal");
        float verticalInputs = Input.GetAxis("Vertical");

        if(_isJumping == false) transform.Translate(Vector3.right * -sideSpeed * Time.deltaTime * horizontalInputs);

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

            transform.DOScaleX(scaleX, .2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
            transform.DOScaleY(scaleY, .2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);                           
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
        runSpeed = turboSpeed;
        yield return new WaitForSeconds(turboTime);
        runSpeed = _currSpeed;
        _currTurbo++;
        StopCoroutine(TurboCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Smash"))
        {
            Destroy(gameObject);
        }
    }
}
