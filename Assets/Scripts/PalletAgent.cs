 using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System;

public class PalletAgent : Agent
{
    public GameObject packagePrefab;
    private float[] _grid;
    private GameObject _package;
    private Vector3 _scale;
    private List<GameObject> _packages = new();
    
    private float SensorHeight = 5.0f;
    float cellSize = 1f;
    
    void Start()
    {
        _grid = new float[50];
    }

    public override void OnEpisodeBegin()
    {
        DeletePackages();
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        _grid = MeasureGrid();
        _scale = new Vector3( UnityEngine.Random.Range(1,3)- 0.01f , 1 , UnityEngine.Random.Range(1,3)- 0.01f ); //random
        sensor.AddObservation(_grid);
        sensor.AddObservation(_scale.x);
        sensor.AddObservation(_scale.z);
    }
    
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        _package = Instantiate(packagePrefab);
        _package.transform.localScale = _scale;
        var cornerposition = new Vector3(normalize(actionBuffers.ContinuousActions[0], 5), 0.75f, normalize(actionBuffers.ContinuousActions[1], 10));
        // Debug.Log(cornerposition);
        // Debug.Log(actionBuffers.ContinuousActions[0]);
        _package.transform.position =  cornerposition + new Vector3(_package.transform.localScale.x/2,0,_package.transform.localScale.z/2);// Create object
        _packages.Add(_package); //track packages
        AddReward(1.0f);
    }

    public void CollisionDetection()
    {
        // collision detection on packages, and not on pallet
        AddReward(-10.0f);
        EndEpisode();
    }

    float normalize(float x, float n)
    {
     return (float) Math.Floor((x+1f)/2f * n);
    }

    float[] MeasureGrid()
    {
        for (int i=0; i<10; i++)
        {
            for(int j=0; j<5; j++)
            {
                RaycastHit hit;
                var origin = new Vector3((j+0.5f)*cellSize, SensorHeight, (i+0.5f)*cellSize);
                // Gizmos.DrawSphere(origin, 0.1f);
                Physics.Raycast(origin, new Vector3(0.0f, -1.0f, 0.0f), out hit, SensorHeight+1f);
                // Debug.Log(hit.distance);
                _grid[i] = (SensorHeight-hit.distance)/SensorHeight;
                // Debug.Log(grid[i]);  
            }
        }
            
        return _grid;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var ContinuousActionsOut = actionsOut.ContinuousActions;
        // var discreteActionsOut = actionsOut.discreteActionsOut;
        ContinuousActionsOut[0] = Input.GetAxis("Vertical");
        ContinuousActionsOut[1] = Input.GetAxis("Horizontal");
        // discreteActionsOut[0] = Input.GetKey("1") ? 1 : 0;
        // discreteActionsOut[0] = Input.GetKey("2") ? 2 : discreteActionsOut[0];
    }
    
    void DeletePackages()
    {
        foreach (var package in _packages)
        {
            Destroy(package);
        }
        _packages.Clear();
    }
}
