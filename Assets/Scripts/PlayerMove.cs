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
        //rigid.freezeRotation = true;  // 회전 고정. 굴러다니는 문제는 해결됐으나 점프 기능이 작동하지 않는 문제점 발생

        rigid = GetComponent<Rigidbody2D>();
        //rigid.drag = 10f;       // 마찰력 지정. 굴러다니는 문제를 해결해주진 않음

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

        // 좌우 움직임을 먼저 인식하면 점프를 씹는 문제를 확인해서 점프를 먼저 인식할 수 있게 변경
        if (onFloor && Input.GetKeyDown(KeyCode.UpArrow))   // 화살표 윗키 누르면 점프
        {
            Debug.Log("Jump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        /*
         * 누르는 시간에 따라 점프 높이가 달라지는 코드. 근데 키에서 손을 떼기 전까지 점프를 하지 않는 문제점 때문에 사용 안하기로 함
        if (onFloor && Input.GetKey(KeyCode.UpArrow))   // 화살표 윗키
        {
            Debug.Log("UpArrow pushed");
            if (!isJumping)
            {
                isJumping = true;
                jumpHoldTime = 0;
            }
            // 윗키를 누른 시간 카운트
            jumpHoldTime += Time.deltaTime;

            // 누른 시간에 따라 점프력 증가
            currentJumpPower = Mathf.Min(jumpHoldTime * 8f, maxJumpPower); // 최대 점프 높이 제한
        }
        else if (onFloor && Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (isJumping)
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                isJumping = false;  // 점프 끝
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

    // OnCollisionEnter2D는 여러 개의 Floor가 동시에 닿았을 때에 onFloor를 제대로 카운트하지 못하는 문제가 있음
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
            collision.gameObject.SetActive(false);  // Coin 오브젝트 비활성화
        }   // Coin 인식을 못해서 충돌 말고 트리거로 변경
        
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

    // 위의 문제 해결을 위해 OnCollisionStay2D로 처리. 바닥에 닿아있는 상태일 때 onFloor를 true로 설정
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }
}