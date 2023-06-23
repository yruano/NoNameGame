using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]

public class Item : ScriptableObject 
{
  public int Id;
  public string ItemName;
  public string ItemType;
  public int Value;
  public Sprite Icon;
}