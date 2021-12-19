using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject obj;
    public float timeToWait;
    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= timeToWait) //Once time has passed, introduce obstacles which carve navmesh and affect pathfinding
        {
            obj.GetComponent<MovingPlatform>().enabled = true;
        }
    }
}
