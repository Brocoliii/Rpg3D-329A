using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField]
    private GameObject RingMarker;
    public GameObject Marker { get { return RingMarker; } }
    public static VFXManager Instance;

    void Start()
    {
        Instance = this;
    }

}
