using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    [SerializeField] private Toggle togglePasueUnpause;
    [SerializeField]
    private Toggle[] toggleMagic;
    public Toggle[] ToggleMagic { get { return toggleMagic; } }
    [SerializeField]
    private int curToggleMagicID = -1;
    public RectTransform SelectionBox { get { return selectionBox; } }

    [SerializeField]
    private GameObject blackImage;
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject itemUIprefab;
    [SerializeField]
    private GameObject[] slots;

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
    public void ShowMagicToggles()
    {
        if (PartyManager.instance.SelectChars.Count <= 0) return;
        Character hero = PartyManager.instance.SelectChars[0];
        for (int i = 0; i< hero.MagicSkills.Count; i++)
        {
            toggleMagic[i].interactable = true;
            toggleMagic[i].isOn = false;
            toggleMagic[i].GetComponentInChildren<Text>().text = hero.MagicSkills[i].Name;
            toggleMagic[i].targetGraphic.GetComponent<Image>().sprite = hero.MagicSkills[i].Icon;
        }
    }
    public void SelectMagicSkill(int i)
    {
        curToggleMagicID = i;
        PartyManager.instance.HeroSelectMagicSkill(i);
    }
    public void IsOncurToggleMagic(bool flag)
    {
        toggleMagic[curToggleMagicID].isOn = flag;
    }    

    public void ClearInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].transform.childCount > 0)
            {
                Transform child = slots[i].transform.GetChild(0);
                Destroy(child.gameObject);
            }
        
        }  
            
    }
    public void ShowInventory()
    {
        if (PartyManager.instance.SelectChars.Count <= 0) return;

        Character hero = PartyManager.instance.SelectChars[0];

        for (int i = 0; i < hero.InventoryItens.Length; i++ )
        {
            if (hero.InventoryItens[i] != null)
         {
                GameObject itemObj = Instantiate(itemUIprefab, slots[i].transform);
                itemObj.GetComponent<Image>().sprite = hero.InventoryItens [i].Icon;

        }
        }    
    }
    public void ToggleInventoryPanel()
    {
        if (!inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(true);
            blackImage.SetActive(true);
            ShowInventory();
        }
        else
        {
            inventoryPanel.SetActive(false);
            blackImage.SetActive(false);    
            ClearInventory();
        }
    }
}
