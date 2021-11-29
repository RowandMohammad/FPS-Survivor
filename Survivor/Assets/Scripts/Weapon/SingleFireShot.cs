using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireShot : Weapon
{
	[SerializeField] Camera cam;


	void Awake()
	{
		
	}

	public override void Use()
	{
		Shoot();
	}

	void Shoot()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		ray.origin = cam.transform.position;
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			Debug.Log("I hit " + hit.collider.gameObject.name);
		}
	}
}
