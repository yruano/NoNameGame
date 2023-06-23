using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshProExample : MonoBehaviour
{
    [SerializeField]
    public TMP_Text textMeshPro;  // TextMeshPro ��ü
    public InputField input;

    public void Awake()
    {
        textMeshPro = GetComponent<TMP_Text>();
        //inputField ����
        input = GameObject.Find("InputField").GetComponent<InputField>();
        //�̺�Ʈ ������ ���
        input.onEndEdit.AddListener(OnEndEdit);
    }
    public void OnEndEdit(string value)
    {
        // TextMeshPro ��ü�� �ؽ�Ʈ�� ������
        textMeshPro.text = value;
    }
}