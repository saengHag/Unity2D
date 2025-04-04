using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    GameObject player;
    public float minY = 0.9f;
    public float maxY = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // Player 오브젝트를 찾아서 객체에 지정
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPos = player.transform.position;                  // 플레이어가 조종중인 게임 오브젝트의 위치를 계산

        float scaleY = Mathf.Clamp(PlayerPos.y, minY, maxY);            // Y좌표 범위 제한

        transform.position = new Vector3(PlayerPos.x, scaleY, transform.position.z);     // 플레이어가 조종중인 오브젝트의 y 값만 카메라의 좌표에 넘김
    }
}
