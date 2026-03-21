using System.Collections.Generic;
using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> monsters;
    public List<Enemy> Monsters
    { get { return monsters; } }

    public static EnemyManeger instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        foreach (Character m in monsters)
        {
            m.charInit(VFXManager.Instance, UiManager.instance, InventoryManager.Instance);
        }
        InventoryManager.Instance.Additem(monsters[0], 0);
        InventoryManager.Instance.Additem(monsters[0], 1); // ไอเทมแต่ละอย่างห
        InventoryManager.Instance.Additem(monsters[0], 2);
    }
}
