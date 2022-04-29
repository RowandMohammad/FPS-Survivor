using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviourPunCallbacks
{
    #region Public Fields

    public int itemIndex;
    public Animator pAnimator;
    public int previousItemIndex = -1;
    public Animator sAnimator;
    public int shootCounter;

    #endregion Public Fields



    #region Private Fields

    private bool isSwitching;
    [SerializeField] private Item[] items;

    #endregion Private Fields



    #region Private Methods

    private void Awake()
    {
        setAnimator();
    }

    //handles the management of the weapons
    private void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            if (itemIndex == 0)
            {
                StartCoroutine(equipPrimary());
            }
            if (itemIndex == 1)
            {
                StartCoroutine(equipSecondary());
            }

            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;
    }

    //Coroutine to equio primary weapon
    private IEnumerator equipPrimary()
    {
        pAnimator.Play("switchIn");
        yield return new WaitForSeconds(pAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    //Coroutine to equip secondary weapon
    private IEnumerator equipSecondary()
    {
        sAnimator.Play("switchIn");
        yield return new WaitForSeconds(sAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    //initialises animator controllers for weapons
    private void setAnimator()
    {
        Animator[] animators = GetComponentsInChildren<Animator>();
        pAnimator = animators[0];
        sAnimator = animators[1];
    }

    // Start is called before the first frame update
    private void Start()
    {
        EquipItem(0);
    }



    //Coroutine to unequip primary weapon
    private IEnumerator unequipPrimary()
    {
        pAnimator.Play("switchOut");
        yield return new WaitForSeconds(pAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    //Coroutine to unequip secondary weapon
    private IEnumerator unequipSecondary()
    {
        sAnimator.Play("switchOut");
        yield return new WaitForSeconds(sAnimator.GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                isSwitching = true;
                EquipItem(i);
                isSwitching = false;
                break;
            }
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            isSwitching = true;
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
            isSwitching = false;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            isSwitching = true;
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
            isSwitching = false;
        }
        if (Input.GetMouseButtonDown(0) && isSwitching == false)
        {
            shootCounter += 1;

            items[itemIndex].Use();
        }
    }

    #endregion Private Methods
}