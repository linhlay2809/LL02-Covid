using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    PeopleNavigationController controller;
    public Waypoint currentWaypoint;
    int direction;

    private void Awake()
    {
        controller = GetComponent<PeopleNavigationController>();
    }

    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    void Update()
    {
        if (controller.reachedDestination)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }
            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
