using System.Collections;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    #region Public Fields

    //The position of the current weapon in the weapon array
    public int cur = 0;

    //All the weapons in the inventory
    public GameObject[] weapons;

    #endregion Public Fields



    #region Private Methods

    private void Start()
    {
        //Iterate through all weapons and disable all but the current one
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == cur)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    private IEnumerator SwitchWeapon()
    {
        //Play ther current weapon's Lower animation
        weapons[cur].GetComponent<Animator>().CrossFade("Lower", 0.15f);

        //Give it time to finish
        yield return new WaitForSeconds(0.5f);

        //Disable the current weapon
        weapons[cur].SetActive(false);

        //Go to the next weapon in the array. If we reach the end of the array, go back to the start
        cur++;
        if (cur >= weapons.Length)
            cur = 0;

        //Activate the new current weapon
        weapons[cur].SetActive(true);

        //Play ther current weapon's Raise animation
        weapons[cur].GetComponent<Animator>().CrossFade("Raise", 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        //Switch weapon on TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartCoroutine("SwitchWeapon");
        }
    }

    #endregion Private Methods
}