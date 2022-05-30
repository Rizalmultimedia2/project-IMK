using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Movement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 14f;
    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private bool usePhysics = true;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource runningSound;
    float turnSmoothVelocity;

    private Camera _mainCamera;
    private Rigidbody _rb;
    private Controls _controls;
    private Vector3 playerVelocity;
    private Animator _animator;
    private CharacterController controller;
    GameObject face;
    private Vector3 position;
    private float timer;
    private float jumpCooldown = 0.1f;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsPunch = Animator.StringToHash("isPunch");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int IsSneak = Animator.StringToHash("isSneak");


    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        _controls.Enable();
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        _controls.Disable();
    }

    private void Start()
    {
        face = GameObject.Find("face");
        position = face.transform.position;
        _mainCamera = Camera.main;
        _rb = gameObject.GetComponent<Rigidbody>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void Update()
    {
        if (usePhysics)
        {
            return;
        }

        Vector2 input = _controls.Player.Move.ReadValue<Vector2>();

        if (_controls.Player.Move.IsPressed())
        {
            _animator.SetBool(IsWalking, true);
            Vector3 target = HandleInput(input, playerSpeed);
            Move(target);
        }
        else
            _animator.SetBool(IsWalking, false);

        timer += Time.deltaTime;
        if (timer < jumpCooldown) return;

        if (_controls.Player.Jump.IsPressed())
        {
            _animator.SetBool(IsJumping, true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            playerVelocity.y += gravityValue * Time.deltaTime;
            Vector3 targetJump = transform.position + playerVelocity * Time.deltaTime;
            Move(targetJump);
            timer = 0f;
        }
        else
        {
            _animator.SetBool(IsJumping, false);
        }

        if (_controls.Player.Sneak.IsPressed())
        {
            _animator.SetBool(IsSneak, true);

        }
        else
        {
            _animator.SetBool(IsSneak, false);
        }

        if (_controls.Player.Running.IsPressed() && _controls.Player.Move.IsPressed())
        {
            _animator.SetBool(IsRunning, true);
            Vector3 target = HandleInput(input, playerSpeed + 2f);
            MovePhysics(target);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
        playerVelocity.y = 0;

    }

    public void FixedUpdate()
    {
        if (!usePhysics)
        {
            return;
        }

        Vector2 input = _controls.Player.Move.ReadValue<Vector2>();

        if (_controls.Player.Move.IsPressed())
        {
            _animator.SetBool(IsWalking, true);
            Vector3 target = HandleInput(input, playerSpeed);
            walkSound.Play();
            MovePhysics(target);
        }
        else
        {
            _animator.SetBool(IsWalking, false);
        }

        if (_controls.Player.Jump.IsPressed() && !_animator.GetBool(IsSneak))
        {
            _animator.SetBool(IsJumping, true);
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            playerVelocity.y += gravityValue * Time.deltaTime;
            Vector3 targetJump = transform.position + playerVelocity * Time.deltaTime;
            MovePhysics(targetJump);
        }
        else
        {
            _animator.SetBool(IsJumping, false);
        }

        if (_controls.Player.Sneak.triggered)
        {
            if (_animator.GetBool(IsSneak))
            {
                _animator.SetBool(IsSneak, false);
                Vector3 jongkok;
                jongkok.z = 0;
                jongkok.x = 0;
                jongkok.y = -0.6f;
                Vector3 targetJongkok = face.transform.position - jongkok;
                face.transform.position = targetJongkok;
            }
            else
            {
                _animator.SetBool(IsSneak, true);
                Vector3 jongkok;
                jongkok.z = 0;
                jongkok.x = 0;
                jongkok.y = 0.6f;
                Vector3 targetJongkok = face.transform.position - jongkok;
                face.transform.position = targetJongkok;
            }
            Debug.Log("masuk press");
        }

        if (_controls.Player.Running.IsPressed() && _controls.Player.Move.IsPressed() && !_animator.GetBool(IsSneak))
        {
            _animator.SetBool(IsRunning, true);
            Vector3 target = HandleInput(input, playerSpeed + 1f);
            runningSound.Play();
            MovePhysics(target);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }

        playerVelocity.y = 0;

    }

    private Vector3 HandleInput(Vector2 input, float speed)
    {
        Vector3 forward = _mainCamera.transform.forward;
        Vector3 right = _mainCamera.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = right * input.x + forward * input.y;

        // if (direction != Vector3.zero)
        // {
        //     Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        // }

        return transform.position + direction * speed * Time.deltaTime;
    }

    private void Move(Vector3 target)
    {
        transform.position = target;

    }

    private void MovePhysics(Vector3 target)
    {
        transform.position = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _animator.SetBool(IsJumping, false);
        }
    }
}
