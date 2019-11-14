using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private CharacterController controller;
    [SerializeField] private Transform camera;
    [SerializeField] private float rotationSpeed = 3;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float gravity = 3;
    [SerializeField] private float jumpHeight = 2;

    private float rotX, rotY, inputX, inputY;
    private Vector3 vel;

    private RaycastHit hit;
    private bool shoot = false;
    [SerializeField] float playerHeight;
    [SerializeField] float floorOffset;

    Vector3 groundDirfwd, groundDirright;

    public Transform bullet, source, muzzle;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    IEnumerator reshoot() {

        yield return new WaitForSeconds(.1f);
        anim.SetBool("shoot", false);
        yield return new WaitForSeconds(.4f);
        shoot = false;

    }

    // Update is called once per frame
    void Update()
    {

        rotX += Input.GetAxis("Mouse X") * rotationSpeed;
        rotY += Input.GetAxis("Mouse Y") * rotationSpeed;

        inputX = Input.GetAxis("Horizontal") * movementSpeed;
        inputY = Input.GetAxis("Vertical") * movementSpeed;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, playerHeight + floorOffset) && vel.y <= 0)
        {
            groundDirfwd = Vector3.Cross(hit.normal, -transform.right);
            groundDirright = Vector3.Cross(hit.normal, transform.forward);

            vel.y = -controller.stepOffset / Time.deltaTime;

            anim.SetBool("fall", false);

            if (Input.GetKeyDown(KeyCode.Space))
            {

                vel.y = jumpHeight;
                anim.SetTrigger("jump");

            }

        }
        else
        {
            anim.SetBool("fall", true);

            groundDirfwd = transform.forward;
            groundDirright = transform.right;
            vel.y -= gravity * Time.deltaTime;
        }

        if(Input.GetKeyDown("e"))
        {
            anim.SetBool("boner", !anim.GetBool("boner"));
        }
        
        if (Input.GetMouseButtonUp(0)) {
            if (anim.GetBool("boner") && !shoot) {
                shoot = true;
                Instantiate(bullet, source.position, camera.rotation);
                muzzle.GetComponent<ParticleSystem>().Play();
                StartCoroutine(reshoot());
                anim.SetBool("shoot", true);
            }
        }
        
        anim.SetFloat("speed", Mathf.Abs(inputX) + Mathf.Abs(inputY));

        rotY = Mathf.Clamp(rotY, -90f, 90f);
       
        vel = Vector3.up * vel.y + groundDirright * inputX + groundDirfwd * inputY;

        camera.rotation = Quaternion.Euler(Vector3.left * rotY + Vector3.up * rotX);

        transform.eulerAngles = Vector3.up * camera.eulerAngles.y;
        controller.Move(vel);

    }
}
