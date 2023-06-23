using UnityEngine;

//X�����θ� ���󰣴�
//follow only Xaxis
public class MonsterFollowInX : MonoBehaviour
{
    //���� ��ǥ
    [SerializeField]
    public GameObject MTarget;
    [SerializeField]
    public Monster monster;
    private Rigidbody2D rb;
    Vector3 dir;

    private void Start()
    {
        //������ǥ �÷��̾�� ����
        MTarget = GetComponent<MonsterTargetFinder>().Mtarget;
        monster = GetComponent<Monster>();
        rb = GetComponent<Rigidbody2D>();
        dir = new Vector3(1, 0, 0);
    }
    void Update()
    {
        //x�����θ� ����
        //���� position�� vector�����ִ� ����
        if (MTarget.transform.position.x > transform.position.x)
        {
            transform.position += new Vector3(monster.MSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            transform.position -= new Vector3(monster.MSpeed * Time.deltaTime, 0f, 0f);
        }
    }
}


/*�β��β����� �����̴� �뵵
//���� ����
Vector3 direction = (MTarget.transform.position - transform.position).normalized;
//������ ���⿡ ������ �̵��ӵ��� �̵� (������ ���� X)
transform.position += direction * speed * Time.deltaTime;*/


//���� AddForce����� ����
//if (MTarget.transform.position.x > transform.position.x)
//{
//    rb.AddForce(dir * monster.MSpeed);
//}
//else
//{
//    rb.AddForce(-dir * monster.MSpeed);
//}

//���� velocity����� ����, �̰� �����ѵ�
//if (MTarget.transform.position.x > transform.position.x)
//{
//    rb.velocity = dir * monster.MSpeed;
//}
//else
//{
//    rb.velocity = (-dir * monster.MSpeed);
//}