using UnityEngine;

public class Movement2D_wall : MonoBehaviour
{
    private float moveSpeed = 5.0f;   //이동속도
    private Rigidbody2D rigid2D;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");    //좌우
        float y = Input.GetAxisRaw("Vertical");     //상하

        //새로운 위치 = 현재 위치 ( 방향 * 속도)
        //transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;

        //Rigidbody2D 컴포넌트에 있는 속력(velocity) 변수 설정, 장애물뚫고 가려고 하지 않음

        rigid2D.velocity = new Vector3(x, y, 0) * moveSpeed;
    }
}