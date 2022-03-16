using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MainBehaviour
{
    private Animator _anim;
    [SerializeField] protected bool isMoving = true;
    [SerializeField] protected float playerSpeed = 2.0f;
    [SerializeField] protected float rotationSpeed = 2.0f;

    private void Start()
    {
        Physics.gravity *= 1.5f;
        _anim = gameObject.GetComponent<Animator>();
    }

    public void SwitchIsMoving()
    {
        this.isMoving = !this.isMoving;
    }

    protected override void Update()
    {
        if (!isMoving)
        {
            _anim.SetFloat("Speed_f", 0f);
            return;
        }
        

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection.Normalize();
        transform.Translate(playerSpeed * Time.deltaTime * moveDirection, Space.World);

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
        //if (maxspeed > 0)
        //    _anim.SetFloat("Speed_f", 1);
        //else
        //    _anim.SetFloat("Speed_f", 0);
    }
}
