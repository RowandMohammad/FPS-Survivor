using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireShot : Weapon
{
    public override void Use()
    {
        Debug.Log("using gun" + itemObjectInfo.itemName);
    }
}
