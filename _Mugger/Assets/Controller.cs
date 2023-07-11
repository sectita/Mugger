using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public AiEnemy aienemyc;

    public static Controller Instance;
    public float _speed;
    Rigidbody2D rb;
    public float _move;

    public Animator anim;

    public float _JumpForce;
    private bool IsGrounded;
    public Transform[] feetPos;
    public float circleRadius;
    public LayerMask layerGround;
    public LayerMask AttackMask;

    private float JumpTimeCounter;
    public float JumpTime;
    public bool IsJumping;
    private bool IsRunning;
    public bool direction; // scale flip variable

    public bool receiveInput;
    public bool receivedInput;
    public bool attack = false;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpTimeCounter = JumpTime;
        anim = GetComponent<Animator>();
        //aienemyc = GetComponent<AiEnemy>();
     }

    // Update is called once per frame
    void FixedUpdate()
    {
        JumpFun();
        MoveFun();
        InputManager();
        AttackFun();
        stickWeapon();
        VisionRay();

    }
    
    void MoveFun()
    {
        //move
        _move = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        rb.velocity = new Vector2(_move * _speed, rb.velocity.y);

        if(_move == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        #region Scale ForFlip
        //flip
        //if ((_move < 0 && !direction) || (_move > 0 && direction))
        //{
        //    direction = !direction;
        //    Vector3 Scaler = transform.localScale;
        //    Scaler.x *= -1;
        //    transform.localScale = Scaler;
        //}
        #endregion

        #region Eular ForFlip
        if (_move > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (_move < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        #endregion

    }
    void JumpFun()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            SoundManager.PlaySound("_jump");
        }
        //grounded(Player Feet)
        for(int i = 0; i < feetPos.Length; i++)
        {
            IsGrounded = Physics2D.OverlapCircle(feetPos[i].transform.position, circleRadius, layerGround);
        }

        if (IsGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("space0");
            anim.SetTrigger("LowJump");
            IsJumping = true;
            JumpTimeCounter = JumpTime;
            rb.velocity = Vector2.up * _JumpForce;  
        }
        if (Input.GetKey(KeyCode.Space) && IsJumping == true)
        {
            
            //Debug.Log("space1");
            anim.SetBool("IsJumping", true);
            if (JumpTimeCounter > 0)
            {
                
                rb.velocity = Vector2.up * _JumpForce;
                JumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                //Debug.Log("space1Exit");
                IsJumping = false;
                anim.SetBool("IsJumping", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("space2Exit");
            IsJumping = false;
            anim.SetBool("IsJumping", false);
        }
    }

    public void AttackFun()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.PlaySound("_punchkickVoice");
        }

        if (Input.GetKeyDown(KeyCode.F) && IsGrounded == true)
        {
            
            if (receiveInput)
            {
                receivedInput = true;
                receiveInput = false;
            }
            else
            {
                return;
            }
        }
    }

    public void InputManager()
    {
        if (!receiveInput)
        {
            receiveInput = true;
        }
        else
        {
            receiveInput = false;
        }
    }

    public void stickWeapon()
    {
        if(attack == true)
        {
            if (Input.GetButton("Fire1"))
            {
                anim.SetBool("StickAttack3", true);
                anim.SetBool("IsRunning", false);
                _speed = 240 / 6;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                anim.SetBool("StickAttack3", false);
                anim.SetBool("IsRunning", true);
                _speed = 240;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                anim.SetBool("Block", true);
                _speed = 240/ 8;
            }
            else if (Input.GetKeyUp(KeyCode.H))
            {
                anim.SetBool("Block", false);
                _speed = 240;
            }
        }
        
        else if(attack == false)
        {
            anim.SetBool("StickAttack3", false);
            anim.SetBool("Block", false);
        }
    }
    public float visionDistance;
    public GameObject dir;

    public void VisionRay()
    {
        RaycastHit2D RayColl = Physics2D.Raycast(dir.transform.position, dir.transform.right, visionDistance);
        if(RayColl.collider != null)
        {

            //AiEnemy.Instance.ani.SetBool("walk", false);
            //AiEnemy.Instance.ani.SetBool("sneak", true);
            //AiEnemy.Instance.speed = 0.1f;
            aienemyc.ani.SetBool("walk", false);
            aienemyc.ani.SetBool("sneak", true);
            aienemyc.speed = 0.1f;

            Debug.DrawLine(dir.transform.position, RayColl.point, Color.blue);
        }
        else
        {
            //AiEnemy.Instance.ani.SetBool("walk", true);
            //AiEnemy.Instance.ani.SetBool("sneak", false);
            //AiEnemy.Instance.speed = 1f;
            aienemyc.ani.SetBool("walk", true);
            aienemyc.ani.SetBool("sneak", false);
            aienemyc.speed = 0.1f;

            Debug.DrawLine(dir.transform.position, dir.transform.position + dir.transform.right * visionDistance, Color.white); 
        }
    }

    public Collider2D [] hitboxes;

    public float radius;

    private void OnDrawGizmosSelected()
    {

        for (int i = 0; i < hitboxes.Length; i++)
        {
            Collider2D[] _colls = Physics2D.OverlapCircleAll(hitboxes[i].bounds.center, radius, AttackMask);

            foreach(Collider2D colls in _colls)
            {
                if(colls.gameObject.tag == ("Enemy"))
                {

                    SoundManager.PlaySound("_punch");
                    aienemyc.EnemyDamageFun(1f);/*break;*/
                }
            }
            
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(hitboxes[i].bounds.center, radius);
        }
    }

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                