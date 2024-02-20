using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    [SerializeField] Transform My;
    [SerializeField] float speed;
    private bool isOk = true;
    private void Start()
    {
        My.position = transform.position;
    }
    private void Update()
    {
        if (My.position.x > 5)
        {
            isOk = false;
        }
        if (My.position.x < 0)
        {
            isOk = true;
        }
        if (My.position.x < 5 && isOk)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        else if (!isOk && My.position.x > 0)
        {
            transform.Translate(Vector3.right * -speed * Time.deltaTime, Space.World);
        }
    }


}
