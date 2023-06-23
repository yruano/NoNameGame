using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshProExample : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textMeshPro;  // TextMeshPro 객체
    public InputField input;

    public void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
        //inputField 연결
        input = GameObject.Find("InputField").GetComponent<InputField>();
        //이벤트 리스너 등록
        input.onEndEdit.AddListener(OnEndEdit);
    }
    public void OnEndEdit(string value)
    {
        // TextMeshPro 객체의 텍스트를 변경합
        textMeshPro.text = value;
    }
}