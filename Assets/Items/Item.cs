using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 오브젝트 스크립트 (스탯 내용 바뀔 수 있다)
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject 
{
  public int Id;
  public string ItemName;
  public string ItemType;
  public int Value;
  public Sprite Icon;
}