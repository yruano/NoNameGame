using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpForce = 8.0f;
    private Rigidbody2D rigid2D;

    //높은 점프용 변수
    [HideInInspector] //숨기기
    public bool isLongJump = false;

    //공중점프 방지용 변수
    [SerializeField]
    private LayerMask groundLayer;  //바닥 체크를 위한 충돌 레이어
    private CircleCollider2D CircleCollider2D;
    [SerializeField]
    private bool isGrounded;
    private Vector3 footPosition;

    //다중 점프용 변수
    [SerializeField]
    private int maxJumpCount = 2; //최대 점프 횟수
    [SerializeField]
    private int currentJumpCount = 0;//현재 남은 점프 횟수

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        //공중 점프방지용 추가
        CircleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        //공중 점프 방지용 추가
        //오브젝트 collider 위치 정보 얻어오기
        Bounds bounds = CircleCollider2D.bounds;
        //오브젝트의 발 위치
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        //오브젝트 발 위치에 0.1f사이즈 충돌 원을 생성하고, groundLayer에 원이 닿아있으면 isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        //점프 횟수여러번용 추가
        //점프횟수 초기화
        if(isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        //높은 점프용 추가
        if(isLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = 1.0f;
        }
        else
        {
            rigid2D.gravityScale = 2.5f;
        }
    }
    
    //공중 점프 방지용 추가, 충돌범위에 원을 그려줌
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    public void Move(float x)
    {
        //x축 이동은 x*speed로, y축 이동은 기존의 속력 값
        rigid2D.velocity = new Vector2 (x * speed, rigid2D.velocity.y);
        
    }
    public void Jump()
    {
        //y축의 속도를 jumpForce만큼 위로 올라감
        //공중 점프 방지용 if문
        //if(isGrounded)
        //점프 횟수 체크용 if문
        if (currentJumpCount > 0)
        {
            rigid2D.velocity = Vector2.up * jumpForce;
            currentJumpCount--;
        }
    }
}
