using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandBehavior : MonoBehaviour

{
    // Where the hand came in from. Used when making the hand retreat with the cookies.
    Vector3 spawn;

    // The plate of cookies, and its behavior script
    GameObject cookies;
    CookiesBehavior cb;


    // Start is called before the first frame update
    void Start()
    {
        // Set the cookies variable and its behavior script, and set initial rotation and spawn point. 
        cookies = GameObject.FindWithTag("Cookies");
        cb = cookies.gameObject.GetComponent<CookiesBehavior>();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, cookies.transform.position - transform.position);
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure the cookies exist, then check if this is hoding them. If it is, move back towards the spawn. If it isn't, move towards the cookies. 
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
            
            // Rotate to face the cookies. 
            transform.rotation = Quaternion.LookRotation(Vector3.forward, cookies.transform.position - transform.position);

            // If we are close enough to the cookies and they don't have a holder, become the holder. 
            if(Vector3.Distance(transform.position, cookies.transform.position) < 0.1f & cb.holder == null)
            {
                cb.SetHolder(gameObject);
            }
        }
    }
}
