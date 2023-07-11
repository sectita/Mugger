using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health Instance;

    public Text healthText;
    public Image HealthIcon;

    //[Range(0f, 100f)]
    float health;
    public float maxhealth;
    private float lerpspeed;

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health" + health + "%";
        healthText.text = health.ToString();

        if (health > maxhealth)
            health = maxhealth;
        lerpspeed = 3f * Time.deltaTime;

        HealthFun();
        ColorFun();
    }

    void HealthFun()
    {
        HealthIcon.fillAmount = Mathf.Lerp(HealthIcon.fillAmount, health / maxhealth, lerpspeed);
    }

    void ColorFun()
    {
        Color healthIconColor = Color.Lerp(Color.red, Color.white, (health / maxhealth));
    }

    public void DamageFun(float damagenumber)
    {
        health -= damagenumber;
        if(health <= 0)
        {
            SoundManager.PlaySound("_swardHurt");
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
