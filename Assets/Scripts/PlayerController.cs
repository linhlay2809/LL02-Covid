using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MainBehaviour
{
    [SerializeField] protected GameObject camera;
    private Rigidbody _rigidbodyPlayer;
    private Animator _anim;
    private Transform _groundChecker;
    [SerializeField] protected float playerSpeed = 2.0f;
    [SerializeField] protected float rotationSpeed = 2.0f;
    [SerializeField] protected float jumpHeight = 1.0f;
    [SerializeField] protected float GroundDistance = 0.2f;
    [SerializeField] protected LayerMask Ground;
    private bool _isGrounded = true;

    private void Start()
    {
        Physics.gravity *= 1.5f;
        _rigidbodyPlayer = gameObject.GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        _anim = gameObject.GetComponent<Animator>();
    }

    protected override void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection.Normalize();
        transform.Translate(moveDirection * playerSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        MovementAnimator(horizontalInput, verticalInput);

        //if (_inputs != Vector3.zero)
        //    transform.forward = _inputs;

        //if (Input.GetButtonDown("Jump") && _isGrounded)
        //{
        //    _rigidbodyPlayer.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        //}
    }

    void MovementAnimator(float horizontal, float vertical)
    {
        float maxspeed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
        _anim.SetFloat("Speed_f", maxspeed);
    }
}
