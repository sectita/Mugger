//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BikeAccleration : MonoBehaviour
//{
//    public Rigidbody2D rb;
//    public float acc;

//    public GameObject BikeSeat;
//    public Transform Player, _BikeSeat;
//    //public Animator ani;

//    public float BikePickRange;

//    public bool BikeEqupied;
//    public bool BikeSlotFull;

//    private void Start()
//    {
//        if (!BikeEqupied)
//        {
//            BikeSeat.gameObject.SetActive(false);
//        }
//        if (BikeEqupied)
//        {
//            BikeSeat.gameObject.SetActive(true);
//            BikeSlotFull = true;
//        }
//    }

//    private void FixedUpdate()
//    {
        
//        Vector2 distanceToplayer = Player.position - transform.position;
//        if(!BikeEqupied && distanceToplayer.magnitude <= BikePickRange && Input.GetKeyDown(KeyCode.E) && !BikeSlotFull)
//        {
//            PickBikeAcc();
//            Controller.Instance.GetComponent<Rigidbody2D>().isKinematic = true;
//            Controller.Instance.direction = false;


//            Controller.Instance._speed = 0f;
//            Controller.Instance.anim.SetBool("Sit", true);
//            Controller.Instance.IsJumping = false;
//        }
//        if(BikeEqupied && Input.GetKeyDown(KeyCode.Q))
//        {
//            DropBikeAcc();
//            Controller.Instance.GetComponent<Rigidbody2D>().isKinematic = false;
//            Controller.Instance.direction = true;

//            Controller.Instance._speed = 240f;
//            Controller.Instance.anim.SetBool("Sit", false);
//            Controller.Instance.IsJumping = true;
//        }
//    }

//    void PickBikeAcc()
//    {
//        BikeEqupied = true;
//        BikeSlotFull = true;
//        transform.SetParent(_BikeSeat);

//        transform.position = _BikeSeat.position;
//        transform.rotation = Quaternion.Euler(Vector2.zero);
//        BikeSeat.gameObject.SetActive(true);
        
//        if (BikeEqupied && Input.GetKeyDown(KeyCode.D))
//        {
//            rb.MovePosition(rb.position + new Vector2(1f, 0f) * acc * Time.fixedDeltaTime);
//        }
//        else if(Input.GetKeyUp(KeyCode.D) || Input.GetKey(KeyCode.A))
//        {
//            rb.MovePosition(rb.position + new Vector2(0f, 0f) * Time.fixedDeltaTime);
//        }
        
        
//    }

//    void DropBikeAcc()
//    {
//        if (!BikeEqupied)
//        {
//            rb.velocity = new Vector2(0f, 0f);
//        }
//        BikeEqupied = false;
//        BikeSlotFull = false;
//        transform.SetParent(null);
//        BikeSeat.gameObject.SetActive(false);
        
//    }
//}
