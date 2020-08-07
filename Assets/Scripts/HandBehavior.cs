using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandBehavior : MonoBehaviour

{
    GameObject cookies;
    CookiesBehavior cb;
    Quaternion target;


    // Start is called before the first frame update
    void Start()
    {
        cookies = GameObject.FindWithTag("Cookies");
        cb = cookies.gameObject.GetComponent<CookiesBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cookies != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, cookies.transform.position, 0.011f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, cookies.transform.position - transform.position);
        }
    }
}
