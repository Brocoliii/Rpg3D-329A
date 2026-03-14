using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject []ItemPrefabs   {  get { return itemPrefabs; } set { itemPrefabs = value; } }

    [SerializeField]
    private itemData[] itemData;
    public itemData[] ItemData { get { return itemData; }  set { itemData  = value; } }

    public const int  MAXSLOT = 16;

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
}

