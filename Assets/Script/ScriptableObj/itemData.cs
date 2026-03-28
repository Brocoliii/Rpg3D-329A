using UnityEngine;

[CreateAssetMenu(fileName = "itemData", menuName = "Scriptable Objects/itemData")]
public class itemData : ScriptableObject
{
    public int id;
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public int power;
    public int prefabID;
}
