using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookiesBehavior : MonoBehaviour
{

    public bool broken;

    // Start is called before the first frame update
    void Start()
    {
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BreakCookies()
    {
        if(broken == false)
        {
            broken = true;
        }
        transform.Rotate(Vector3.forward * Random.Range(15.0f, 180.0f));
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse click on cookies detected");
        BreakCookies();
    }
}
