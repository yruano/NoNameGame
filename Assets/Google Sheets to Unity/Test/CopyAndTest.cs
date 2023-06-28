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

    public List<string> items = new List<string>();

    public List<string> Names = new List<string>();

    //아래에서 긁어온 데이터중 name에 해당하는 행을 가져와 열단위로 분리해서 저장.
    internal void UpdateStats(List<GSTU_Cell> list, string name)
    {
        //초기화
        items.Clear();
        //저장할 변수 선언
        int type = 0, ID = 0, damage = 0;
        //UpdateMethodOne에서 행단위로 분리해둔 데이터list중 name에 해당하는 행을 열단위로 분리해서 변수에 저장
        //구조를 세워야한다. name으로 찾을건지, id로 찾을건지를 정해야한다
        for (int i = 0; i < list.Count; i++)
        {
            //열 이름을 읽어 각각 맞는 변수에 저장
            switch (list[i].columnId)
            {
                case "type":
                    {
                        type = int.Parse(list[i].value);
                        break;
                    }
                case "ID":
                    {
                        ID = int.Parse(list[i].value);
                        break;
                    }
                case "damage":
                    {
                        damage = int.Parse(list[i].value);
                        break;
                    }
            }
        }
        //데이터가 갈 곳으로 이동
        Debug.Log($"{name}의 데이터 타입:{type} 아이디:{ID} 데미지:{damage}");
    }

}

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
        //inspertorc창에서 
        foreach (string dataName in data.Names)
            data.UpdateStats(ss.rows[dataName], dataName);
        EditorUtility.SetDirty(target);
    }

}