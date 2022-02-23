using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MainBehaviour
{
    private Animator _anim;
    private Transform _groundChecker;
    [SerializeField] protected bool isMoving = true;
    [SerializeField] protected float playerSpeed = 2.0f;
    [SerializeField] protected float rotationSpeed = 2.0f;
    [SerializeField] protected float jumpHeight = 1.0f;
    [SerializeField] protected float GroundDistance = 0.2f;
    [SerializeField] protected LayerMask Ground;
    private bool _isGrounded = true;

    private void Start()
    {
        Physics.gravity *= 1.5f;
        _groundChecker = transform.GetChild(0);
        _anim = gameObject.GetComponent<Animator>();
    }

    public void SwitchIsMoving()
    {
        this.isMoving = !this.isMoving;
    }

    protected override void Update()
    {
        if (!isMoving) return;
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

    }

    void MovementAnimator(float horizontal, float vertical)
    {
        float maxspeed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));
        _anim.SetFloat("Speed_f", maxspeed);
    }
}
