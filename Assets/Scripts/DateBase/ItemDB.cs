using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDB : ScriptableObject
{
	[SerializeField]
	List<ItemData> list = null;

	public ItemData GetItem(ItemData.ID id)
	{
		foreach (ItemData item in list)
		{
			if (item.Id == id)
			{
				return item;
			}
		}
		return null;
	}
}
