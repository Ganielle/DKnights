using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class playerMovement : MonoBehaviour
{

    //Character variable settings
    [Header("Values")]
    [SerializeField]private Transform center;
    private Rigidbody playerCharacter;
    private CapsuleCollider characterCollider;
    [HideInInspector]public Animator anim;
    [SerializeField]private AnalogStick analog;
    [SerializeField]private float maxDistanceCollision;
    [SerializeField]private float maxHitWallDistance;

    [Header("Movement Values")]
    //movement variables settings
    float ver,hor;
    Vector3 inputVector;
    [SerializeField]private float rotationSpeed;
    [SerializeField]private float runSpeed, walkSpeed;
    float speed;

    [Header("Slope Movement Values")]
    [SerializeField] private float height = 1.75f;
    [SerializeField] private float heightPadding = 0.05f;
    [SerializeField] private float maxGroundAngle = 120;
    [SerializeField] private float slopeForce;
    float angle;
    float groundAngle;
    Vector3 forward;

    [Header("Camera")]
    //camera Variable settings
    [SerializeField]private Camera relativeTransform;

    [Header("World Values")]
    //World Variables settings
    [SerializeField] private float minimumFallingVelocity = -0.1f;
    [SerializeField]private float fallingVelocity = -0.1f;
    [SerializeField] private float fallingToLandingVelocity = -0.1f;
    [SerializeField]private LayerMask ground;
    [SerializeField]private LayerMask wall;

    //Animation Variables settings
    AnimatorStateInfo stateInfo;
    int runStateHash = Animator.StringToHash("Base Layer.Locomotion.Running");
    int walkStateHash = Animator.StringToHash("Base Layer.Locomotion.Walking");
    int idleStateHash = Animator.StringToHash("Base Layer.Locomotion.Neutral Idle");
    int fallingStateHash = Animator.StringToHash("Base Layer.Locomotion Jump.Falling");
    int fallToGroundStateHash = Animator.StringToHash("Base Layer.Locomotion Jump.Falling To Landing");
    int shortLanding = Animator.StringToHash("Base Layer.Locomotion Jump.ShortLanding");
    int pickUpItems = Animator.StringToHash("Base Layer.Locomotion.pickUpItem");

    //Collision settings
    RaycastHit hit, hitInfo;
    bool isHittingCollider, grounded;
    float characterVelocityY;

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall") &&
            other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            isHittingCollider = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall") &&
            col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isHittingCollider = false;
        }
    }

    private void Start()
    {
        playerCharacter = GetComponent<Rigidbody>();
        characterCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    }

    private void FixedUpdate()
    {
        CalculateFoward();
        CalculateGroundAngle();
        CheckGround();
        DrawDebugLines();

        characterVelocityY = playerCharacter.velocity.y * Time.deltaTime;
        if (!GameManager.instance.isPickingUp)
            movePlayerRigidbody();

        
    }
    
    private void movePlayerRigidbody()
    {

        hor = analog.Horizontal();
        ver = analog.Vertical();


        ApplyGravity();

        inputVector = new Vector3(hor, 0.0f, ver).normalized;
        inputVector = relativeTransform.transform.TransformDirection(inputVector);
        inputVector.y = 0.0f;

        inputVector = inputVector.normalized * speed;

        if (!isRaycastHitWall() && groundAngle < maxGroundAngle){
            if(stateInfo.fullPathHash != fallToGroundStateHash && stateInfo.fullPathHash != shortLanding && stateInfo.fullPathHash != pickUpItems)
            playerCharacter.MovePosition((Vector3)playerCharacter.position + inputVector);
        }

        if(inputVector != Vector3.zero && stateInfo.fullPathHash != pickUpItems && stateInfo.fullPathHash != fallToGroundStateHash)
            playerCharacter.rotation = Quaternion.Slerp(playerCharacter.rotation,Quaternion.LookRotation(inputVector.normalized), 
            rotationSpeed * Time.deltaTime);


        movementPlayerAnimation();
    }

    private void movementPlayerAnimation()
    {

        //walking
        if (((hor <= 0.5f && hor >= 0.01f) || (ver <= 0.5f && ver >= 0.01f)) || ((hor >= -0.5f && hor <= -0.01f) || (ver >= -0.5f && ver <= -0.01f)))
        {
            anim.SetBool("isWalking",true);
            anim.SetBool("isRunning",false);
            anim.SetBool("isIdle",false);
            speed = walkSpeed;
        }

        //running
        if(((hor > 0.5f && hor <= 1.0f) || (ver > 0.5f && ver <= 1.0f)) || ((hor < -0.5f && hor >= -1.0f) || (ver < -0.5f && ver >= -1.0f)))
        {
            anim.SetBool("isRunning",true);
            anim.SetBool("isWalking",false);
            anim.SetBool("isIdle",false);
            speed = runSpeed;
        }

        //idle
        if(hor == 0.0f && ver == 0.0f)
        {
            anim.SetBool("isIdle",true);
            anim.SetBool("isWalking",false);
            anim.SetBool("isRunning",false);
            speed = 0;
        }

        if(stateInfo.fullPathHash == idleStateHash || stateInfo.fullPathHash == runStateHash || stateInfo.fullPathHash == walkStateHash)
        {
            anim.ResetTrigger("fallToGround");
            anim.ResetTrigger("isShortLanding");
        }

        if(isFalling())
        {
            speed = 0.15f;
            anim.SetTrigger("isFalling");
        }

        if(isGrounded() && isFallToGround() && stateInfo.fullPathHash == fallingStateHash)
        {
            anim.ResetTrigger("isFalling");
            anim.SetTrigger("fallToGround");
        }

        if(DetectGroundWhenFalling() && (FallingToIdle() || ResetFallingWhenGround()) && stateInfo.fullPathHash == fallingStateHash)
        {
            anim.ResetTrigger("isFalling");
            anim.SetTrigger("isShortLanding");
        }
    }
    
    private bool isRaycastHitWall(){

        if(Physics.Raycast(center.position, inputVector, out hit, maxHitWallDistance,wall))
            return true;
        return false;
    }

    private bool isFalling(){

        if(characterVelocityY < fallingVelocity && !isGrounded())
            return true;
        return false;
    }

    private bool isFallToGround(){
        if(characterVelocityY < fallingToLandingVelocity)
            return true;
        return false;
    }

    private bool ResetFallingWhenGround()
    {
        if (characterVelocityY > minimumFallingVelocity)
            return true;
        return false;
    }

    private bool FallingToIdle()
    {
        if (characterVelocityY < minimumFallingVelocity && characterVelocityY > fallingToLandingVelocity)
            return true;

        return false;
    }

    private bool DetectGroundWhenFalling()
    {
        if (Physics.Raycast(center.position, transform.TransformDirection(Vector3.down), maxDistanceCollision, ground))
            return true;
        return false;
    }

    private bool isGrounded()
    {
        return Physics.CheckCapsule(characterCollider.bounds.center, new Vector3(characterCollider.bounds.center.x,
                            characterCollider.bounds.min.y, characterCollider.bounds.center.z),
                             characterCollider.radius * .9f, ground);
    }

    private void CalculateFoward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }

        forward = Vector3.Cross(hitInfo.normal, -transform.right);
    }

    private void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }

    private void CheckGround()
    {
        if (Physics.Raycast(center.position, Vector3.down, out hitInfo, height, ground))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void ApplyGravity()
    {
        if (groundAngle < 90 && (hor != 0 || ver != 0))
            playerCharacter.position += Vector3.down * characterCollider.height / 2 * slopeForce * Time.deltaTime;
    }

    private void DrawDebugLines()
    {
        Debug.DrawLine(center.position, center.position + forward * height * 2, Color.blue);
        Debug.DrawLine(center.position, center.position + Vector3.down * height, Color.yellow);
    }
}
