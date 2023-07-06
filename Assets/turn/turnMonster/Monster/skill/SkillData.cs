using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using GoogleSheetsToUnity;
using static UnityEngine.GraphicsBuffer;
using System.Linq;
//구글 시트에서 모든 데이터를 가져와 double배열로 갖고 저장합니다.

public class SkillData : MonoBehaviour { 
    [SerializeField]
    public string associatedSheet = "1mpe5Gjq7nO5HTYhLEZkTnNSrFAMFnCKSGZ0tyuJynds";
    public string associatedWorksheet = "test";
    public List<List<double>> Skills = new List<List<double>>();
    public Dictionary<string, int> NameToId = new Dictionary<string, int>();
    public Dictionary<int, string> IdToName = new Dictionary<int, string>();

    //공통 인덱스
    public const int ID = 0;
    public const int PRIORITY = 1;
    //공격스킬 인덱스
    public const int DAMAGE = 2;
    public const int HIT_COUNT = 3;
    //버프스킬 인덱스
    public const int ADD_VAL = 2;
    public const int MULTI_VAL = 3;
    public const int TARGET_STAT = 4;
    public const int DURATION = 5;
    //힐 스킬 인덱스
    public const int HEAL_AMOUNT = 2;

    //실제로 데이터 불러오는 부분
    private void Awake()
    {
        LoadDataCall(ProcessingData);
    }

    //API메소드 호출, 실행중엔 awake만 호출 해야함
    internal void LoadDataCall(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        Skills.Clear();
        //라이브러리 호출
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheet), callback, mergedCells);
    }
    //ss는 읽어온 모든 데이터를 갖고있는 구조체, 위에서 호출함
    internal void ProcessingData(GstuSpreadSheet ss)
    {
        //행교체 for문
        for(int rows = 2; rows <= ss.rows.primaryDictionary.Count; rows++)
        {
            //공통 데이터 읽어오기
            Skills.Add(new List<double>());
            //ID
            Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][0].value));
            //우선순위
            Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][1].value));
            //이름과 ID 연결
            NameToId[ss.rows[rows][2].value] = (int)Skills[Skills.Count - 1][0];
            IdToName[(int)Skills[Skills.Count - 1][0]] = ss.rows[rows][2].value;

            //공격 데이터를 읽는 부분
            if ((int)(Skills[Skills.Count - 1][0]) % 10 == 1)
            {
                //데미지
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][3].value));
                //타수
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][8].value));
            }
            //버프데이터를 읽는 부분
            else if ((int)(Skills[Skills.Count - 1][0]) % 10 == 2 || (int)(Skills[Skills.Count - 1][0]) % 10 == 3)
            {
                //덧셈 값
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][4].value));
                //곱셈 값
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][5].value));
                //목표 스텟
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][6].value));
                //지속 시간
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][7].value));
            }
            //힐 데이터를 읽는 부분
            else if ((int)(Skills[Skills.Count - 1][0]) % 10 == 4)
            {
                //힐량
                Skills[Skills.Count - 1].Add(int.Parse(ss.rows[rows][9].value));

            }
            //로그남기기, string.Join은 list내부 데이터를 한개의 string으로 묶는것
            Debug.Log("데이터 불러옴: " + string.Join(", ", Skills[Skills.Count - 1].Select(d => d.ToString()).ToArray()) + ": " + IdToName[(int)Skills[Skills.Count - 1][0]]);
        }

        Debug.Log("데이터 로딩 완료");
    }
}

//------
//inspector창 구성
[CustomEditor(typeof(SkillData))]
public class DataTest : Editor
{
    SkillData data;

    //초기화
    void OnEnable()
    {
        data = (SkillData)target;
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
