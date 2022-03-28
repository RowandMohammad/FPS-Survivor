using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Instances of the type weapon can be easily created and stored in the project as ".asset" files
[CreateAssetMenuAttribute(menuName = "SuriviorFPS/New Weapon")]
public class WeaponInfo : ItemObjectInfo
{
    public GameObject weaponPrefab;
    //This variable stores the damage value of each bullet produced by a weapon
    public float damage;

    //This variable stores the rate of fire of a weapon
    public float rof;

    public AudioClip au_shot;

    public int magSize;

    public GameObject casing;


}

