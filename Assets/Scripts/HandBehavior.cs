using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandBehavior : MonoBehaviour

{
    Vector3 spawn;
    GameObject cookies;
    CookiesBehavior cb;
    Quaternion target;
    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        cookies = GameObject.FindWithTag("Cookies");
        cb = cookies.gameObject.GetComponent<CookiesBehavior>();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, cookies.transform.position - transform.position);
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cookies != null)
        {
            if(cb.holder == gameObject)
            {
                transform.position = Vector3.MoveTowards(transform.position, spawn, 0.011f);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, cookies.transform.position, 0.011f);
            }
            
            transform.rotation = Quaternion.LookRotation(Vector3.forward, cookies.transform.position - transform.position);



            if(Vector3.Distance(transform.position, cookies.transform.position) < 0.1f & cb.holder == null)
            {
                cb.SetHolder(gameObject);
                direction *= -1.0f;
            }
        }
    }
}
