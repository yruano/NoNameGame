using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CopyAndTest", menuName = "Scriptable Object/CopyAndTest")]
public class CopyAndTest : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    //public List<string> items = new List<string>();

    public List<string> Names = new List<string>();

    //아래에서 긁어온 데이터중 name에 해당하는 행을 가져와 열단위로 분리해서 저장.
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        //초기화
        //items.Clear();
        //list는 한 행의 데이터를 가지고있음, [i]는 셀 하나의 정보를 가짐.
        //현재 구조에선 B열을 먼저 읽음, (list[i].column[0] - 'A') == i
        //type을 기준으로 데이터를 찾음, 나중에 순서가 확정되면 순차접근 말고 임의접근으로 바꿀 것

        //공격 데이터를 읽는 중 이라면 
        if (list[1].value == "1")
        {
            //데이터 저장할 변수 선언
            int type = -1, ID = -1, damage = -1, priority = -1;
            //각 셀을 순차 접근하며 필요한 데이터를 챙겨옴
            for (int i = 0; i < list.Count; i++)
            {
                //열 이름을 읽어 각각 맞는 변수에 저장
                switch (list[i].columnId)
                {
                    case "type":
                        type = int.Parse(list[i].value);
                        break;
                    case "ID":
                        ID = int.Parse(list[i].value);
                        break;
                    case "priority":
                        priority = int.Parse(list[i].value); 
                        break;
                    case "damage":
                        damage = int.Parse(list[i].value);
                        break;
                }
                //모든 데이터를 읽어왔다면
                if (type != -1 && ID != -1 && damage != -1)
                    //순차접근 종료
                    break;
            }
            //데이터가 갈 곳으로 이동
            Debug.Log($"{name}의 데이터 타입:{type} 아이디:{ID} 우선순위:{priority} 데미지:{damage}");
        }
        //버프 데이터를 읽는중 이라면
        else if (list[1].value == "2" || list[1].value == "3")
        {
            //데이터 저장할 변수 선언
            int type = -1, ID = -1, priority = -1, AddVal = -1, MultiVal = 0, TargetStat = -1, Duration = -1;

            for (int i = 0; i < list.Count; i++)
            {
                //열 이름을 읽어 각각 맞는 변수에 저장
                switch (list[i].columnId)
                {
                    case "type":
                        type = int.Parse(list[i].value);
                        break;
                    case "ID":
                        ID = int.Parse(list[i].value);
                        break;
                    case "priority":
                        priority = int.Parse(list[i].value);
                        break;
                    case "AddVal":
                        AddVal = int.Parse(list[i].value);
                        break;
                    case "MultiVal":
                        MultiVal = int.Parse(list[i].value);
                        break;
                    case "TargetStat":
                        TargetStat = int.Parse(list[i].value);
                        break;
                    case "Duration":
                        Duration = int.Parse(list[i].value);
                        break;
                }
                //모든 데이터를 읽어왔다면
                if (type != -1 && ID != -1 && AddVal != -1 && MultiVal != 0 && TargetStat != -1 && Duration != -1)
                    //순차접근 종료
                    break;
            }
            //데이터가 갈 곳으로 이동
            Debug.Log($"{name}의 데이터 타입:{type} 아이디:{ID} 우선순위:{priority} 덧셈 값:{AddVal} 곱셈 값:{MultiVal} 목표 스텟 ID : {TargetStat} 지속 시간: {Duration}");
        }
        //힐 데이터를 읽는 중이라면
        else if (list[1].value == "4")
        {
            //데이터 저장할 변수 선언
            int type = -1, ID = -1, priority = -1, HealAmount = -1;
            //각 셀을 순차 접근하며 필요한 데이터를 챙겨옴
            for (int i = 0; i < list.Count; i++)
            {
                //열 이름을 읽어 각각 맞는 변수에 저장
                switch (list[i].columnId)
                {
                    case "type":
                            type = int.Parse(list[i].value);
                            break;
                    case "ID":
                            ID = int.Parse(list[i].value);
                            break;
                    case "priority":
                        priority = int.Parse(list[i].value);
                        break;
                    case "HealAmount":
                            HealAmount = int.Parse(list[i].value);
                            break;
                }
                //모든 데이터를 읽어왔다면
                if (type != -1 && ID != -1 && HealAmount != -1)
                    //순차접근 종료
                    break;
            }
            //데이터가 갈 곳으로 이동
            Debug.Log($"{name}의 데이터 타입:{type} 아이디:{ID} 우선순위:{priority} 치료양:{HealAmount}");
        }
    }
}

//-------------------------------------

//inspector창 구성
[CustomEditor(typeof(CopyAndTest))]
public class TestEditor : Editor
{
    CopyAndTest data;

    void OnEnable()
    {
        data = (CopyAndTest)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //버튼설명
        GUILayout.Label("Read Data Examples");

        //버튼 정의
        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    //버튼 이벤트 발생시 호출될 함수
    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        //라이브러리 호출
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    //저 멀리서 읽어온 데이터를 행(name) 단위로 끊음
    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        //저기 멀리서 가져온 데이터(data)에서 name을 기준으로 행을 하나 하나 나눠서 UpdateStats호출(행 단위로 쪼개서 데이터 처리)
        foreach (string dataName in data.Names)
            data.UpdateStats(ss.rows[dataName], dataName);
        //.. 개체나 에셋을 수정한 후 해당 변경 사항을 저장하기 위해 호출됩니다. ...
        EditorUtility.SetDirty(target);
    }

}