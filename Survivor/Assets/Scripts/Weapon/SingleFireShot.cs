using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireShot : Weapon
{
	[SerializeField] Camera cam;
	public ParticleSystem hitEffect;


	void Awake()
	{
		
	}

	public override void Use()
	{
		Shoot();
	}

	void Shoot()
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		ray.origin = cam.transform.position;
		
		if (Physics.Raycast(ray, out hit))
		{
			hit.collider.gameObject.GetComponentInParent<IDamageable>()?.TakeDamage(((WeaponInfo)itemObjectInfo).damage);
			hitEffect.transform.position = hit.point;
			hitEffect.transform.forward = hit.normal;
			hitEffect.Emit(1);
		}
	}
}
