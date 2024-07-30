using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUiCamer : MonoBehaviour
{
    private Transform maincamertr;
    private Transform tr;
    void Start()
    {
        maincamertr = Camera.main.transform;
        tr = transform;
    }

   
    void Update()
    {
        tr.LookAt(maincamertr);
    }
}
