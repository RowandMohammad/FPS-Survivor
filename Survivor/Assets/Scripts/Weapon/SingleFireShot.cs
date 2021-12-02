using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireShot : Weapon
{
	[SerializeField] Camera cam;
	public GameObject hitEffect;
	private Animator _animator;


	
	


	void Awake()
	{
		_animator = GetComponentInChildren<Animator>();
	}

	public override void Use()
	{
		Shoot();
	}

	void Shoot()
	{
		_animator.Play("shooting");
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		ray.origin = cam.transform.position;

		if (Physics.Raycast(ray, out hit))
		{
			hit.collider.gameObject.GetComponentInParent<IDamageable>()?.TakeDamage(((WeaponInfo)itemObjectInfo).damage);

			GameObject impactGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(impactGO, 0.75f);
		}
	}
}
