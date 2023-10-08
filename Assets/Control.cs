using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public GameObject Target;
    public Transform target; // The target object to rotate around
    public float distance = 10.0f; // The distance from the target object
    public float sensitivity = 5.0f; // The sensitivity of the mouse movement
    public float zoomSpeed = 2.0f; // The speed of zooming

    private float currentLongitude = 0.0f; // The current longitude of the camera
    private float currentLatitude = 0.0f; // The current latitude of the camera

    void Start()
    {
        target = Target.transform;
    }

    // Update is called once per frame
    public void Update()
{
    // Check if the middle mouse button is pressed
    if (Input.GetMouseButton(0))
    {
        // Get the mouse movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Update the longitude and latitude based on the mouse movement
        currentLongitude += mouseX;
        currentLatitude -= mouseY;

        // Convert the spherical coordinates to Cartesian coordinates
        Vector3 position = new Vector3();
        position.x = distance * Mathf.Sin(currentLatitude * Mathf.Deg2Rad) * Mathf.Cos(currentLongitude * Mathf.Deg2Rad);
        position.y = distance * Mathf.Cos(currentLatitude * Mathf.Deg2Rad);
        position.z = distance * Mathf.Sin(currentLatitude * Mathf.Deg2Rad) * Mathf.Sin(currentLongitude * Mathf.Deg2Rad);

        // Set the camera position and look at the target object
        transform.position = target.position + position;
        transform.LookAt(target.position);
    }

    // Check if the scroll wheel is being used
    if (Input.GetAxis("Mouse ScrollWheel") != 0)
    {
        // Adjust the distance from the target object based on the scroll wheel input
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, 1.0f, 100.0f);

        // Convert the spherical coordinates to Cartesian coordinates
        Vector3 position = new Vector3();
        position.x = distance * Mathf.Sin(currentLatitude * Mathf.Deg2Rad) * Mathf.Cos(currentLongitude * Mathf.Deg2Rad);
        position.y = distance * Mathf.Cos(currentLatitude * Mathf.Deg2Rad);
        position.z = distance * Mathf.Sin(currentLatitude * Mathf.Deg2Rad) * Mathf.Sin(currentLongitude * Mathf.Deg2Rad);

        // Set the camera position and look at the target object
        transform.position = target.position + position;
        transform.LookAt(target.position);
    }

    // Check if the user has clicked on a satellite prefab
    
}
}
