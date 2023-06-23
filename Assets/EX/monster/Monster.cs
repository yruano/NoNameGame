using UnityEngine;

public class Monster : MonoBehaviour
{
    public float MHP = 100.0f;
    public float MDamage = 1.0f;
    public float MArmor = 0.0f;
    public float MSpeed = 1.0f;
    public float MRange = 1.0f;
    public float MCritChance = 0.2f;
    public float MCritDamage = 2.0f;
    public float MAttackRate = 1f;

    //�ǰݸ޼ҵ�, �����ڰ� ȣ���ϴ°� ��ǥ�� �����
    public void MFhited(float damage) {
        MHP -= damage - MArmor;
    }
}
