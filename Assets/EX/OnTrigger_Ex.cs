using UnityEngine;

public class OnTrigger_Ex : MonoBehaviour
{
    [SerializeField]
    private GameObject moveObject;
    [SerializeField]
    private Vector3 moveDirection;
    private float moveSpeed;

    private void Awake()
    {
        moveSpeed = 5.0f;
    }

    /// <summary>
    /// 충돌 발생시 1회호출
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //moveObject 오브젝트의 색깔 검은색으로
        moveObject.GetComponent<SpriteRenderer>().color = Color.black;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //moveObject 오브젝트를 moveDirection방향으로 moveSpeed속도로 이동 + 로딩에 따른 시간차이 없도록
        moveObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        moveObject.GetComponent<SpriteRenderer>().color = Color.white;

        moveObject.transform.position = new Vector3(0, 4, 0);
    }
}
