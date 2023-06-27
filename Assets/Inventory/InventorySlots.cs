using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 인벤토리 슬롯에 정보를 넣는 스크립트
public class InventorySlots : MonoBehaviour
{
  public static InventorySlots Instance;
  public GameObject Inventory;
  public Item PlayerWeapon = null;

  // 아이템 리스트
  public List<Item> IngredientItems = new List<Item>();

  // 장비아이템 리스트
  public List<Item> EquipmentItems = new List<Item>();

  // 현재 장착하고있는 방어구 리스트
  public Dictionary<string, Item> Equipment = new Dictionary<string, Item>();
  public Transform ItemContent;
  public GameObject InventoryItem;
  public ObjectStat PStat;
  public GameObject Player;

  // 기본적인 초기화
  private void Awake()
  {
    Instance = this;
    Equipment.Add("Head", null);
    Equipment.Add("Top", null);
    Equipment.Add("Pants", null);
    Equipment.Add("Shoes", null);
  }

  // 아이템리스트에 아이템 추가
  public void IngredientAdd(Item item)
  {
    IngredientItems.Add(item);
    IListItems();
  }

  // 아이템리스트에 아이템 삭제
  public void IngredientRemove(Item item)
  {
    IngredientItems.Remove(item);
    IListItems();
  }

  // 장비리스트에 장비 추가
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

  // 장비리스트에 장비 삭제
  public void EquipmentRemove(Item item)
  {
    EquipmentItems.Remove(item);
    IListItems();
  }

  // 인벤토리에 들어가는 아이템 
  public void IListItems()
  {
    // 열기전 아이템 정리
    foreach (Transform item in ItemContent) { Destroy(item.gameObject); }

    // 인벤토리에 장비아이템 넣음
    for (int i = 0; i < EquipmentItems.Count; i++)
    {
      GameObject obj = Instantiate(InventoryItem, ItemContent);
      obj.AddComponent<Button>();
      Button button = obj.GetComponent<Button>();

      // 장비의 타입에 따라서 버튼 이벤트를 넣어준다
      // 방어구를 클릭하면 EquipmentSwaps() 함수가 발동한다
      // 무기를 클릭하면 WeaponSwaps()가 발생한다
      if (EquipmentItems[i].ItemType != "Weapon") { button.onClick.AddListener(() => EquipmentSwaps()); }
      else { button.onClick.AddListener(() => WeaponSwaps()); }

      var ItemName = obj.transform.Find("ItemName").GetComponent<Text>();
      var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

      obj.name = EquipmentItems[i].Id.ToString();
      obj.tag = EquipmentItems[i].ItemType;
      ItemName.text = EquipmentItems[i].ItemName;
      ItemIcon.sprite = EquipmentItems[i].Icon;
    }

    // 인벤토리에 아이템을 넣는다
    for (int i = 0; i < IngredientItems.Count; i++)
    {
      // 아이템 타입이 돈이라면 지갑에 돈을 추가한다 아닐경우 그냥 인벤토리에 아이템 추가
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

  // 장착하고 있는 무기를 인벤토리의 무기와 스왑한다
  public void WeaponSwaps()
  {
    GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
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

  // 장착하고 있는 방어구를 인벤토리의 방어구와 스왑한다
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