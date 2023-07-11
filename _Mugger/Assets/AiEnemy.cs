using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//namespace aienemyNamespace
//{
    public class AiEnemy : MonoBehaviour
    {
        //public static AiEnemy Instance;
        public float speed;
        public Transform playerTarget, currentTransform;
        public float UnitDiastance;
        public Animator ani;


        //private void Awake()
        //{
        //    Instance = this;
        //}

        private void FixedUpdate()
        {
            AiMove();
        }

        public void AiMove()
        {
            //move
            if (Vector2.Distance(currentTransform.position, playerTarget.position) > UnitDiastance)
            {
                ani.SetBool("enemykick", false);
                currentTransform.position = Vector2.MoveTowards(currentTransform.position, playerTarget.position, speed * Time.fixedDeltaTime);
            }
            //attack
            else if (Vector2.Distance(currentTransform.position, playerTarget.position) < UnitDiastance)
            {
                ani.SetBool("enemykick", true);
            }
            //flip
            if (currentTransform.position.x < playerTarget.position.x)
            {
                currentTransform.localScale = new Vector3(-0.04489056f, 0.04489056f, 0.04489056f);
            }
            else if (currentTransform.position.x > playerTarget.position.x)
            {
                currentTransform.localScale = new Vector3(0.04489056f, 0.04489056f, 0.04489056f);
            }
        }

        public Collider2D swardWeaponHit;
        public LayerMask swardLayerMask;
        public Vector2 swardBoxSize;

        private void OnDrawGizmosSelected()
        {
            Collider2D[] _enemySward = Physics2D.OverlapBoxAll(swardWeaponHit.bounds.center, swardWeaponHit.bounds.extents, 0f, swardLayerMask);

            foreach (Collider2D enemySward in _enemySward)
            {

                if (enemySward.gameObject.tag == ("Player"))
                {
                    SoundManager.PlaySound("_swardHurt");
                    Health.Instance.DamageFun(1f);
                }
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(swardWeaponHit.bounds.center, swardBoxSize);

        }

        public Text EnemyHealthText;
        public Image EnemyHealthIcon;

        /*[Range(1f, 100f)] */
        float Ehealth;
        public float Emaxhealth;
        float ELerpSpeed;

        private void Start()
        {
            Ehealth = Emaxhealth;
        }

        private void Update()
        {
            EnemyHealthText.text = "HEalth" + Ehealth + "%";
            EnemyHealthText.text = Ehealth.ToString();

            if (Ehealth > Emaxhealth)
                Ehealth = Emaxhealth;
            ELerpSpeed = 2f * Time.deltaTime;

            EnemyHEalthFun();

        }

        void EnemyHEalthFun()
        {
            EnemyHealthIcon.fillAmount = Mathf.Lerp(EnemyHealthIcon.fillAmount, Ehealth / Emaxhealth, ELerpSpeed);
        }

        public void EnemyDamageFun(float EnemyDamageNumber)
        {
            Ehealth -= EnemyDamageNumber;
            if (Ehealth <= 0)
            {
                this.gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }

    }
//}
