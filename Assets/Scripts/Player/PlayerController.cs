using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using DG.Tweening;

// TIRAR OS HARDCODES

public class PlayerController : Singleton<PlayerController>
{
    public Rigidbody rigidbody;
    public Animator animator;
    //public ParticleSystem particleSystem;
    public AudioSource audioSource;
    public List<AudioClip> sfxPlayer;

    [Header("Movement")]
    public float runSpeed = 5;
    public float sideSpeed = 5;
    float _currRunSpeed;
    float _currSideSpeed;
    [Range(1, 4)]
    public float walkSpeed = 3;

    //float _currSpeed;
    public float jumpForce = 5;
    public bool canRun = false;

    [Header("Jump Animation")]
    public float scaleX = .9f;
    public float scaleY = 1.1f;
    public float delayBetweensJumps = 1.5f;
    public bool _isJumping = false;

    [Header("Turbo")]
    public float turboSpeed;
    public int maxTurbos = 3;
    public int _currTurbo;
    public float turboTime;
    bool _turboOn = false;

    [Header("Magnetic Powerup")]
    public Transform magneticCollider;
    public float magneticSize;
    public float magneticTime;
    public bool _hasMagnetic = false;

    [Header("Bounds")]
    private float range = 5.6f;

    public bool _isAlive = true;

    private void OnValidate()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (audioSource == null) audioSource = GetComponentInChildren<AudioSource>();
        //if (particleSystem == null) particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currRunSpeed = runSpeed;
        _currSideSpeed = sideSpeed;
        _currTurbo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.z == 0) animator.SetTrigger("Idle");
        if (Input.GetKeyUp(KeyCode.S) && !_turboOn) TurboPlayer();
        if (Input.GetKey(KeyCode.W)) Walk();
        if (Input.GetKeyUp(KeyCode.W)) BackRun();
    }

    private void FixedUpdate()
    {
        if (canRun)
        {
            Movement();
            if (rigidbody.velocity.z > 0) animator.SetTrigger("Run");
            Jump();
        }
    }


    #region === MOVEMENTS ===

    public void Movement()
    {
        //Move Forward
        transform.position += Vector3.forward.normalized * _currRunSpeed * Time.deltaTime;
        //SFXPool.Instance.Play(SFXType.FOOTSTEPS);

        //Move Sides
        float horizontalInputs = Input.GetAxis("Horizontal");
        float verticalInputs = Input.GetAxis("Vertical");

        if (_isJumping == false) transform.Translate(Vector3.right.normalized * -_currSideSpeed * Time.deltaTime * horizontalInputs);

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
        if (Input.GetKeyUp(KeyCode.Space) && _isJumping == false)
        {
            rigidbody.velocity = Vector3.up * jumpForce * 50 * Time.deltaTime;
            SFXPool.Instance.Play(SFXType.JUMP_02);
            //animator.SetTrigger("Jump");
            _isJumping = true;
            Invoke(nameof(NotJumping), delayBetweensJumps);

            transform.DOScaleX(scaleX, .2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
            transform.DOScaleY(scaleY, .2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack);
        }
    }

    public void NotJumping()
    {
        _isJumping = false;
        animator.SetTrigger("Run");
    }

    [NaughtyAttributes.Button]
    public void StopRun()
    {
        runSpeed = 0;
    }

    [NaughtyAttributes.Button]
    public void BackRun()
    {
        _currRunSpeed = runSpeed;
        _currSideSpeed = sideSpeed;
        animator.speed = 1;
    }

    public void Walk()
    {
        _currRunSpeed = walkSpeed;
        _currSideSpeed = walkSpeed;
        animator.speed = .5f;
    }
    #endregion

    #region === HEALTH ===
    public void Dead()
    {
        EnableRagDoll();
        _isAlive = false;
        canRun = false;
        SFXPool.Instance.Play(SFXType.DEATH_03);
        //particleSystem.Stop();
        OnDead();
    }

    public void OnDead()
    {
        runSpeed = 0;
        
        animator.SetTrigger("Die");
        Invoke(nameof(ShowEndGameScreen), 5);
    }

    public void EnableRagDoll()
    {
        rigidbody.isKinematic = true;
        rigidbody.detectCollisions = false;
    }

    public void ShowEndGameScreen()
    {
        GameManager.Instance.EndGame();
    }
    #endregion

    #region === POWERUPS ===
    public void TurboPlayer()
    {
        if (_currTurbo < maxTurbos && !_isJumping)
        {
            StartCoroutine(TurboCoroutine());
            _currTurbo++;
            ItemManager.Instance.RemoveTurbo();
        }
        else Debug.Log("Without turbo");
    }

    public IEnumerator TurboCoroutine()
    {
        _turboOn = true;
        _currRunSpeed = turboSpeed;
        SFXPool.Instance.Play(SFXType.USE_TURBO_06);
        yield return new WaitForSeconds(turboTime);
        _currRunSpeed = runSpeed;
        _turboOn = false;
        StopCoroutine(TurboCoroutine());
    }

    public void MagneticOn(bool b = false)
    {
        //b = _hasMagnetic;
        if (b == true) StartCoroutine(MagneticCoroutine());
    }

    public IEnumerator MagneticCoroutine()
    {
        magneticCollider.transform.DOScaleX(5, 1);
        yield return new WaitForSeconds(magneticTime);
        magneticCollider.transform.DOScaleX(1, 1);
        MagneticOn(false);
        StopCoroutine(MagneticCoroutine());
        //_hasMagnetic = false;
    }

    #endregion
}
