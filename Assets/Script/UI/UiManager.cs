using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    public RectTransform SelectionBox { get { return selectionBox; } }
    public static UiManager instance;

    void Awake()
    {
        instance = this;
    }
    public void ToggleAI(bool isOn)
    {
        foreach (Character member in PartyManager.instance.MemberChars)
        {
            AttackAI ai = member.gameObject.GetComponent<AttackAI>();
            if (ai != null)
                ai.enabled = isOn;
        }
    }
}
