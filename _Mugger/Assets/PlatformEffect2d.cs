using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect2d : MonoBehaviour
{
    PlatformEffector2D plat;
    float WaitTime = .5f;
    // Start is called before the first frame update
    void Start()
    {
        plat = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (WaitTime <= 0)
                plat.GetComponent<PlatformEffector2D>().rotationalOffset = 180f;
            else
                WaitTime -= Time.deltaTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        WaitTime = .5f;
    }
}
