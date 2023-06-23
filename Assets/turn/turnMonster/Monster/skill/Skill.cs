using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//temporary card, contains card's information
//임시, 카드대용으로 쓸 카드의 공통적인 정보 담고 있음.  
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
