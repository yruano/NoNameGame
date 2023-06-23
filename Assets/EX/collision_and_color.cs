using UnityEngine;

public class collision_and_color : MonoBehaviour
{
    [SerializeField] //inspector View���� ������ �ɼ��� ������ �� �ְ� ���ش�
    private Color color;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    /// <summary>
    /// �浹�� �Ͼ�� ���� 1ȸ ȣ��
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        spriteRenderer.color = color;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " : OnCollisionStay2D() �޼ҵ� ����");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        spriteRenderer.color = Color.white;
    }
}
