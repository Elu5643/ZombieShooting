using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : SingletonMonoBehaviour<DBManager>
{
    protected override bool dontDestroyOnLoad { get { return true; } }

    [SerializeField]
    ItemDB itemDB = null;

    public ItemData GetItem(ItemData.ID id)
    {
        return itemDB.GetItem(id);
    }
}
