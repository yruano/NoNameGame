using UnityEngine;

public class Movement1 : MonoBehaviour
{
    private void Update()
    {
        //소속된 게임오브젝트의 transform컴포넌트
        transform.position = transform.position + new Vector3(1, 0, 0) * 1 * Time.deltaTime ;
        //                                              방향              속도
        //transform.position += Vector3.right * 1; //이거랑 같은 역할함
    }
}
