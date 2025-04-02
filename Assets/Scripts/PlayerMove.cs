using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
    public int update_count;
    public int FixedUpdate_count;
    public float speed;
    public Rigidbody2D rigid;
    public float h;
    public float jump_power;

    // Start is called before the first frame update
    void Start()
    {
        update_count = 10;
        FixedUpdate_count = 40;
        speed = 5;
        
        rigid = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //Debug.Log("FixedUpdate");
        //rigid.AddForce(new Vector2(1, 0));                    //add speed by adding force
        //rigid.AddForce(transform.right, ForceMode2D.Force);   // ForceMode2D + [ Force, Impulse, Acceleration(for 3D), VelocityChange(for 3D) ]
        //rigid.AddForce(transform.right, ForceMode2D.Impulse);
        rigid.velocity = new Vector2(h * speed, rigid.velocity.y);            // define speed. use rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))    // [ GetKeyDown, GetKey, GetKeyUp ]    What behavior you do is trigger.
        {
            //Debug.Log("Down");
        }
        //Debug.Log("update");                              // write debug log per frame
        //transform.Translate(x, y, z);                     // add position
        //transform.Translate(Vector2.right);               // move position
        //transform.position = new Vector3(3, 5, 0);        // change position
        //transform.position += new Vector3(0.1f, 0, 0);    // add position
        h = Input.GetAxisRaw("Horizontal");                    // move horizontally 

        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        }
    }
    void OncollisionEnter2D(Collision2D collision)
    {
        
    }
}