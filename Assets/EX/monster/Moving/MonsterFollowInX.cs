using UnityEngine;

//X축으로만 따라간다
//follow only Xaxis
public class MonsterFollowInX : MonoBehaviour
{
    //추적 목표
    [SerializeField]
    public GameObject MTarget;
    [SerializeField]
    public Monster monster;
    private Rigidbody2D rb;
    Vector3 dir;

    private void Start()
    {
        //추적목표 플레이어로 지정
        MTarget = GetComponent<MonsterTargetFinder>().Mtarget;
        monster = GetComponent<Monster>();
        rb = GetComponent<Rigidbody2D>();
        dir = new Vector3(1, 0, 0);
    }
    void Update()
    {
        //x축으로만 추적
        //여긴 position에 vector더해주는 구조
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


/*부끄부끄같이 움직이는 용도
//방향 설정
Vector3 direction = (MTarget.transform.position - transform.position).normalized;
//설정된 방향에 지정된 이동속도로 이동 (프레임 영향 X)
transform.position += direction * speed * Time.deltaTime;*/


//여긴 AddForce사용한 구조
//if (MTarget.transform.position.x > transform.position.x)
//{
//    rb.AddForce(dir * monster.MSpeed);
//}
//else
//{
//    rb.AddForce(-dir * monster.MSpeed);
//}

//여긴 velocity사용한 구조, 이게 적절한듯
//if (MTarget.transform.position.x > transform.position.x)
//{
//    rb.velocity = dir * monster.MSpeed;
//}
//else
//{
//    rb.velocity = (-dir * monster.MSpeed);
//}