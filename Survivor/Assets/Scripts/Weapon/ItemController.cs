using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviourPunCallbacks
{
    [SerializeField] Item[] items;
    public int itemIndex;
    public int previousItemIndex = -1;
	public Animator pAnimator;
	public Animator sAnimator;
	void setAnimator()
	{

		Animator[] animators = GetComponentsInChildren<Animator>();
		pAnimator = animators[0];
		sAnimator = animators[1];


	}
	private void Awake()
    {

		setAnimator();

	}


    // Start is called before the first frame update
    void Start()
    {
        EquipItem(0);
    }

    // Update is called once per frame
    void Update()
    {
		for (int i = 0; i < items.Length; i++)
		{
			if (Input.GetKeyDown((i + 1).ToString()))
			{
				EquipItem(i);
				break;
			}
		}
		if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
		{
			if (itemIndex >= items.Length - 1)
			{
				EquipItem(0);
			}
			else
			{
				EquipItem(itemIndex + 1);
			}
		}
		else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
		{
			if (itemIndex <= 0)
			{
				EquipItem(items.Length - 1);
			}
			else
			{
				EquipItem(itemIndex - 1);
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			items[itemIndex].Use();
		}
	}
	void EquipItem(int _index)
	{
		if (_index == previousItemIndex)
			return;

		itemIndex = _index;

		items[itemIndex].itemGameObject.SetActive(true);

		if (previousItemIndex != -1)
		{
			items[previousItemIndex].itemGameObject.SetActive(false);
		}

		previousItemIndex = itemIndex;

	}
}
