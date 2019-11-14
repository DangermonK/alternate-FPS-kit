using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    [SerializeField] private PlayerMovement player;
    [SerializeField] private float shootDelay = 2;
    private Animator anim;

    [SerializeField] private Transform emitter;
    [SerializeField] private GameObject bullet;

    private bool fall, shoot = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(shoot)
        {
            shoot = false;
            anim.SetBool("shoot", false);
        }

        if (Input.GetMouseButtonDown(0) && anim.GetBool("boner") && !shoot)
        {
            shoot = true;
            anim.SetBool("shoot", true);
            Instantiate(bullet, emitter.position, emitter.rotation);
        }

        if (player.isGrounded())
        {

            if(fall)
            {
                anim.SetBool("fall", false);
                fall = false;
            }

            anim.SetFloat("speed", Mathf.Abs(player.getInputX()) + Mathf.Abs(player.getInputY()));

            if(Input.GetKeyDown(KeyCode.Space))
            {
                player.Jump();
                anim.SetTrigger("jump");
            } else if(Input.GetKeyDown(KeyCode.E)) 
            {
                anim.SetBool("boner", !anim.GetBool("boner"));
            }

            

        } else
        {

            anim.SetBool("fall", true);
            fall = true;

        }

    }
}
