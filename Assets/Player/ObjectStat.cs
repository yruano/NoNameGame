using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Object/Create New Object")]
public class ObjectStat : ScriptableObject
{
  public int Hp;
  public int Damage;
  public int Defense;
  public float Critical;
  public float CriticalDamage;
  public float Evasion;
  public float Speed;
  public float Crossroads;
  public float HpRegen;
  public float ManaRegen;
  public float Vampirism;
  public int Money;
}