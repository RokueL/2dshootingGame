using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;
using System;

public class PlayerControll : MonoBehaviour
{
    Stats stats = new Stats();
    Inven inven = new Inven();

    Animator animator;

    GameObject BulletA, BulletB;
    public GameObject firePos;

    private bool Touch_Right = false;
    private bool Touch_Left = false;
    private bool Touch_Top = false;
    private bool Touch_Bottom = false;
    bool fireReady = true;

    Vector3 curPos;
    Vector3 inputPos;

    float bulletSpeed = 10f;
    float fireDelay = 0.5f;
    float h, v;

    void Setup()
    {
        stats.HP = 100f;
        stats.Speed = 3f;
        stats.Power = 1f;
        inven.powerUp = 0f;
        inven.bombAmount = 1f;
    }

    private void Awake()
    {
        Setup();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        BulletA = Resources.Load<GameObject>("Prefabs/BulletA");
        BulletB = Resources.Load<GameObject>("Prefabs/BulletB");

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    #region CHARACTER
    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if((Touch_Right && h == 1) || (Touch_Left && h == -1))
            h = 0;
        if ((Touch_Top&& v == 1) || (Touch_Bottom && v == -1))
            v = 0;

        curPos = transform.position;
        inputPos = new Vector3(h, v, 0) * stats.Speed * Time.deltaTime;

        transform.position = curPos + inputPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            animator.SetInteger("horizon", (int)h);
        }
    }

    void Fire()
    {
        if(fireReady == true)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                switch (stats.Power + inven.powerUp)
                {
                    case 1:
                        GameObject bullet = Instantiate(BulletA, firePos.transform.position, firePos.transform.rotation);
                        Rigidbody2D rb2 = bullet.GetComponent<Rigidbody2D>();
                        rb2.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                        break;
                    case 2:
                        GameObject bulletL = Instantiate(BulletA, firePos.transform.position+Vector3.left * 0.1f, firePos.transform.rotation);
                        GameObject bulletR = Instantiate(BulletA, firePos.transform.position + Vector3.right * 0.1f, firePos.transform.rotation);
                        Rigidbody2D rb2L = bulletL.GetComponent<Rigidbody2D>();
                        Rigidbody2D rb2R = bulletR.GetComponent<Rigidbody2D>();
                        rb2L.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                        rb2R.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                        break;
                    case 3:
                        GameObject bulletLL = Instantiate(BulletA, firePos.transform.position + Vector3.left * 0.2f, firePos.transform.rotation);
                        GameObject bulletRR = Instantiate(BulletA, firePos.transform.position + Vector3.right * 0.2f, firePos.transform.rotation);
                        GameObject bulletM = Instantiate(BulletB, firePos.transform.position, firePos.transform.rotation);
                        Rigidbody2D rb2M = bulletM.GetComponent<Rigidbody2D>();
                        Rigidbody2D rb2LL = bulletLL.GetComponent<Rigidbody2D>();
                        Rigidbody2D rb2RR = bulletRR.GetComponent<Rigidbody2D>();
                        rb2M.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                        rb2LL.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                        rb2RR.AddForce(Vector2.up * bulletSpeed, ForceMode2D.Impulse);
                        break;
                }
                StartCoroutine(FireDelay());
            }
        }
    }

    IEnumerator FireDelay()
    {
        fireReady = false;
        yield return new WaitForSeconds(fireDelay);
        fireReady = true;
    }
    #endregion

    #region TRIGGER
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            switch(collision.gameObject.name) 
            {
                case "wall_Top":
                    Touch_Top = true;
                    break;
                case "wall_Bottom":
                    Touch_Bottom = true;
                    break;
                case "wall_Right":
                    Touch_Right = true;
                    break;
                case "wall_Left":
                    Touch_Left = true;
                    break;
            }
        }

        if(collision.gameObject.tag == "Item")
        {
            var item = collision.gameObject.GetComponent<Items>();
            string s = item.itemType.ToString();
            switch (s)
            {
                case "Bomb":
                    inven.bombAmount++;
                    break;
                case "Coin":
                    break;
                case "Power":
                    if (stats.Power + inven.powerUp < 3)
                    {
                        inven.powerUp++;
                        //Destroy(collision.gameObject);
                    }
                    break;
                case "Life":
                    break;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            switch (collision.gameObject.name)
            {
                case "wall_Top":
                    Touch_Top = false;
                    break;
                case "wall_Bottom":
                    Touch_Bottom = false;
                    break;
                case "wall_Right":
                    Touch_Right = false;
                    break;
                case "wall_Left":
                    Touch_Left = false;
                    break;
            }
        }
    }

    #endregion
}
