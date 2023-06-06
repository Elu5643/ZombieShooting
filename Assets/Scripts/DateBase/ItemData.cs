using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	ItemData
		ItemDBで使用するためのデータ
*/
[System.Serializable]
public class ItemData
{
	public enum EffectType
	{
		Non,
		Heal,
		Buff,
		Debuff,
	}

	public enum ID
	{
		Non,
		Item01,
	}


	[SerializeField]
	ID id;              // アイテムID

	[SerializeField]
	string itemName;    // アイテム名

	[SerializeField]
	Sprite image;       // アイテム画像

	[SerializeField]
	EffectType effect;  // 効果

	[SerializeField]	// 個数
	int num;

	public ID Id
	{
		get
		{
			return id;
		}
	}

	public string ItemName
	{
		get
		{
			return itemName;
		}
	}

	public Sprite Image
	{
		get
		{
			return image;
		}
	}

	public EffectType Effect
	{
		get
		{
			return effect;
		}
	}

	public int Num
    {
		get 
		{ 
			return num;
		}
    }
}
