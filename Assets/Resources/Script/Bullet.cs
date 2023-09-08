using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float bulletPower = 10;

    public enum BulletType
    {
        BulletA,
        BulletB,
    }
    public BulletType bulletType;

    // Start is called before the first frame update
    void Awake()
    {
        switch(bulletType)
        {
            case BulletType.BulletA:
                bulletPower = 15f;
                break; 
            case BulletType.BulletB:
                bulletPower = 30f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "wall_Bullet" || collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
