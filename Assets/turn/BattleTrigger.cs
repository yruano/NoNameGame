using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField]
    List<TurnMonster> monster = new List<TurnMonster>();
    [SerializeField]
    BattleController bc;

    private void Awake()
    {
        //������ �� �߰��� ���Ͱ� ������
        if( monster.Count == 0 ) 
            //�ϳ� ã�� ��
            monster.Add(gameObject.AddComponent<TurnMonster>());

        //��Ʋ ��Ʈ�ѷ� ����
        bc = GetComponent<BattleController>();
        //���͵� ���
        for(int i = 0; i < monster.Count; i++)
        {
            //0���� id����
            bc.BMonsters.Add(i, monster[i]);
        }
        //�� ����
        bc.turnStart();
    }
}
