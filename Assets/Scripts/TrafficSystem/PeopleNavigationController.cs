using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleNavigationController : MainBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected float movementSpeed = 2f;
    [SerializeField] protected float rotationSpeed = 120f;
    [SerializeField] protected float stopDistance = 2.5f;
    [SerializeField] protected Vector3 destination;
    [SerializeField] protected bool reachedDestination;
    [SerializeField] protected bool isMoving = true;
    protected Rigidbody rb;

    // Lấy giá trị isMoving
    public bool IsMoving()
    {
        return this.isMoving;
    }

    // Lấy giá trị reachedDestination
    public bool ReachedDestination()
    {
        return this.reachedDestination;
    }

    // Set giá trị bool cho isMoving
    public void SetIsMoving(bool value)
    {
        this.isMoving = value;
        rb.isKinematic = !value;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAnimatior();
    }

    protected override void Awake()
    {
        rb = GetComponent<Rigidbody>();
        this.movementSpeed = Random.Range(1.75f, 2.25f);
    }

    protected void LoadAnimatior()
    {
        if (animator != null) return;
        animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator");
    }

    protected override void Update()
    {
        if (!IsMoving()) 
        {
            animator.SetFloat("Speed_f", 0f);
            return;
        }
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                this.reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
            }
            else
            {
                this.reachedDestination = true;
            }
            
                animator.SetFloat("Speed_f", 0.3f);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        this.reachedDestination = false;
    }
}
