using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbodyPlayer;
    private Animator _anim;
    private Transform _groundChecker;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float GroundDistance = 0.2f;
    [SerializeField] private LayerMask Ground;
    Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;

    private void Start()
    {
        Physics.gravity *= 1.5f;
        _rigidbodyPlayer = gameObject.GetComponent<Rigidbody>();
        _groundChecker = transform.GetChild(0);
        _anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        
        InputAxis();

        MovementAnimator();

        if (_inputs != Vector3.zero)
            transform.forward = _inputs;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbodyPlayer.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        _rigidbodyPlayer.MovePosition(_rigidbodyPlayer.position + _inputs * playerSpeed * Time.fixedDeltaTime);
    }
    Vector3 InputAxis()
    {
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal");
        _inputs.z = Input.GetAxis("Vertical");
        return _inputs;
    }
    void MovementAnimator()
    {
        float maxspeed = Mathf.Max(Mathf.Abs(_inputs.x), Mathf.Abs(_inputs.z));
        _anim.SetFloat("Speed_f", maxspeed);
    }
}
