using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapmove : MonoBehaviour
{
    Vector3 nowPos, movePos;
    float moveSpeed = 1f;
    float mapMinRange = -11f;
    float mapMaxRange = 24f;

    public enum Speed {
        Grass,
        Dirt,
        Hill
    }
    public Speed speedType = Speed.Dirt;

    // Start is called before the first frame update
    void Start()
    {
        nowPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < mapMinRange)
        {
            transform.position += Vector3.up * mapMaxRange;
        }
        

        nowPos = transform.position;
        if ( speedType == Speed.Dirt)
        {
            moveSpeed = 1;
        }
        else if( speedType == Speed.Hill)
        {
            moveSpeed = 1.5f;
        }
        else if(speedType == Speed.Grass)
        {
            moveSpeed = 2f;
        }
        movePos = Vector3.down * moveSpeed * Time.deltaTime;
        transform.position = nowPos + movePos;
    }
}
