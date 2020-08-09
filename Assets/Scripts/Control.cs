using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class Control : MonoBehaviour
{
    public int score;
    public List<GameObject> objects;
    public Collider2D cookies;

    // Start is called before the first frame update
    void Start()
    {
        objects = new List<GameObject>();
        objects.Add(GameObject.FindWithTag("Cookies"));
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            List<Collider2D> colliders = new List<Collider2D>();
            CookiesBehavior cb = cookies.GetComponent<CookiesBehavior>();
            int highestIndex = 0;

            foreach (RaycastHit2D h in hit)
            {
                colliders.Add(h.collider);
                if(colliders.IndexOf(h.collider) > highestIndex)
                {
                    highestIndex = colliders.IndexOf(h.collider);
                }
            }

            if (colliders.Contains(cookies) & colliders.Count == 1)
            {
                cb.BreakCookies();
            }
            else if (cb.holder != null)
            {
                if (colliders.Contains(cb.holder.GetComponent<Collider2D>()))
                {
                    cb.holder.GetComponent<HandBehavior>().SlapHand();
                }
            }
            else
            {
                if(colliders.Count > 0)
                {
                    colliders[highestIndex].gameObject.GetComponent<HandBehavior>().SlapHand();
                }
            }











            /*
            // Iterate through each object under the mouse
            foreach(RaycastHit2D h in hit)
            {
                int highestIndex = 0;
                // If the object clicked was the cookies, break them
                if (h.collider.CompareTag("Cookies") & hit.Length == 1)
                {
                    GameObject.FindWithTag("Cookies").GetComponent<CookiesBehavior>().BreakCookies();
                }

                // If the object being checked is a hand
                else if (h.collider.CompareTag("Hand"))
                {
                    // If the hand is holding the cookies, slap that hand
                    if(h.collider.gameObject == GameObject.FindWithTag("Cookies").GetComponent<CookiesBehavior>().holder)
                    {
                        h.collider.gameObject.GetComponent<HandBehavior>().SlapHand();
                    }

                    // Otherwise, check if it is the highest hand. If it is, update the highest hand index
                    else
                    {
                        if(System.Array.IndexOf(hit, h.collider.gameObject) > highestIndex)
                        {
                            highestIndex = System.Array.IndexOf(hit, h);

                            // If it's the last collider, delete the one with the highest index
                            if(System.Array.IndexOf(hit, h) == hit.Length - 1)
                            {
                                Debug.Log("Made it here");

                                foreach (GameObject g in objects)
                                {
                                    if (g == hit[highestIndex].collider.gameObject)
                                    {                                      
                                        g.GetComponent<HandBehavior>().SlapHand();
                                    }
                                }
                            }
                        }                       
                    }
                }
            }
            */
        }
    }
}
