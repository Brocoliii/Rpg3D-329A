using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject []ItemPrefabs   {  get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private itemData[] itemData;
    public itemData[] ItemData { get { return itemData; }  set { itemData  = value; } }

    public const int  MAXSLOT = 17;

    public static InventoryManager Instance;

     void Awake()
    {
        Instance = this;
    }

    public bool Additem(Character character , int id)
    {
        Item item = new Item(itemData[id]);
        for (int i = 0; i < character.InventoryItens.Length; i++)
            if (character.InventoryItens[i] == null)
            {
                character.InventoryItens[i] = item;
                return true;
                   
            }
        Debug.Log("Inventory Full");
        return false;
    }
    public void SaveItemInBag (int index , Item item)
    {
        if (PartyManager.instance.SelectChars.Count == 0) return;
        PartyManager.instance.SelectChars[0].InventoryItens[index] = item;
        switch(index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].EquipShield(item);
                break;
        }

    }
    public void RemoveItemInBag (int index)
    {
        if (PartyManager.instance.SelectChars.Count == 0) return;
        PartyManager.instance.SelectChars[0].InventoryItens[index] = null;

        switch (index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].UnEquipShield();
                break;
        }
    }
    private void SpawnDropItem (Item item , Vector3 pos)
    {
        int id;
        switch(item.Type)
        {
            case ItemType.Consumable: id= 1; break;
                default: id= 0; break;
        }
        GameObject itemObj = Instantiate(ItemPrefabs[id], pos, Quaternion.identity);
        itemObj.AddComponent<ItemPick>();

        ItemPick itemPick = itemObj.GetComponent<ItemPick>();
        itemPick.Init(item, Instance, PartyManager.instance);
    }    
    public void SpawnDropInventory(Item[] items, Vector3 pos)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null) SpawnDropItem(items[i], pos);
        }
    }

    public void DrinkConsumableItem(Item item , int slotId)
    {
        string s = string.Format("Drank : {0}", item.ItemName);
        Debug.Log(s);
        if (PartyManager.instance.SelectChars.Count > 0)
        {
            PartyManager.instance.SelectChars[0].Recover(item.Power);
            RemoveItemInBag(slotId);
                
        }
    }

}

