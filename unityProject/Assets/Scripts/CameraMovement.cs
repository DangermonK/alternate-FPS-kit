using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationSmoothing;
    [SerializeField] private float minYClamp, maxYClamp;
    
    private float inputX, inputY;
    private float currRotX = 0, currRotY = 0;
    private float rotX, rotY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        rotX += inputX * rotationSpeed * Time.deltaTime;
        rotY += inputY * rotationSpeed * Time.deltaTime;

        rotY = Mathf.Clamp(rotY, minYClamp, maxYClamp);

        currRotX = Mathf.Lerp(currRotX, rotX, rotationSmoothing);
        currRotY = Mathf.Lerp(currRotY, -rotY, rotationSmoothing);

        transform.rotation = Quaternion.Euler(currRotY, currRotX, 0);
        
    }

    public float getRotationX()
    {

        return currRotX;

    }

    public float getRotationY()
    {

        return currRotY;

    }

    public float getInputX()
    {

        return inputX;

    }

    public float getInputY()
    {

        return inputY;

    }

}
