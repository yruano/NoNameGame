using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public Item Item;
  void IngredientPickup()
  {
    InventorySlots.Instance.IngredientAdd(Item);
    Destroy(gameObject);
  }

  void EquipmentPickup(Collider2D other)
  {
    bool mounting = InventorySlots.Instance.EquipmentAdd(Item);
    if (mounting)
    {
      var PWeapon = other.gameObject.transform.Find("sword_man").gameObject.transform.Find("body").gameObject.transform.Find("Weapon").gameObject.transform.GetChild(0);
      PWeapon.name = gameObject.name;
      PWeapon.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    Destroy(gameObject);
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.name == "Player")
    {
      if (Item.ItemType != "Item") { EquipmentPickup(other); }
      else { IngredientPickup(); }
    }
  }
}