using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class control : MonoBehaviour
{
    public float maxSpeed;//最远端最大速度(可在脚本调整)
    public float minSpeed;//中间时最小速度(可在脚本调整)
    public float waitside;//两边等待时间(可在脚本调整)
    public float waitmiddle;//中间等待时间(可在脚本调整)
    private float waitside1;//两边等待时间变化值
    private float waitmiddle1;//中间等待时间变化值
    private Rigidbody rb;
    public float maxDisplacement;//运动最远距离(可在脚本调整)
    private int direction = 1;//方向

    //获取刚体组件
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }


    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    
    //运动代码
    public void Move()
    {
        float x = transform.position.x;//实时位置
        
        //速度为最小速度+(最大速度-最小速度)*当前位置/最远位置
        float movespeed = Mathf.Abs(minSpeed) + (Mathf.Abs(maxSpeed) - Mathf.Abs(minSpeed)) * Mathf.Abs(x) / Mathf.Abs(maxDisplacement) ;
        rb.velocity = new Vector3(direction * movespeed, 0, 0);
        
        //进行判断，到达两边和中间时并进行停止等待，到达两边时进行转向
        if (x < maxDisplacement && x > 0.1 || x > -maxDisplacement && x < -0.1)
        {
            waitside1 = waitside;
            waitmiddle1 = waitmiddle;
        }
        if (x >= maxDisplacement)
        {

            if (waitside1 > 0)
            {
                waitside1 -= Time.deltaTime;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else
                direction = -1;
        }

        if (x <= -maxDisplacement)
        {
            if (waitside1 > 0)
            {
                waitside1 -= Time.deltaTime;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else
                direction = 1;
        }
        if (x>=-0.1&&x<=0.1)
        {
            if (waitmiddle1 > 0)
            {
                waitmiddle1 -= Time.deltaTime;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }


         
    }
}
