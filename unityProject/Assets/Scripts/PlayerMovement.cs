using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CameraMovement camMovement;
    
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float movementSpeed;

    private CharacterController controller;
    private float velocityY;
    private bool grounded, jump;

    private float inputX, inputY;

    private RaycastHit hit;
    private Vector3 rightVect, fwdVect;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height * 0.5f + 0.3f))
        {
            fwdVect = Vector3.Cross(transform.right, hit.normal);
            rightVect = Vector3.Cross(-transform.forward, hit.normal);
        } else
        {
            fwdVect = transform.forward;
            rightVect = transform.right;
        }
        
        if(controller.isGrounded && !jump)
        {
            
            grounded = true;

        } else
        {
            if(jump)
                jump = false;

            velocityY -= gravity;
            grounded = false;

        }

        controller.Move((velocityY * Vector3.up + (fwdVect * inputY + rightVect * inputX) * movementSpeed) * Time.fixedDeltaTime );

    }

    void Update() {

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.rotation = Quaternion.Euler(Vector3.up * camMovement.getRotationX());

    }

    public void Jump()
    {

        jump = true;
        velocityY = jumpHeight;

    }

    public float getInputX()
    {
        return inputX;
    }

    public float getInputY()
    {
        return inputY;
    }

    public float getVelocityY()
    {
        return velocityY;
    }

    public bool isGrounded()
    {
        return grounded;
    }

}
