using Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyController : MonoBehaviour
{
    public Stats stats = new Stats();

    GameObject exPlosion;
    GameObject[] items = new GameObject[1];

    SpriteRenderer spriteRenderer;
    public Sprite[] sprite = new Sprite[2];

    public enum EnemyType
    {
        EnemyA,
        EnemyB,
        EnemyC,
    }
    public EnemyType enemyType;

    void Setup()
    {
        items[0] = Resources.Load<GameObject>("Prefabs/Power");
        exPlosion = Resources.Load<GameObject>("Prefabs/Explosion");
        switch (enemyType)
        {
            case EnemyType.EnemyA:
                stats.Damage = 25f;
                stats.HP = 50f;
                stats.Speed = 1f;
                break; 
            case EnemyType.EnemyB:
                stats.Damage = 30f;
                stats.HP = 75f;
                stats.Speed = 0.7f;
                break;
            case EnemyType.EnemyC:
                stats.Damage = 50f;
                stats.HP = 100f;
                stats.Speed = 0.3f;
                break;
        }
    }

    private void Awake()
    {
        Setup();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnHit(float dmg)
    {
        stats.HP -= dmg;
        StartCoroutine(HitSprite());
        if(stats.HP <= 0)
        {
            GameObject deadEx = Instantiate(exPlosion,transform.position,transform.rotation);
            Instantiate(items[0],transform.position,transform.rotation);
            Destroy(deadEx,0.5f);
            Destroy(gameObject);
        }
    }

    IEnumerator HitSprite()
    {
        spriteRenderer.sprite = sprite[1];
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = sprite[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.bulletPower);
        }
        if(collision.gameObject.name == "wall_Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
