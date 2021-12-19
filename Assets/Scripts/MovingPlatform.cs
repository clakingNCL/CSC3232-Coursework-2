using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1.0f;

    public Transform startMarker;
    public Transform endMarker;

    private float startTime;
    private float journeyLength;
    private bool startJourney = false;
    private bool platformsInStartPos = true;

    void OnEnable()
    {
        InitialiseJourney();
    }

    void Update()
    {
        if (startJourney && platformsInStartPos) //Platforms are ready to move down
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            if (transform.position == endMarker.position)
            {
                startJourney = false;
                platformsInStartPos = false;
                this.enabled = false;
            }
        }
        if (startJourney && !platformsInStartPos) //Platforms are ready to move back up
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(endMarker.position, startMarker.position, fractionOfJourney);
            if (transform.position == startMarker.position)
            {
                startJourney = false;
                platformsInStartPos = true;
                this.enabled = false;
            }
        }

    }

    void InitialiseJourney()
    {
        if (platformsInStartPos)
        {
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
            startJourney = true;
            
        }
        if (!platformsInStartPos)
        {
            startTime = Time.time;
            journeyLength = Vector3.Distance(endMarker.position, startMarker.position);
            startJourney = true;
        }
    }

}
