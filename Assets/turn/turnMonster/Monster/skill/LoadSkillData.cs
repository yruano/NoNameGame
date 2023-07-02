using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using GoogleSheetsToUnity;
using static UnityEngine.GraphicsBuffer;
//구글 시트에서 모든 데이터를 가져와 double배열로 갖고 저장합니다.

[CreateAssetMenu(fileName = "LoadSkillData", menuName = "Scriptable Object/LoadSkillData_Test")]
public class LoadSkillData : ScriptableObject
{
    [SerializeField]
    public string associatedSheet = "1mpe5Gjq7nO5HTYhLEZkTnNSrFAMFnCKSGZ0tyuJynds";
    public string associatedWorksheet = "test";
    public List<string> Names = new List<string>();
    public List<double> Skills = new List<double>();
    public int skillCount = 0;
    //실제로 데이터 불러오는 부분

    //API메소드 호출
    public void LoadDataCall(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        //라이브러리 호출
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheet), callback, mergedCells);
    }
    //ss는 읽어온 모든 데이터를 갖고있는 구조체, 위에서 호출함
    public void ProcessingData(GstuSpreadSheet ss)
    {
        //
        for(int i = 0; i < skillCount; i++)
        {
            //spreadsheetRef. //가장 큰 문제발견, 조회를 string으로 해야한다 이거 해결봐야힘
        }
    }
    //-------------------------------------

}
//inspector창 구성
[CustomEditor(typeof(LoadSkillData))]
public class DataTest : Editor
{
    LoadSkillData data;

    //초기화
    void OnEnable()
    {
        data = (LoadSkillData)target;
    }

    //버튼 구성
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //버튼설명
        GUILayout.Label("Read Data Examples");

        //버튼 정의
        if (GUILayout.Button("Pull Data Method One"))
        {
            //버튼 클릭시 불러올애들
            data.LoadDataCall(data.ProcessingData);
        }
    }
}
