using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private Interaction interact;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float shootDelay = .5f;
    private float nextShot;
    private Animator anim;

    [SerializeField] private Transform emitter;
    [SerializeField] private GameObject bullet;

    private bool fall, shoot = false;
    [SerializeField] private int shotsPerMag, mags, shots;
    [SerializeField] private TextMeshProUGUI ammo;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void UpdateText()
    {

        ammo.SetText($"{shots} / {mags}");

    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetBool("shoot"))
        {
            anim.SetBool("shoot", false);
            if(shots == 0 && mags == 0)
            {
                anim.SetBool("boner", false);
            }
        } else if (Input.GetMouseButton(0) && anim.GetBool("boner") && Time.time > nextShot && shots > 0)
        {
            shots--;
            UpdateText();
            nextShot = Time.time + shootDelay;
            anim.SetBool("shoot", true);
            Instantiate(bullet, emitter.position, gameObject.GetComponentInParent<Transform>().rotation);
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            if(mags > 0 && shots < shotsPerMag)
            {
                mags--;
                shots = shotsPerMag;
                UpdateText();
            }
        } else if(Input.GetKeyDown(KeyCode.E))
        {
            if(interact.hasHit() && interact.getHitObject().tag == "mag")
            {
                AddMag();
                Destroy(interact.getHitObject());
            }
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
            }
            
        } else
        {

            if (player.getVelocityY() < 0)
            {
                anim.SetBool("fall", true);
                fall = true;
            }

        }

    }

    public void AddMag()
    {
        
        mags++;
        UpdateText();
        anim.SetBool("boner", true);

    }

}
