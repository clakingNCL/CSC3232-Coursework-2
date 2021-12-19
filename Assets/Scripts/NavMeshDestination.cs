using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshDestination : MonoBehaviour
{
    [SerializeField]
    GameObject destination;

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = destination.transform.position;
    }
}
