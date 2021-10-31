using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance;

	[SerializeField] public Menu[] menus;

	public void Awake()
	{
		Instance = this;
	}

	public void menuOpen(string menuName)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].menuName == menuName)
			{
				menus[i].Open();
			}
			else if (menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
	}

	public void menuOpen(Menu menu)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
		menu.Open();
	}

	public void CloseMenu(Menu menu)
	{
		menu.Close();
	}
}

