using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	ItemData
		ItemDB�Ŏg�p���邽�߂̃f�[�^
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
	ID id;              // �A�C�e��ID

	[SerializeField]
	string itemName;    // �A�C�e����

	[SerializeField]
	Sprite image;       // �A�C�e���摜

	[SerializeField]
	EffectType effect;  // ����

	[SerializeField]	// ��
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
