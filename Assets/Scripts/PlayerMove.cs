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
    public float jumpPower;
    //public float maxJumpPower = 12f;
    //public float currentJumpPower;
    //public float jumpHoldTime;

    public bool onFloor = false;
    public bool onCoin = false;

    //public bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        update_count = 10;
        FixedUpdate_count = 40;
        speed = 5;
        jumpPower = 10;
        //rigid.freezeRotation = true;  // ȸ�� ����. �����ٴϴ� ������ �ذ������ ���� ����� �۵����� �ʴ� ������ �߻�

        rigid = GetComponent<Rigidbody2D>();
        //rigid.drag = 10f;       // ������ ����. �����ٴϴ� ������ �ذ������� ����

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
        //if (Input.GetKeyDown(KeyCode.W))    // [ GetKeyDown, GetKey, GetKeyUp ]    What behavior you do is trigger.
        //{
        //Debug.Log("Down");
        //}

        // �¿� �������� ���� �ν��ϸ� ������ �ô� ������ Ȯ���ؼ� ������ ���� �ν��� �� �ְ� ����
        if (onFloor && Input.GetKeyDown(KeyCode.UpArrow))   // ȭ��ǥ ��Ű ������ ����
        {
            Debug.Log("Jump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        /*
         * ������ �ð��� ���� ���� ���̰� �޶����� �ڵ�. �ٵ� Ű���� ���� ���� ������ ������ ���� �ʴ� ������ ������ ��� ���ϱ�� ��
        if (onFloor && Input.GetKey(KeyCode.UpArrow))   // ȭ��ǥ ��Ű
        {
            Debug.Log("UpArrow pushed");
            if (!isJumping)
            {
                isJumping = true;
                jumpHoldTime = 0;
            }
            // ��Ű�� ���� �ð� ī��Ʈ
            jumpHoldTime += Time.deltaTime;

            // ���� �ð��� ���� ������ ����
            currentJumpPower = Mathf.Min(jumpHoldTime * 8f, maxJumpPower); // �ִ� ���� ���� ����
        }
        else if (onFloor && Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (isJumping)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = false;  // ���� ��
                currentJumpPower = 0;
            }
        }
        */
        //Debug.Log("update");                              // write debug log per frame
        //transform.Translate(x, y, z);                     // add position
        //transform.Translate(Vector2.right);               // move position
        //transform.position = new Vector3(3, 5, 0);        // change position
        //transform.position += new Vector3(0.1f, 0, 0);    // add position
        
        h = Input.GetAxisRaw("Horizontal");                 // move horizontally 
    }

    // OnCollisionEnter2D�� ���� ���� Floor�� ���ÿ� ����� ���� onFloor�� ����� ī��Ʈ���� ���ϴ� ������ ����
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("onFloor");
            onFloor = true;
        }
        
        if (collision.gameObject.CompareTag("Coin"))
        {
            onCoin = true;
            Debug.Log("onCoin");
            collision.gameObject.SetActive(false);  // Coin ������Ʈ ��Ȱ��ȭ
        }   // Coin �ν��� ���ؼ� �浹 ���� Ʈ���ŷ� ����
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("onCoin");
            onCoin = true;
            other.gameObject.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            onCoin = false;
        }
    }

    // ���� ���� �ذ��� ���� OnCollisionStay2D�� ó��. �ٴڿ� ����ִ� ������ �� onFloor�� true�� ����
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }
}