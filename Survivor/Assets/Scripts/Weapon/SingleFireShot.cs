using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireShot : Weapon
{
	[SerializeField] Camera cam;
	public GameObject hitEffect;
	public Animator animator;
	public AudioSource gunShoot;
	private float nextFire;
	ScoreDisplay scoreDisplay;






    void Awake()
	{
		scoreDisplay= GameObject.Find("ScoreBoard").GetComponent<ScoreDisplay>();


	}

	public override void Use()
	{
		//Checks whether user can fire the weapon again in relation to fire rate of weapon.
		if (Time.time > nextFire)
		{
			nextFire = Time.time + ((WeaponInfo)itemObjectInfo).fireRate;
			Shoot();
		}
	}

	//This method handles when a weapon is fired.
	void Shoot()
	{

		//Plays the shooting animation like recoil for the weapon.
		animator.Play("shooting");
		//Plays the sounds of the weapon firing.
		gunShoot.Play();

		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		ray.origin = cam.transform.position;

		//Detects if the players hits a damageable target and increases the score.
		if (Physics.Raycast(ray, out hit))
		{
			hit.collider.gameObject.GetComponentInParent<IDamageable>()?.TakeDamage(((WeaponInfo)itemObjectInfo).damage);
			if (hit.collider.gameObject.GetComponentInParent<IDamageable>() != null)
			{ 
				//Increases score if user hits a target.
				scoreDisplay.successfulHits += 1;
			}
			//Creates bullet collision effect with objects.
			GameObject impactGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactGO, 0.125f);
		}
	}
}
