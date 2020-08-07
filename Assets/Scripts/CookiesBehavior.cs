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
        if(holder != null)
        {
            transform.position = holder.transform.position;
        }
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

    public void SetHolder(GameObject newHolder)
    {
        holder = newHolder;
    }

    public void ResetHolder()
    {
        holder = null;
    }
}
