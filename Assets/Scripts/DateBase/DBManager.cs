using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
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


    [SerializeField]
    ItemDB itemDB;


    void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
