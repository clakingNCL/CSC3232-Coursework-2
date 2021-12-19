using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    [SerializeField]
    private float FOVAngle;
    [SerializeField]
    private float smoothDamp;
    [SerializeField]
    private Vector3[] directionsToCheck;
    [SerializeField]
    private LayerMask obstacleMask;

    private List<FlockUnit> cohesionNeighbours = new List<FlockUnit>();
    private List<FlockUnit> alignmentNeighbours = new List<FlockUnit>();
    private List<FlockUnit> avoidanceNeighbours = new List<FlockUnit>();

    private Flock assignedFlock;
    private Vector3 currentVelocity;
    private Vector3 currentObstacleAvoidanceVector;
    private float speed;

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;
    }

    public void InitialiseSpeed(float speed)
    {
        this.speed = speed;
    }

    public void MoveUnit()
    {
        FindNeighbours();
        CalculateSpeed();

        var cohesionVector = CalculateCohesionVector() * assignedFlock.cohesionWeight;
        var avoidanceVector = CalculateAvoidanceVector() * assignedFlock.avoidanceWeight;
        var alignmentVector = CalculateAlignmentVector() * assignedFlock.alignmentWeight;
        var boundsVector = CalculateBoundsVector() * assignedFlock.boundsWeight;
        var obstacleVector = CalculateObstacleVector() * assignedFlock.obstacleWeight; 

        var moveVector = cohesionVector + avoidanceVector + alignmentVector + boundsVector + obstacleVector;
        moveVector = Vector3.SmoothDamp(transform.forward, moveVector, ref currentVelocity, smoothDamp);
        moveVector = moveVector.normalized * speed;
        if (moveVector == Vector3.zero)
        {
            moveVector = transform.forward;
        }

        transform.forward = moveVector;
        transform.position += moveVector * Time.deltaTime;
    }

    private void CalculateSpeed()
    {
        if(cohesionNeighbours.Count == 0)
        {
            return;
        }
        for (int i=0;i<cohesionNeighbours.Count; i++)
        {
            speed += cohesionNeighbours[i].speed;
            speed /= cohesionNeighbours.Count;
            speed = Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed);
        }
    }

    private void FindNeighbours()
    {
        cohesionNeighbours.Clear();
        avoidanceNeighbours.Clear();
        alignmentNeighbours.Clear();
        var allUnits = assignedFlock.allUnits;

        for (int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if (currentUnit != this)
            {
                float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);
                if (currentNeighbourDistanceSqr <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
                {
                    cohesionNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanceSqr <= assignedFlock.avoidanceDistance * assignedFlock.avoidanceDistance)
                {
                    avoidanceNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanceSqr <= assignedFlock.alignmentDistance * assignedFlock.alignmentDistance)
                {
                    alignmentNeighbours.Add(currentUnit);
                }
            }
        }
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbours.Count == 0)
        {
            return cohesionVector;
        }
        int neighboursInFOV = 0;
        for(int i = 0; i < cohesionNeighbours.Count; i++)
        {
            if (IsInFOV(cohesionNeighbours[i].transform.position))
            {
                neighboursInFOV++;
                cohesionVector += cohesionNeighbours[i].transform.position;
            }
        }
        cohesionVector /= neighboursInFOV;
        cohesionVector -= transform.position;
        cohesionVector = cohesionVector.normalized;
        return cohesionVector;
    }

    private Vector3 CalculateAlignmentVector()
    {
        var alignmentVector = transform.forward;
        if(alignmentNeighbours.Count == 0)
        {
            return alignmentVector;
        }
        int neighboursInFOV = 0;
        for (int i = 0; i < alignmentNeighbours.Count; i++)
        {
            if (IsInFOV(alignmentNeighbours[i].transform.position))
            {
                neighboursInFOV++;
                alignmentVector += alignmentNeighbours[i].transform.forward;
            }
        }
        alignmentVector /= neighboursInFOV;
        alignmentVector = alignmentVector.normalized;
        return alignmentVector;
    }

    private Vector3 CalculateAvoidanceVector()
    {
        var avoidanceVector = Vector3.zero;
        if (avoidanceNeighbours.Count == 0)
        {
            return avoidanceVector;
        }
        int neighboursInFOV = 0;
        for (int i = 0; i < avoidanceNeighbours.Count; i++)
        {
            if (IsInFOV(avoidanceNeighbours[i].transform.position))
            {
                neighboursInFOV++;
                avoidanceVector += transform.position - avoidanceNeighbours[i].transform.position;
            }
        }
        avoidanceVector /= neighboursInFOV;
        avoidanceVector = avoidanceVector.normalized;
        return avoidanceVector;
    }

    private Vector3 CalculateBoundsVector()
    {
        var offsetToCenter = assignedFlock.transform.position - transform.position;
        bool isNearCenter = offsetToCenter.magnitude >= assignedFlock.boundsDistance * 0.9f;
        return isNearCenter ? offsetToCenter.normalized : Vector3.zero;
    }

    private Vector3 CalculateObstacleVector()
    {
        var obstacleVector = Vector3.zero;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, assignedFlock.obstacleDistance, obstacleMask))
        {
            obstacleVector = FindBestDirection();
        }
        else
        {
            currentObstacleAvoidanceVector = Vector3.zero;
        }
        return obstacleVector;
    }

    private Vector3 FindBestDirection() //Find best direction to avoid obstacle.
    {
        if(currentObstacleAvoidanceVector != Vector3.zero)
        {
            RaycastHit hit;
            if(!Physics.Raycast(transform.position, transform.forward, out hit, assignedFlock.obstacleDistance, obstacleMask))
            {
                return currentObstacleAvoidanceVector;
            }
        }
        float maxDistance = int.MinValue;
        Vector3 selectedDirection = Vector3.zero;
        for (int i=0; i < directionsToCheck.Length; i++)
        {
            RaycastHit hit;
            var currentDirection = transform.TransformDirection(directionsToCheck[i].normalized);
            if (Physics.Raycast(transform.position, currentDirection, out hit, assignedFlock.obstacleDistance, obstacleMask)) 
            {
                float currentDistance = (hit.point - transform.position).sqrMagnitude;     
                if(currentDistance > maxDistance)
                {
                    maxDistance = currentDistance;
                    selectedDirection = currentDirection;
                }
            }
            else
            {
                selectedDirection = currentDirection;
                currentObstacleAvoidanceVector = currentDirection.normalized;
                return selectedDirection.normalized;
            }
        }
        return selectedDirection.normalized;
    }

    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(transform.forward, position - transform.position) <= FOVAngle;
    }
}
