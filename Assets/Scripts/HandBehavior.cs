using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandBehavior : MonoBehaviour

{
    // The spawn and direction the hand came in from. Used when making the hand retreat with the cookies.
    Vector3 spawn;
    Vector3 backwards;

    // The point on the hand that detects if the cookies are close enough
    public GameObject pivot;

    // Maximum velocity and rotation speed of the hand
    // 0.011 is a good value for velocity
    public float maxRotationSpeed;
    public float maxVelocity;

    // The plate of cookies, and its behavior script
    GameObject cookies;
    CookiesBehavior cb;

    // The control object that tracks the score
    public Control control;


    // Start is called before the first frame update
    void Start()
    {
        // Set the cookies variable and its behavior script, and set initial rotation.
        cookies = GameObject.FindWithTag("Cookies");
        control = GameObject.FindWithTag("Control").GetComponent<Control>();
        cb = cookies.gameObject.GetComponent<CookiesBehavior>();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, cookies.transform.position - transform.position);
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (cb.broken == false)
        {
            // Make sure the cookies exist, then check if this is hoding them. If it is, move back towards the spawn. If it isn't, move towards the cookies. 
            if (cookies != null)
            {
                if (cb.holder == gameObject)
                {
                    transform.position = Vector3.MoveTowards(transform.position, spawn, maxVelocity);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, cookies.transform.position, maxVelocity);
                }

                // If you are not holding the cookies, rotate towards them
                if (cb.holder != gameObject)
                {
                    transform.rotation = Quaternion.RotateTowards(pivot.transform.rotation, Quaternion.LookRotation(Vector3.forward, cookies.transform.position - pivot.transform.position), 0.1f);
                }

                // If we are close enough to the cookies and they don't have a holder, become the holder. 
                if (Vector3.Distance(transform.position, cookies.transform.position) < 0.1f & cb.holder == null)
                {
                    cb.SetHolder(gameObject);
                }
            }
        }
    }

    public void SlapHand()
        /**
         * Called when the hand is clicked. 
         */
    {
        control.score += 1;
        control.objects.Remove(gameObject);
        if(cb.holder == gameObject)
        {
            cb.ResetHolder();
        }
        Destroy(gameObject);
    }
}
