using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maneged : MonoBehaviour
{
    Generator.DeleteEvent deleteEvent = null;

    public Generator.DeleteEvent DeleteEvent
    {
        set
        {
            deleteEvent = value;
        }
    }

    public void ExecuteEvent(GameObject obj)
    {
        deleteEvent(obj);
    }
}
