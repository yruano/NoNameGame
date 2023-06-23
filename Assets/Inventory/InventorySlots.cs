using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
  public static InventorySlots Instance;
  public GameObject Inventory;
  public Item PlayerWeapon = null;
  public List<Item> IngredientItems = new List<Item>();
  public List<Item> EquipmentItems = new List<Item>();
  public Dictionary<string, Item> Equipment = new Dictionary<string, Item>();
  public Transform ItemContent;
  public GameObject InventoryItem;
  public ObjectStat PStat;
  public GameObject Player;
  private void Awake() 
  { 
    Instance = this;
    Equipment.Add("Head", null);
    Equipment.Add("Top", null);
    Equipment.Add("Pants", null);
    Equipment.Add("Shoes", null);
  }
  public void IngredientAdd(Item item)
  {
    IngredientItems.Add(item);
    IListItems();
  }
  public void IngredientRemove(Item item)
  {
    IngredientItems.Remove(item);
    IListItems();
  }
  public bool EquipmentAdd(Item item)
  {
    bool check = false;
    if (PlayerWeapon == null && item.ItemType == "Weapon") 
    {
      PlayerWeapon = item;
      check = true; 
    }
    else { EquipmentItems.Add(item); }
    IListItems();
    return check;
  }
  public void EquipmentRemove(Item item)
  {
    EquipmentItems.Remove(item);
    IListItems();
  }

  public void IListItems()
  {
    // Clean content before open
    foreach (Transform item in ItemContent) { Destroy(item.gameObject); }

    for (int i = 0; i < EquipmentItems.Count; i++)
    {
      GameObject obj = Instantiate(InventoryItem, ItemContent);
      obj.AddComponent<Button>();
      Button button = obj.GetComponent<Button>();

      if (EquipmentItems[i].ItemType != "Weapon") { button.onClick.AddListener(() => EquipmentSwaps()); }
      else { button.onClick.AddListener(() => WeaponSwaps()); }

      var ItemName = obj.transform.Find("ItemName").GetComponent<Text>();
      var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

      obj.name = EquipmentItems[i].Id.ToString();
      obj.tag = EquipmentItems[i].ItemType;
      ItemName.text = EquipmentItems[i].ItemName;
      ItemIcon.sprite = EquipmentItems[i].Icon;
    }

    for (int i = 0; i < IngredientItems.Count; i++)
    {
      if (IngredientItems[i].ItemName == "Coin")
      {
        PStat.Money += IngredientItems[i].Value;
        Inventory.transform.Find("Wallet").GetComponent<Text>().text = PStat.Money.ToString();
        IngredientRemove(IngredientItems[i]);
        i--;
      }
      else
      {
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var ItemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

        obj.name = IngredientItems[i].Id.ToString();
        obj.tag = IngredientItems[i].ItemType;
        ItemName.text = IngredientItems[i].ItemName;
        ItemIcon.sprite = IngredientItems[i].Icon;
      }
    }
  }
  public void WeaponSwaps()
  {
    GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    // clickedButton
    for (int i = 0; i < EquipmentItems.Count; i++)
    {
      if (EquipmentItems[i].Id.ToString() == clickedButton.name)
      {
        var Temp = PlayerWeapon;
        PlayerWeapon = EquipmentItems[i];
        EquipmentItems[i] = Temp;

        var PWeapon = Player.gameObject.transform.Find("sword_man").gameObject.transform.Find("body").gameObject.transform.Find("Weapon").gameObject.transform.GetChild(0);
        PWeapon.GetComponent<SpriteRenderer>().sprite = PlayerWeapon.Icon;

        break;
      }
    }

    IListItems();
  }

  public void EquipmentSwaps()
  {
    GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
    for (int i = 0; i < EquipmentItems.Count; i++)
    {
      if (EquipmentItems[i].Id.ToString() == clickedButton.name)
      {
        List<Item> Temp = new List<Item>();
        Temp.Add(EquipmentItems[i]);

        var equipment = Inventory.gameObject.transform.Find(clickedButton.tag);

        if (Equipment[clickedButton.tag] == null)
        {
          equipment.transform.Find("EquipmentName").GetComponent<Text>().text = EquipmentItems[i].ItemName;
          equipment.transform.Find("EquipmentIcon").GetComponent<Image>().sprite = EquipmentItems[i].Icon;
          Equipment[clickedButton.tag] = EquipmentItems[i];
          EquipmentRemove(EquipmentItems[i]);
        }
        else
        {
          var tempItem = Equipment[clickedButton.tag];

          equipment.transform.Find("EquipmentName").GetComponent<Text>().text = EquipmentItems[i].ItemName;
          equipment.transform.Find("EquipmentIcon").GetComponent<Image>().sprite = EquipmentItems[i].Icon;

          Equipment[clickedButton.tag] = EquipmentItems[i];
          EquipmentItems[i] = tempItem;
        }

        break;
      }
    }


    IListItems();
  }

  private void Update()
  {
    if (!Inventory.activeSelf && Input.GetKeyDown(KeyCode.Tab))
    {
      Inventory.SetActive(true);
    }
    else if (Inventory.activeSelf && Input.GetKeyDown(KeyCode.Tab))
    {
      Inventory.SetActive(false);
    }
  }
}