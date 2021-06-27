using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animPav : MonoBehaviour
{
    private Animator anim;
    float speed = 5f; //скорость перемещения
    public float move;
   // public bool ground = false;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start() //ссылаемся на анимацию и ригидбоди, отвечающий за просчет скорости перемещения
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() //подключаем джойстик
    {
       move = JoyStick.Horizontal();
        transform.Translate(transform.right * move * speed * Time.fixedDeltaTime);
        if (JoyStick.Vertical() > 0)
        {
            transform.Translate(transform.up * move * speed * Time.fixedDeltaTime);
        }
    }
}
