using UnityEngine;
using System.Collections;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using GoogleSheetsToUnity.ThirdPary;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class TestData : ScriptableObject
{
    public string associatedSheet = "";
    public string associatedWorksheet = "";

    public List<string> items = new List<string>();
    
    public List<string> Names = new List<string>();

    //긁어온 행 데이터 조회
    //실질적으로 데이터 가져오는 곳
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        //초기화
        items.Clear();
        //저장할 변수 선언
        int math=0, korean=0, english=0;
        //UpdateMethodOne에서 행단위로 분리해둔 데이터list중 name에 해당하는 행을 열단위로 분리해서 변수에 저장
        for (int i = 0; i < list.Count; i++)
        {
            //열 이름을 읽어 각각 맞는 변수에 저장
            switch (list[i].columnId)
            {
                case "Math":
                {
                    math = int.Parse(list[i].value);
                    break;
                }
                case "Korean":
                {
                    korean = int.Parse(list[i].value);
                    break;
                }
                case "English":
                {
                    english = int.Parse(list[i].value);
                    break;
                }
            }
        }
        //데이터가 갈 곳으로 이동
        Debug.Log($"{name}의 점수 수학:{math} 국어:{korean} 영어:{english}");
    }

}

[CustomEditor(typeof(TestData))]
public class DataEditor : Editor
{
    TestData data;

    void OnEnable()
    {
        data = (TestData)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("Read Data Examples");

        if (GUILayout.Button("Pull Data Method One"))
        {
            UpdateStats(UpdateMethodOne);
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    //행 단위로 끊기
    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        //data.UpdateStats(ss.rows["Jim"]);
        foreach (string dataName in data.Names)
            data.UpdateStats(ss.rows[dataName], dataName);
        EditorUtility.SetDirty(target);
    }
    
}