using UnityEngine;

public class collision_and_color : MonoBehaviour
{
    [SerializeField] //inspector View에서 변수의 옵션을 조절할 수 있게 해준다
    private Color color;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    /// <summary>
    /// 충돌이 일어나는 순간 1회 호출
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.color = color;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " : OnCollisionStay2D() 메소드 실행");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        spriteRenderer.color = Color.white;
    }
}
