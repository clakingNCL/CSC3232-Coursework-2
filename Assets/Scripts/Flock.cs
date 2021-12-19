using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField] 
    private FlockUnit flockUnitPrefab;
    [SerializeField]
    private int flockSize;
    [SerializeField]
    private Vector3 spawnBounds;

    //Initialise minimum and maximum speed values

    [SerializeField]
    private float _minSpeed;

    public float minSpeed { get { return _minSpeed; } }

    [SerializeField]
    private float _maxSpeed;

    public float maxSpeed { get { return _maxSpeed; } }

    //Initialise Detection Distances

    [SerializeField]
    private float _cohesionDistance;

    public float cohesionDistance { get { return _cohesionDistance; } }

    [SerializeField]
    private float _avoidanceDistance;

    public float avoidanceDistance { get { return _avoidanceDistance; } }

    [SerializeField]
    private float _alignmentDistance;

    public float alignmentDistance { get { return _alignmentDistance; } }

    [SerializeField]
    private float _boundsDistance;

    public float boundsDistance { get { return _boundsDistance; } }

    [SerializeField]
    private float _obstacleDistance;

    public float obstacleDistance { get { return _obstacleDistance; } }

    //Initialise Behaviour Weights

    [SerializeField]
    private float _cohesionWeight;

    public float cohesionWeight { get { return _cohesionWeight; } }

    [SerializeField]
    private float _avoidanceWeight;

    public float avoidanceWeight { get { return _avoidanceWeight; } }

    [SerializeField]
    private float _alignmentWeight;

    public float alignmentWeight { get { return _alignmentWeight; } }

    [SerializeField]
    private float _boundsWeight;

    public float boundsWeight { get { return _boundsWeight; } }


    [SerializeField]
    private float _obstacleWeight;

    public float obstacleWeight { get { return _obstacleWeight; } }

    public FlockUnit[] allUnits { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GenerateUnits();
    }

    private void GenerateUnits() //Creates all flock members
    {
        allUnits = new FlockUnit[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, 90, Random.Range(0, 360));
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitialiseSpeed(Random.Range(minSpeed, maxSpeed));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i< allUnits.Length; i++)
        {
            allUnits[i].MoveUnit();
        }
    }
}
