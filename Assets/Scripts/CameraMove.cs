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
        player = GameObject.Find("Player"); // Player ������Ʈ�� ã�Ƽ� ��ü�� ����
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPos = player.transform.position;                  // �÷��̾ �������� ���� ������Ʈ�� ��ġ�� ���

        float scaleY = Mathf.Clamp(PlayerPos.y, minY, maxY);            // Y��ǥ ���� ����

        transform.position = new Vector3(PlayerPos.x, scaleY, transform.position.z);     // �÷��̾ �������� ������Ʈ�� y ���� ī�޶��� ��ǥ�� �ѱ�
    }
}
