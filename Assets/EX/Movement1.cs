using UnityEngine;

public class Movement1 : MonoBehaviour
{
    private void Update()
    {
        //�Ҽӵ� ���ӿ�����Ʈ�� transform������Ʈ
        transform.position = transform.position + new Vector3(1, 0, 0) * 1 * Time.deltaTime ;
        //                                              ����              �ӵ�
        //transform.position += Vector3.right * 1; //�̰Ŷ� ���� ������
    }
}
