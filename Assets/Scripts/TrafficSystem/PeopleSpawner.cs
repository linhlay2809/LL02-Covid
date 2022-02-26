using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MainBehaviour
{
    [SerializeField] protected GameObject peoplePrefab;
    [SerializeField] protected int peopleToSpawn;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int current = 0;
        while (current < peopleToSpawn)
        {
            GameObject obj = Instantiate(peoplePrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;
            obj.transform.parent = PeopleManager.Instance.transform;
            PeopleManager.Instance.AddPepleToList(obj);

            yield return new WaitForEndOfFrame();
            current++;
        }
        
    }

}
