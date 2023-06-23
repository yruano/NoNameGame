using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpForce = 8.0f;
    private Rigidbody2D rigid2D;

    //���� ������ ����
    [HideInInspector] //�����
    public bool isLongJump = false;

    //�������� ������ ����
    [SerializeField]
    private LayerMask groundLayer;  //�ٴ� üũ�� ���� �浹 ���̾�
    private CircleCollider2D CircleCollider2D;
    [SerializeField]
    private bool isGrounded;
    private Vector3 footPosition;

    //���� ������ ����
    [SerializeField]
    private int maxJumpCount = 2; //�ִ� ���� Ƚ��
    [SerializeField]
    private int currentJumpCount = 0;//���� ���� ���� Ƚ��

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        //���� ���������� �߰�
        CircleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        //���� ���� ������ �߰�
        //������Ʈ collider ��ġ ���� ������
        Bounds bounds = CircleCollider2D.bounds;
        //������Ʈ�� �� ��ġ
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        //������Ʈ �� ��ġ�� 0.1f������ �浹 ���� �����ϰ�, groundLayer�� ���� ��������� isGrounded = true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        //���� Ƚ���������� �߰�
        //����Ƚ�� �ʱ�ȭ
        if(isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        //���� ������ �߰�
        if(isLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = 1.0f;
        }
        else
        {
            rigid2D.gravityScale = 2.5f;
        }
    }
    
    //���� ���� ������ �߰�, �浹������ ���� �׷���
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    public void Move(float x)
    {
        //x�� �̵��� x*speed��, y�� �̵��� ������ �ӷ� ��
        rigid2D.velocity = new Vector2 (x * speed, rigid2D.velocity.y);
        
    }
    public void Jump()
    {
        //y���� �ӵ��� jumpForce��ŭ ���� �ö�
        //���� ���� ������ if��
        //if(isGrounded)
        //���� Ƚ�� üũ�� if��
        if (currentJumpCount > 0)
        {
            rigid2D.velocity = Vector2.up * jumpForce;
            currentJumpCount--;
        }
    }
}
