using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    private float legth, startpos;
    public GameObject cam;
    public float parralaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        legth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parralaxEffect));
        float dist = (cam.transform.position.x * parralaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + legth) startpos += legth;
        else if (temp < startpos -legth) startpos -= legth;
    }
}
