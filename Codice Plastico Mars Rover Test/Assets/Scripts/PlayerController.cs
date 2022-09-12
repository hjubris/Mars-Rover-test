using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //This field is displayed in the inspector to make it easier to tweak the boundaries.
    [SerializeField]
    int boundary = 75;

    public TextMeshProUGUI warningText;
    public TextMeshProUGUI coordinates;

    public Camera mainCamera;

    public int speed = 1;

    private void Start()
    {
        //to avoid displaying the (quite large) warning text, initialize it to be invisible
        warningText.alpha = 0;
    }

    void Update()
    {
        MoveRover();
        ManageBoundaries();
    }

    /// <summary>
    /// Restrains the rover between boundaries, to give the illusion of moving on a planet
    /// </summary>
    private void ManageBoundaries()
    {
        if (transform.position.z > boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundary); //teleports the rover to the bottom of the map
        }
        else if (transform.position.z < -boundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundary); //teleports the rover to the top of the map
        }
        else if (transform.position.x > boundary)
        {
            transform.position = new Vector3(-boundary, transform.position.y, transform.position.z); //teleports the rover to the left of the map
        }
        else if (transform.position.x < -boundary)
        {
            transform.position = new Vector3(boundary, transform.position.y, transform.position.z); //teleports the rover to the right of the map
        }
    }

    /// <summary>
    /// given a user input, moves the rover forward or backwards or turns it left and right, along with the camera and camera angle.
    /// </summary>
    private void MoveRover()
    {
        var rot = gameObject.transform.localRotation.eulerAngles; //get the angles

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot.Set(0f, rot.y - 45, 0f); //turns the object's angle 45° to the left
            gameObject.transform.localRotation = Quaternion.Euler(rot); //update the object's transform
            mainCamera.transform.localRotation = Quaternion.Euler(rot); //update the camera's transform
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot.Set(0f, rot.y + 45, 0f); //turns the object's angle 45° to the right
            gameObject.transform.localRotation = Quaternion.Euler(rot); //update the transform
            mainCamera.transform.localRotation = Quaternion.Euler(rot); //update the camera's transform
        }

        //After having moved, update the "coordinates" text object
        UpdateCoordinates();
    }

    // When the rover detects a collision with an object, display a warning message, when the collision is not happening anymore, stop displaying it
    #region Collision managment    
    private void OnCollisionEnter(Collision collision)
    {
        warningText.alpha = 1;
    }
    private void OnCollisionExit(Collision collision)
    {
        warningText.alpha = 0;
    }
    #endregion

    /// <summary>
    /// Update the text in the top right to show the coordinates of the rover, displaying only the first two decimal digits for clarity
    /// </summary>
    private void UpdateCoordinates()
    {
        coordinates.text = ("Current coordinates: X (" + transform.position.x.ToString("F2") + "), Y (1), Z (" + transform.position.z.ToString("F2") + ")");
    }
}
