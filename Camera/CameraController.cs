using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameramoveSpeed;
    public GameObject playerTarget;
    Vector3 FollowPos;
    public float clampAngle;
    public float inputSensitivity;
    public float mouseX;
    public float mouseY;
    public float finalInputx;
    public float finalInputz;
    private float rotY;
    private float rotX;


    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        //We need to set the rotation of the sticks
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputx = inputX + mouseX;
        finalInputz = inputZ + mouseY;

        rotY += finalInputx * inputSensitivity * Time.deltaTime;
        rotX += finalInputz * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        //set the target object to follow
        Transform target = playerTarget.transform;

        //move towarrds the game object(target)
        float step = cameramoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
