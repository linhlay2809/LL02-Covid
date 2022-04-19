using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] protected GameObject wayPoint;
    [SerializeField] protected GameObject[] peoplePrefabs;
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
            GameObject obj = Instantiate(peoplePrefabs[Random.Range(0,peoplePrefabs.Length)]);
            Transform child = wayPoint.transform.GetChild(Random.Range(0, wayPoint.transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;
            obj.transform.parent = PeopleManager.Instance.transform;
            PeopleManager.Instance.AddPepleToList(obj);

            yield return new WaitForEndOfFrame();
            current++;
        }

    }
}
