using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//temporary card, contains card's information
//�ӽ�, ī�������� �� ī���� �������� ���� ��� ����.  
abstract public class Skill : MonoBehaviour
{
    public const int TYPE = 0;
    public const int ID = 1;
    public const int PRIORITY = 2;
    public int Type;
    public int Id;
    public int priority = 3;
    public virtual List<double> SFunction()
    {
        return new List<double>() { Type, Id, priority };
    }
}
