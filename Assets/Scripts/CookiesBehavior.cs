using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiesBehavior : MonoBehaviour
{

    public bool broken;
    public GameObject holder;

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the cookies are currently held, and if they are, stay with the holder.
        if(holder != null)
        {
            transform.position = holder.transform.position;
        }
    }

    void BreakCookies()
        /**
         * This function is called when the plate is clicked on. It ends the game. 
         */
    {
        if(broken == false)
        {
            broken = true;
        }
        // Randomly rotate the shattered plate.
        transform.Rotate(Vector3.forward * Random.Range(15.0f, 180.0f));
    }

    private void OnMouseDown()
        /**
         * Function that checks if the cookies have been clicked on. Calls BreakCookies()
         */
    {
        Debug.Log("Mouse click on cookies detected");
        BreakCookies();
    }

    public void SetHolder(GameObject newHolder)
        /**
         * Sets the current holder of the plate. Called from the HandBehavior script when a hand gets close enough.
         */
    {
        holder = newHolder;
    }

    public void ResetHolder()
        /**
         * Resets the holder variable
         */
    {
        holder = null;
    }
}
