using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Vector2 CurrentVelocity;
    public Transform player;
    public float smoothSpeed; 
    public Vector3 offset;
    //public float smoothTimeX;
    //public float smoothTimeY;

    //public float zoomSpeed = 3f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void FixedUpdate()
    {
        Vector3 setPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, setPos, smoothSpeed);
        transform.position = smoothPos;

        transform.LookAt(smoothPos);
        //zoomMouseWheel();
        //x,y_direction
        //float x_Pos = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref CurrentVelocity.x, smoothTimeX * Time.deltaTime);
        //float y_Pos = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref CurrentVelocity.y, smoothTimeY * Time.deltaTime);

        //transform.position = new Vector3(x_Pos, y_Pos, transform.position.z);
    }

















    //void zoomMouseWheel()
    //{
    //    float zoomSpeedAmount = 3f;
    //    if (Input.GetKey(KeyCode.KeypadPlus))
    //    {
    //        zoomSpeed -= zoomSpeedAmount * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.KeypadMinus))
    //    {
    //        zoomSpeed += zoomSpeedAmount * Time.deltaTime;
    //    }
    //    if(Input.mouseScrollDelta.y > 0)
    //    {
    //        zoomSpeed -= zoomSpeedAmount * Time.deltaTime;
    //    }
    //    if(Input.mouseScrollDelta.y < 0)
    //    {
    //        zoomSpeed += zoomSpeedAmount * Time.deltaTime;
    //    }
    //    zoomSpeed = Mathf.Clamp(zoomSpeed, 2f, 3f);
    //}
}
