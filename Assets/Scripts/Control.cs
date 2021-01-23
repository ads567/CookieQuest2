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
    public GameObject spawner;
    public GUI gui;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner");
        gui = GameObject.FindWithTag("GUI").GetComponent<GUI>();
        objects = new List<GameObject>();
        objects.Add(GameObject.FindWithTag("Cookies"));
        score = 0;
        SetScore();

        // Disable all the game objects
        Pause();
    }

    public void Pause()
    {
        // Pause the hand spawning
        spawner.SetActive(false);

        // Pause all of the spawned hands and the cookies
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        // Hide the score text
        gui.score.gameObject.SetActive(false);

        // Show the pause menu
        gui.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        // Reset the score, the cookies, and the hands
        score = 0;
        cookies.transform.position = new Vector2(0, 0);
        cookies.GetComponent<CookiesBehavior>().ResetCookies();
        Resume();
    }

    public void Resume()
    {
        // Resume the hand spawning
        spawner.SetActive(true);
        spawner.GetComponent<SpawnerBehavior>().StartCoroutine(spawner.GetComponent<SpawnerBehavior>().Spawn());

        // Resume all of the spawned hands and the cookies
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }

        // Show the score text
        gui.score.gameObject.SetActive(true);

        // Hide the pause menu
        gui.gameObject.SetActive(false);

    }

    public void SetScore()
    {
        gui.score.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * Because of the unpredictable nature of overlapping colliders on the z axis, hit detection must be done carefully and deliberately. 
         * Also check for game pause here.
         */

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }


        // Check for a mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Create an array for all hits for the raycast at mouse position
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            // List of colliders, pulled from the hits (mostly just for convenience)
            List<Collider2D> colliders = new List<Collider2D>();

            // The cookies behavior script, from which we'll need the current holder of the cookies
            CookiesBehavior cb = cookies.GetComponent<CookiesBehavior>();

            // The highest index found for all colliders at the click position. The holder of the cookies is prioritized, then the highest hand after that and so on. 
            int highestIndex = 0;

            // Iterate through the RaycastHits and add their colliders to a list. Compare to the current highest index and update if it's higher. 
            foreach (RaycastHit2D h in hit)
            {
                colliders.Add(h.collider);
                if(colliders.IndexOf(h.collider) > highestIndex)
                {
                    highestIndex = colliders.IndexOf(h.collider);
                }
            }

            // If the cookies are the only thing that was clicked, destroy them
            if (colliders.Contains(cookies) & colliders.Count == 1)
            {
                cb.BreakCookies();
            }

            // If the cookies have a holder and it was in the clicked colliders, destroy it. 
            else if (cb.holder != null)
            {
                if (colliders.Contains(cb.holder.GetComponent<Collider2D>()))
                {
                    cb.holder.GetComponent<HandBehavior>().SlapHand();
                }
            }

            // Otherwise, destroy the "highest" hand
            else
            {
                if(colliders.Count > 0)
                {
                    colliders[highestIndex].gameObject.GetComponent<HandBehavior>().SlapHand();
                }
            }
        }
    }
}
