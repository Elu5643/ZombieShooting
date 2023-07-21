using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    [SerializeField]
    ItemDB itemDB = null;


    static public DBManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<DBManager>();

            return instance;
        }
    }
    static DBManager instance = null;

    public ItemData GetItem(ItemData.ID id)
    {
        return itemDB.GetItem(id);
    }


    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
