using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpawnerBehavior : MonoBehaviour
{
    public float minSpawnTime;
    public float maxSpawnTime;
    public GameObject hand;
    public Control control;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
        /**
         * Coroutine for spawning hands. 
         */
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        // Choose a side of the screen to spawn the hand on (0 = left, 1 = top, 2 = right, 3 = bottom)
        int side = Random.Range(0, 4);
        Camera cam = Camera.current;
        switch (side)
        {
            case 0:
                control.objects.Add(Instantiate(hand, new Vector3(-10, Random.Range(-6, 7), 0), transform.rotation));
                break;

            case 1:
                control.objects.Add(Instantiate(hand, new Vector3(Random.Range(-4, 5), 12, 0), transform.rotation));
                break;

            case 2:
                control.objects.Add(Instantiate(hand, new Vector3(10, Random.Range(-6, 7), 0), transform.rotation));
                break;

            case 3:
                control.objects.Add(Instantiate(hand, new Vector3(Random.Range(-4, 4), -12, 0), transform.rotation));
                break;
        }
        StartCoroutine(Spawn());

        
    }
}
