using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private AiEnemy aienemyp;

    public float PatrolSpeed;
    public Transform[] patrolCheckPoints;
    public Transform PatrolCurrentTransform, PatrolPlayerTransform;
    //float waitingTime;

    // Start is called before the first frame update
    void Start()
    {
      aienemyp = GetComponent<AiEnemy>();
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < patrolCheckPoints.Length; i++)
        {
            if (transform.position != patrolCheckPoints[i].position)
            {
                aienemyp.ani.SetBool("walk", true);
                transform.position = Vector2.MoveTowards(transform.position, patrolCheckPoints[i].position, PatrolSpeed * Time.deltaTime);
            }
            //else if (i +1 < patrolCheckPoints.Length)
            //{
            //    i++;
            //}
            //else
            //i = 0;
        }

        //flip

        if (PatrolCurrentTransform.position.x < PatrolPlayerTransform.position.x)
        {
            PatrolCurrentTransform.localScale = new Vector3(-0.04489056f, 0.04489056f, 0.04489056f);
        }
        else if (PatrolCurrentTransform.position.x > PatrolPlayerTransform.position.x)
        {
            PatrolCurrentTransform.localScale = new Vector3(0.04489056f, 0.04489056f, 0.04489056f);
        }
    }
}
