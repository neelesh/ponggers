using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
     if (!Cursor.visible) Cursor.visible = true;   
    }
}
