using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        EquipItem(0);
    }

    // Update is called once per frame
    void Update()
    {
        EquipItem(0);
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
