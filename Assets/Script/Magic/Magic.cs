using UnityEngine;

[System.Serializable]
public class Magic
{
    [SerializeField]
    private int id;
    public int ID {get {return id ;} }
    [SerializeField]
    private string name;
    public string Name {get {return name ;} }
    [SerializeField]
    private float range;
    public float Range { get { return range; } }
    [SerializeField]
    private int power;
    public int Power { get { return power; } }
    [SerializeField]
    private float loadTime;
    public float LoadTime { get { return loadTime; } }
    [SerializeField]
    private float shootTime;
    public float ShootTime { get { return shootTime; } }

    [SerializeField]
    private int loadID;
    public int LoadID { get { return loadID; } }
    [SerializeField]
    private int shootID;
    public int ShootID { get { return shootID; } }
    [SerializeField]
    private Sprite icon;
    public Sprite Icon { get { return icon; } }

    public Magic (MagicData data)
    {
        id = data.id ;
        icon = data.icon ;
        range = data.range ;
        name = data.name ;
        power = data.power;
        loadTime = data.loadTime ;
        shootTime = data.shootTime ;
        loadID = data.loadId;
        shootID = data.shootId;
    }
}
