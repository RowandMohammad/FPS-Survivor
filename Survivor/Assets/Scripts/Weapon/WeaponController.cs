using UnityEngine;

public class WeaponController : Weapon
{
    #region Public Fields

    //How fast the speed value will change
    public float acceleration = 4;

    //Sound played when firing
    public AudioClip au_shot;

    //Is the weapon automatic or semi-auto
    //True: Weapon will fire as long as the trigger is held down
    //False: Weapon will fire once only when the trigger is pressed
    public bool automatic = false;

    //Casing object to be instantiated
    public GameObject casing;

    //The relative force to be applied to the casing's rigidbody
    public Vector3 casingForce;

    //Where the casing should be spawned
    public Transform casingSpawn;

    public GameObject crosshair;

    //Magazine: all bullets are loaded at once
    //Single: bullets are loaded one at a time. Reloads take longer but can be interrupted
    public ReloadType loadType;

    //Bullets currently in the magazine
    public int mag;

    //Maximum size of the magazine
    public int magSize = 7;

    //Rate of fire
    public float rof = 0.1f;

    #endregion Public Fields



    #region Private Fields

    private Animator a;

    //Used to tell the animation controller which states to switch to
    private bool aiming;

    private AudioSource au;
    [SerializeField] private Camera cam;
    private CharacterController co;

    //Current speed. Used to tell the animation controller which movement animation to play
    private float curSpeed;

    private bool jumping;

    //Particles for muzzle flash
    private ParticleSystem[] ps;

    private bool reloading;

    //Used to keep track of when we can shoot
    private float shotTimer;

    #endregion Private Fields



    #region Public Enums

    //Define what type of reload the weapon uses
    public enum ReloadType
    {
        Magazine,
        Single
    }

    #endregion Public Enums



    #region Public Methods

    public void Eject()
    {
        //If there is no ammo left, activate the appropriate animation
        if (mag == 0)
        {
            NoAmmo(true);
        }

        //If there is a casing and a casing spawn point
        if (casing != null && casingSpawn != null)
        {
            //Instantiate the casing at the spawn point's position
            GameObject c = (GameObject)Instantiate(casing, casingSpawn.position, casingSpawn.rotation);
            //Apply force to the casing's rigidbody
            c.GetComponent<Rigidbody>().AddForce(casingSpawn.transform.TransformDirection(casingForce * Random.Range(0.9f, 1.1f)));
            //Mag the casing a child of the player
            //This will prevent the casing from going through the weapon when the player is shooting and moving at the same time
            //The casing has a script that will automatically unparent it from the player after a short time
            c.transform.SetParent(transform);
            Destroy(c, 0.25f);
        }
    }

    public void LoadMag()
    {
        //Refil the magazine
        mag = magSize;

        //Update the empty magazine animation
        NoAmmo(false);
    }

    public void LoadSingle()
    {
        //Add ammo
        mag++;
    }

    public void NoAmmo(bool s)
    {
        //In some weapons, there is an animation to show that the magazine is empty
        //This animation is being played constantly on the second layer of the animation controller
        //This layer will override the base layer

        //This function's purpose is to switch the layer on and off by changing the weight
        if (s)
            a.SetLayerWeight(1, 1);
        else
            a.SetLayerWeight(1, 0);
    }

    public override void Use()
    {
    }

    #endregion Public Methods



    #region Private Methods

    //This method handles when a weapon is fired.
    private void Shoot()
    {
        //Play the correct shooting animation
        if (aiming)
            a.CrossFade("AimShoot", 0.02f, 0, 0);
        else
            a.CrossFade("Shoot", 0.02f, 0, 0);

        //Reduce ammo
        if (mag > 0)
            mag--;

        //Play the shot sound if there is one
        if (au_shot != null && au != null)
            au.PlayOneShot(au_shot);

        //Emit particles for muzzle flash
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].Emit(10);
        }

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        ray.origin = cam.transform.position;

        //Detects if the players hits a damageable target and increases the score.
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.GetComponentInParent<IDamageable>()?.TakeDamage(((WeaponInfo)itemObjectInfo).damage);
            if (hit.collider.gameObject.GetComponentInParent<IDamageable>() != null)
            {
            }
            //Creates bullet collision effect with objects.
        }
    }

    // Use this for initialization
    private void Start()
    {
        a = gameObject.GetComponentInChildren<Animator>();
        au = gameObject.GetComponent<AudioSource>();

        mag = magSize;

        ps = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    private void Update()
    {
        crosshair.SetActive(true);
        //If the shot timer is greater than zero, reduce it by 1 per second
        if (shotTimer > 0)
            shotTimer -= Time.deltaTime;

        //Get the character controller if there is none
        if (co == null)
            co = gameObject.GetComponentInParent<CharacterController>();

        //Calculate the movement speed
        //The target speed is 0 by default
        int targetSpeed = 0;
        //Change it to 1 if there is input
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            targetSpeed = 1;
            //And to 2 if we are sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetSpeed = 2;
            }
        }

        //If we are not sprinting
        if (targetSpeed != 2)
        {
            //Check if we should be aiming
            aiming = Input.GetKey(KeyCode.Mouse1);
            crosshair.SetActive(false);

            //Shooting logic
            if (mag > 0)
            {
                if (automatic)
                {
                    //If the trigger is held and we can shoot
                    if (Input.GetKey(KeyCode.Mouse0) && shotTimer <= 0)
                    {
                        //Play the shoot animation
                        Shoot();

                        //Set the time in which we can't shoot again
                        shotTimer = rof;
                    }
                }
                else
                {
                    //If the trigger is pulled and we can shoot
                    if (Input.GetKeyDown(KeyCode.Mouse0) && shotTimer <= 0)
                    {
                        //Play the shoot animation
                        Shoot();

                        //Set the time in which we can't shoot again
                        shotTimer = rof;
                    }
                }
            }
        }
        else
        {
            //If we are sprinting, we are not allowed to aim
            aiming = false;
        }

        //Check if we are jumping
        jumping = Input.GetKey(KeyCode.Space);

        //Reload logic
        if (loadType == ReloadType.Magazine)
        {
            //Check if we should be reloading
            reloading = Input.GetKey(KeyCode.R);
        }
        else
        {
            //Start reloading when the R key is pressed
            if (Input.GetKey(KeyCode.R))
            {
                reloading = true;
            }
            //We want to keep reloading if the key is released, however

            //If there magazine is full, or there are any interruptions, stop reloading
            if (mag == magSize || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                reloading = false;
            }

            //The current ammo is increased by animation events
        }

        //Lerp the movement animation speed to the target speed
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, Time.deltaTime * acceleration);

        //Set the animation parameters
        a.SetBool("aiming", aiming);
        a.SetFloat("curSpeed", curSpeed);
        a.SetBool("jumping", jumping);
        a.SetBool("reloading", reloading);

        //If there a character controller, check if we are grounded
        if (co != null)
            a.SetBool("grounded", co.isGrounded);
        else
            a.SetBool("grounded", true);
    }

    #endregion Private Methods
}