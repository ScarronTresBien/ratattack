using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howTo : MonoBehaviour
{

    private GameObject HOWto;
    public bool howtoOPEN = false;
    // Start is called before the first frame update
    void Start()
    {
        HOWto = GameObject.Find("HOWTO"); //находим игровой объект с именем HOWTO
        HOWto.SetActive(false); //изначально делаем невидимым
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
