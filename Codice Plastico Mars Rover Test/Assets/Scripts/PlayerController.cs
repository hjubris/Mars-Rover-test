using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int boundary = 75;

    public TextMeshProUGUI warningText;
    public TextMeshProUGUI coordinates;
    public int speed = 1;
    public Camera mainCamera;

    private void Start()
    {
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
    /// given a user input, moves the rover forward or backwards or turns it left and right
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

        UpdateCoordinates();
    }

    private void OnCollisionEnter(Collision collision)
    {
        warningText.alpha = 1;
    }
    private void OnCollisionExit(Collision collision)
    {
        warningText.alpha = 0;
    }

    public void ChangeSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    private void UpdateCoordinates()
    {
        coordinates.text = ("Current coordinates: X (" + transform.position.x.ToString("F2") + "), Y (1), Z (" + transform.position.z.ToString("F2") + ")");
    }
}
