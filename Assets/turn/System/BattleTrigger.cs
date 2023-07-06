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
    [SerializeField]
    SkillData skillData;

    private void Awake()
    {
        //편집기 상에 추가한 몬스터가 없으면
        if( monster.Count == 0 ) 
            //하나 찾아 옴
            monster.Add(gameObject.AddComponent<TurnMonster>());

        //배틀 컨트롤러 연결
        bc = GetComponent<BattleController>();
        //스킬 데이터 담당 컴포넌트 연결
        skillData = GetComponent<SkillData>();
        //몬스터에 스킬 등록
        skillData.LoadDataCall(skillData.ProcessingData);
        monster[0].Skills.Add(skillData.Skills[2]);
        monster[0].Skills.Add(skillData.Skills[4]);
        //몬스터들 등록
        for (int i = 0; i < monster.Count; i++)
        {
            //0부터 id연결
            bc.BMonsters.Add(i, monster[i]);
        }
        //턴 시작
        bc.turnStart();
    }
}
