using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject rover;
    public Vector3 positionOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets the camera position to be the same as the rover's. In the editor, an offset can be set to change the viewpoint
        transform.position = rover.transform.position + positionOffset;
    }
}
