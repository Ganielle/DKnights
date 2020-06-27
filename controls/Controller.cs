using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    public float playerHeight;
    public float heightPadding;
    public LayerMask ground;
    public float maxGroundAngle;
    public bool debug;

    Vector2 input;
    float angle;
    float groundAngle;

    [HideInInspector]public Vector3 forward;
    [HideInInspector]public bool isGrounded;
    RaycastHit hitInfo;

    void Update(){
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
        DrawDebugLines();
    }

    void CalculateForward(){
        if(!isGrounded){
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(hitInfo.normal, -transform.right);
    }

    void CalculateGroundAngle(){
        if(!isGrounded){
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    void CheckGround(){
        if(Physics.Raycast(transform.position, -Vector3.up, out hitInfo, playerHeight, ground)){
            isGrounded = true;
        }
        else{
            isGrounded = false;
        }
    }

    void ApplyGravity(){

    }

    void DrawDebugLines(){
        if(!debug) return;

        Debug.DrawLine(transform.position, transform.position + forward * playerHeight * 2, Color.blue);
        Debug.DrawLine(transform.position, transform.position - Vector3.up * playerHeight, Color.green);
    }
}
