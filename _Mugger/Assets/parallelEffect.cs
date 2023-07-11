using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallelEffect : MonoBehaviour
{
    
    private float length, startpos;
    public GameObject cam;
    public float parallel_Effect;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallel_Effect));
        float dis = (cam.transform.position.x * parallel_Effect);
        transform.position = new Vector3(startpos + dis, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }

}
