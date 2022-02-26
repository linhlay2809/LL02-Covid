using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleNavigationController : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    public float movementSpeed;
    public float rotationSpeed;
    public float stopDistance;
    public Vector3 destination;
    public bool reachedDestination;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
            
            if (transform.position != Vector3.zero)
            {
                animator.SetFloat("Speed_f", 0.3f);
            }
            else
            {
                animator.SetFloat("Speed_f", 0f);
            }
            
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}
