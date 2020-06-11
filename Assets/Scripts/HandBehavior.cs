using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehavior : MonoBehaviour

{
    GameObject cookies;


    // Start is called before the first frame update
    void Start()
    {
        cookies = GameObject.FindWithTag("Cookies");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cookies != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, cookies.transform.position, 0.015f);
            transform.rotation = Quaternion.SetLookRotation(cookies.transform.position, Vector3.up);
        }
    }
}
