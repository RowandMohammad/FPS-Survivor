using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
	public ItemObjectInfo itemObjectInfo;
	public GameObject itemGameObject;

	public abstract void Use();

	
}
