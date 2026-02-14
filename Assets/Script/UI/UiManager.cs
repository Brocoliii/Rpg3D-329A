using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private Toggle togglePasueUnpause;
    public RectTransform SelectionBox { get { return selectionBox; } }
    public static UiManager instance;

    void Awake()
    {
        instance = this;
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            togglePasueUnpause.isOn = !togglePasueUnpause.isOn;
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
    public void SelectAll()
    {
        PartyManager.instance.SelectChars.Clear();
        foreach (Character member in PartyManager.instance.MemberChars)
        {
            if (member.HP > 0)
            {
                member.ToggleRingSelection(true);
                PartyManager.instance.SelectChars.Add(member); 
            }
        }
    }
    public void PasuseUnpause(bool isOn)
    {
        Time.timeScale = isOn ? 0 : 1;
    }
}
