using UnityEngine;

public class Movement2D_wall : MonoBehaviour
{
    private float moveSpeed = 5.0f;   //�̵��ӵ�
    private Rigidbody2D rigid2D;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");    //�¿�
        float y = Input.GetAxisRaw("Vertical");     //����

        //���ο� ��ġ = ���� ��ġ ( ���� * �ӵ�)
        //transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;

        //Rigidbody2D ������Ʈ�� �ִ� �ӷ�(velocity) ���� ����, ��ֹ��հ� ������ ���� ����

        rigid2D.velocity = new Vector3(x, y, 0) * moveSpeed;
    }
}