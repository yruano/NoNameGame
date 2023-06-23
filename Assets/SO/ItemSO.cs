using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardItem
{
  public string name;
  public int attack;
  public int action;
  public Sprite sprite;
  public float percent;
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
  public CardItem[] carditems;
}
