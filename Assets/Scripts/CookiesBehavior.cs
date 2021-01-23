using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiesBehavior : MonoBehaviour
{
    public Sprite brokenCookies;
    public Sprite cookies;
    public bool broken;
    public GameObject holder;

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
        holder = null;
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

    public void BreakCookies()
        /**
         * This function is called when the plate is clicked on. It ends the game. 
         */
    {
        if(broken == false)
        {
            broken = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = brokenCookies;
            ResetHolder();
        }
        // Randomly rotate the shattered plate.
        transform.Rotate(Vector3.forward * Random.Range(15.0f, 180.0f));
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

    public void ResetCookies()
        /**
         * Reset the holder and the sprite for starting a new game
         */
    {
        broken = false;
        ResetHolder();
        gameObject.GetComponent<SpriteRenderer>().sprite = cookies;
    }
}
