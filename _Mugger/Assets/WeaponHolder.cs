using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private AiEnemy aienemyw;

    public GameObject Holder;
    public Rigidbody2D rb;
    //public Collider2D coll;
    public Transform player, _Holder, weapon;

    public float pickRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equiped;
    public bool slotFull;

    private void Start()
    {
        if (!equiped)
        {
            rb.isKinematic = false;
            Holder.gameObject.SetActive(false);
        }
        if (equiped)
        {
            rb.isKinematic = true;
            Holder.gameObject.SetActive(true);
            slotFull = true;
        }
        aienemyw = GetComponent<AiEnemy>();
    }

    private void Update()
    {
        Vector2 distanceToPlayer = player.position - transform.position;
        if (!equiped && distanceToPlayer.magnitude <= pickRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickFun();
            Controller.Instance.stickWeapon();
            Controller.Instance.attack = true;
        }
        if (equiped && Input.GetKeyDown(KeyCode.Q))
        {
            DropFun();
            Controller.Instance.stickWeapon();
            Controller.Instance.attack = false;
        }


    }

    private void PickFun()
    {
        equiped = true;
        slotFull = true;

        transform.SetParent(_Holder);
        //transform.localPosition = _Holder.position;
        //transform.localRotation = Quaternion.Euler(Vector2.zero);

        transform.position = _Holder.position;
        transform.rotation = Quaternion.Euler(Vector2.zero);

        rb.isKinematic = true;
        Holder.gameObject.SetActive(true);
        
    }

    private void DropFun()
    {
        equiped = false;
        slotFull = false;

        transform.SetParent(null);
        

        rb.isKinematic = false;
        Holder.gameObject.SetActive(false);

        rb.velocity = player.GetComponent<Rigidbody2D>().velocity;

        rb.AddForce(player.forward * dropForwardForce, ForceMode2D.Impulse);
        rb.AddForce(player.up * dropUpwardForce, ForceMode2D.Impulse);

    }

    public Collider2D stickHitBox;
    public Vector2 OverlapSizeBox;
    public LayerMask AttackMask;

    private void OnDrawGizmosSelected()
    {
        Collider2D [] stickCol = Physics2D.OverlapBoxAll(stickHitBox.bounds.center, stickHitBox.bounds.extents, 0f, AttackMask);

        foreach (Collider2D ColStick in stickCol)
        {
            //Debug.Log(ColStick.name);
            if (ColStick.gameObject.tag == ("Enemy"))
            {
                aienemyw.EnemyDamageFun(1);
                break;
            }
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(stickHitBox.bounds.center, OverlapSizeBox);

    }
}
