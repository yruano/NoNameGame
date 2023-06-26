using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 아이템 획득
public class ItemPickup : MonoBehaviour
{
  public Item Item;

  // 무기가 아닌 아이템을 얻으면 아이템리스트에 들어간다 그리고 화면의 아이템을 삭제
  void IngredientPickup()
  {
    InventorySlots.Instance.IngredientAdd(Item);
    Destroy(gameObject);
  }

  // 무기종류의 아이템을 얻으면 무기리스트에 들어간다 그리고 화면의 무기를 삭제
  void EquipmentPickup(Collider2D other)
  {
    bool mounting = InventorySlots.Instance.EquipmentAdd(Item);

    // 만약 플레이어가 아무런 무기를 들고있지 않은면 자동적으로 무기를 든다
    if (mounting)
    {
      var PWeapon = other.gameObject.transform.Find("sword_man").gameObject.transform.Find("body").gameObject.transform.Find("Weapon").gameObject.transform.GetChild(0);
      PWeapon.name = gameObject.name;
      PWeapon.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    Destroy(gameObject);
  }

  // 플레이어와 충돌했는지 확인한다
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      if (Item.ItemType != "Item") { EquipmentPickup(other); }
      else { IngredientPickup(); }
    }
  }
}