using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public GameObject block;
    public GameObject Pallet;
    int chance;
    float Change;

    private float SensorHeight = 5.0f;
    float cellSize = 1f;
    float[] grid;
    public Material transparent;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PlaceBlock", 0f, 2f);
        InvokeRepeating("Measure", 0f, 0.5f);
        //InvokeRepeating("PackageGenerator", 0f, 0.5f);
        grid = new float[50];
        var Block_Pallet = Instantiate(Pallet);


    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    
    void PlaceBlock()
    {

        var thisGo = Instantiate(block);
        thisGo.transform.localScale = new Vector3( Random.Range(1,3) , 1 , Random.Range(1,3) );
        var cornerposition = new Vector3(x:Random.Range(0, 5), y:0.75f, z:Random.Range(0, 10));
        
        Rigidbody GameObjectRigidbody = thisGo.AddComponent<Rigidbody>();
        GameObjectRigidbody.mass = 5;

    } 

    float[] Measure()
    {
        for (int i=0; i<10; i++)
        {
            for(int j=0; j<5; j++)
            {
                RaycastHit hit;
                var origin = new Vector3((j+0.5f)*cellSize, SensorHeight, (i+0.5f)*cellSize);
                Physics.Raycast(origin, new Vector3(0.0f, -1.0f, 0.0f), out hit, SensorHeight+1f);
                Debug.Log(hit.distance);
                grid[i] = (SensorHeight-hit.distance)/SensorHeight;
                // Debug.Log(grid[i]);  
            }
        }
            
        return grid;
    }
    

    
    // float[] PackageGenerator()
    // {
    //     var Package = new float[3] { Random.Range(1,3) , Random.Range(1,3) , 1 };

    //     Instantiate(block)
    //     return Package;
    // } 

    
}
