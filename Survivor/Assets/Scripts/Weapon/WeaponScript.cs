using UnityEngine;
using System.Collections;
using TMPro;

public class WeaponScript : Weapon
{

	Animator a;
	CharacterController co;
	AudioSource au;

	[SerializeField] Camera cam;
	public GameObject hitEffect;
	public TextMeshProUGUI currentMag;
	public TextMeshProUGUI totalAmmoText;
	public TextMeshProUGUI weaponName;
	public GameObject hitMarker;

	//Sound played when firing
	public AudioClip au_shot;
	public AudioClip au_rel;
	public AudioClip au_shotSingle;
	public AudioClip au_hitmarker;


	public string name;

	//Maximum size of the magazine
	public int magSize = 7;
	//Bullets currently in the magazine
	public int mag;

	public int totalAmmo;

	public int weaponDamage;

	//Define what type of reload the weapon uses
	public enum ReloadType {
		Magazine,
		Single
	}
	//Magazine: all bullets are loaded at once
	//Single: bullets are loaded one at a time. Reloads take longer but can be interrupted
	public ReloadType loadType;

	//Rate of fire
	public float rof = 0.1f;
	//Used to keep track of when we can shoot
	float shotTimer;

	//Is the weapon automatic or semi-auto
	//True: Weapon will fire as long as the trigger is held down
	//False: Weapon will fire once only when the trigger is pressed
	public bool automatic = false;

	//Used to tell the animation controller which states to switch to
	bool aiming;
	bool jumping;
	bool reloading;

	//Current speed. Used to tell the animation controller which movement animation to play
	float curSpeed;

	//How fast the speed value will change
	public float acceleration = 4;

	//Casing object to be instantiated
	public GameObject casing;
	//Where the casing should be spawned
	public Transform casingSpawn;
	//The relative force to be applied to the casing's rigidbody
	public Vector3 casingForce;

	//Particles for muzzle flash
	ParticleSystem[] ps;
    public GameObject crosshair;

    // Use this for initialization
    void Start () {
		a = gameObject.GetComponentInChildren<Animator> ();
		au = gameObject.GetComponent<AudioSource> ();

		mag = magSize;

		ps = gameObject.GetComponentsInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {

		currentMag.text = mag.ToString("0");
		totalAmmoText.text = totalAmmo.ToString("0");
		weaponName.text = name;
		if (aiming)
			crosshair.SetActive(false);
        else
        {
			crosshair.SetActive(true);
		}
		//If the shot timer is greater than zero, reduce it by 1 per second
		if (shotTimer > 0)
			shotTimer -= Time.deltaTime;

		//Get the character controller if there is none
		if (co == null)
			co = gameObject.GetComponentInParent<CharacterController> ();

		//Calculate the movement speed
		//The target speed is 0 by default
		int targetSpeed = 0;
		//Change it to 1 if there is input
		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
			targetSpeed = 1;
			//And to 2 if we are sprinting
			if (Input.GetKey(KeyCode.LeftShift)) {
				targetSpeed = 2;
			}
		}

		//If we are not sprinting
		if (targetSpeed != 2) {
			//Check if we should be aiming
			aiming = Input.GetKey (KeyCode.Mouse1);


			//Shooting logic
			if (mag > 0) {
				if (automatic) {
					//If the trigger is held and we can shoot
					if (Input.GetKey (KeyCode.Mouse0) && shotTimer <= 0) {
						//Play the shoot animation
						Shoot ();

						//Set the time in which we can't shoot again
						shotTimer = rof;
					}
				} else {
					//If the trigger is pulled and we can shoot
					if (Input.GetKeyDown (KeyCode.Mouse0) && shotTimer <= 0) {
						//Play the shoot animation
						Shoot ();

						//Set the time in which we can't shoot again
						shotTimer = rof;
					}
				}
			}
		} else {
			//If we are sprinting, we are not allowed to aim
			aiming = false;
		}

		//Check if we are jumping
		jumping = Input.GetKey (KeyCode.Space);

		//Reload logic
		if (loadType == ReloadType.Magazine) {
			//Check if we should be reloading
			reloading = Input.GetKey (KeyCode.R) && totalAmmo > 0;
		} else {
			//Start reloading when the R key is pressed
			if (Input.GetKey (KeyCode.R) && totalAmmo > 0) {
				reloading = true;


			}
			//We want to keep reloading if the key is released, however

			//If there magazine is full, or there are any interruptions, stop reloading
			if (mag == magSize || Input.GetKey (KeyCode.Mouse0) || Input.GetKey (KeyCode.Mouse1) || totalAmmo==0) {
				reloading = false;

			}

			//The current ammo is increased by animation events
		}

		//Lerp the movement animation speed to the target speed
		curSpeed = Mathf.Lerp(curSpeed, targetSpeed, Time.deltaTime * acceleration);

		//Set the animation parameters
		a.SetBool ("aiming", aiming);
		a.SetFloat("curSpeed",curSpeed);
		a.SetBool ("jumping", jumping);
		a.SetBool ("reloading", reloading);

		//If there a character controller, check if we are grounded
		if (co != null)
			a.SetBool ("grounded", co.isGrounded);
		else
			a.SetBool ("grounded", true);
	}

	private void HitActive()
    {
		hitMarker.SetActive(true);
    }
	private void HitDisable()
	{
		hitMarker.SetActive(false);
	}


	void Shoot () {
		//Play the correct shooting animation
		if (aiming)
        {
			a.CrossFade("AimShoot", 0.02f, 0, 0);
		}
		else
			a.CrossFade ("Shoot", 0.02f, 0, 0);

		//Reduce ammo
		if (mag > 0)
			mag--;

		//Play the shot sound if there is one
		if (au_shot != null && au != null)
			au.PlayOneShot (au_shot);
		    au.PlayOneShot (au_shotSingle);

		//Emit particles for muzzle flash
		for (int i = 0;i < ps.Length;i++) {
			ps[i].Emit (10);
		}

		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		ray.origin = cam.transform.position;

		//Detects if the players hits a damageable target and increases the score.
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider.tag == "Head")
			{
				hit.collider.gameObject.GetComponentInParent<IEnemyDamageable>()?.TakeDamage(weaponDamage * 5);
				GameObject impactGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(impactGO, 0.4f);
				HitActive();
				au.PlayOneShot(au_hitmarker);
				Invoke("HitDisable", 0.15f);


			}
			else if (hit.collider.gameObject.GetComponent<IEnemyDamageable>() != null)
			{
				hit.collider.gameObject.GetComponent<IEnemyDamageable>()?.TakeDamage(weaponDamage);
				GameObject impactGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(impactGO, 0.4f);
				HitActive();
				au.PlayOneShot(au_hitmarker);
				Invoke("HitDisable", 0.15f);
			}

			//Creates bullet collision effect with objects.

		}
	}

	public void LoadMag () {
		//Refil the magazine

		int tempAmmo = magSize - mag;
		
		if (totalAmmo >= 0){
			if (au_rel != null && au != null)
				au.PlayOneShot(au_rel);

			
			if (totalAmmo + mag < 7)
            {
				mag = mag + totalAmmo;
            }
            else
            {
				mag = magSize;

			}
			




			totalAmmo = totalAmmo - tempAmmo;
			if (totalAmmo <= 0)
			{
				totalAmmo = 0;
			}


			NoAmmo(false);
		}

        else
        {
			NoAmmo(true);
        }
		


		//Update the empty magazine animation
		
	}

	public void LoadSingle()
	{
		//Add ammo
		if (totalAmmo > 0)
		{

			if (au_rel != null && au != null)
			{
				au.PlayOneShot(au_rel);
			}

			mag++;
			totalAmmo--;

		}
	

		
		
        
	}

	public void Eject () {
		//If there is no ammo left, activate the appropriate animation
		if (mag == 0) {
			NoAmmo (true);
		}

		//If there is a casing and a casing spawn point
		if (casing != null && casingSpawn != null) {
			//Instantiate the casing at the spawn point's position
			GameObject c = (GameObject)Instantiate (casing, casingSpawn.position, casingSpawn.rotation);
			//Apply force to the casing's rigidbody
			c.GetComponent<Rigidbody> ().AddForce (casingSpawn.transform.TransformDirection(casingForce*Random.Range(0.9f,1.1f)));
			//Mag the casing a child of the player
			//This will prevent the casing from going through the weapon when the player is shooting and moving at the same time
			//The casing has a script that will automatically unparent it from the player after a short time
			c.transform.SetParent (transform);
			Destroy(c, 0.25f);
		}
	}


	public void NoAmmo (bool s) {
		//In some weapons, there is an animation to show that the magazine is empty
		//This animation is being played constantly on the second layer of the animation controller
		//This layer will override the base layer

		//This function's purpose is to switch the layer on and off by changing the weight
		if (s)
			a.SetLayerWeight (1, 1);
		else
			a.SetLayerWeight (1, 0);
	}

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}

