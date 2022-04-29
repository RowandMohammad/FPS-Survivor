using UnityEngine;

//Instances of the type weapon can be easily created and stored in the project as ".asset" files
[CreateAssetMenuAttribute(menuName = "SuriviorFPS/New Weapon")]
public class WeaponInfo : ItemObjectInfo
{
    #region Public Fields

    //This variable stores the audio of the wepaons fire
    public AudioClip au_shot;

    //This variable stores the object the casing of the weapons fire
    public GameObject casing;

    //This variable stores the damage value of each bullet produced by a weapon
    public float damage;

    //This variable stores the magazine size value of  a weapon
    public int magSize;

    //This variable stores the rate of fire of a weapon
    public float rof;

    //This bariable stores the weapon object
    public GameObject weaponPrefab;

    #endregion Public Fields
}