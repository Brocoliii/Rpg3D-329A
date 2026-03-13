using UnityEngine;
using System.Collections.Generic;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private List<Character> selectChars = new List<Character>();
    public List<Character> SelectChars  { get{ return selectChars; }}
    [SerializeField]
    private List<Character> memberChars = new List<Character>();
    public List<Character> MemberChars { get { return memberChars; } }

    public static PartyManager instance;

    void Awake()
    {
        instance = this;
    }

     void Start()
    {
        foreach (Character character in memberChars)
        {
            character.charInit(VFXManager.Instance , UiManager.instance);
           character.MagicSkills.Add(new Magic(0, "Fire ball", 10f, 50, 3f, 1f, 0, 1));
           character.MagicSkills.Add(new Magic(0, "Lighing", 10f, 50, 3f, 1f, 0, 1));
        }
        SelectSingHero(0);
        memberChars[0].MagicSkills.Add(new Magic(0, "power Glow", 10f, 50, 3f, 1f, 2, 2));
        memberChars[1].MagicSkills.Add(new Magic(0, "Fire ball", 10f, 50, 3f, 1f, 2, 2));
        memberChars[2].MagicSkills.Add(new Magic(0, "Lighing", 10f, 50, 3f, 1f, 2, 2));
        UiManager.instance.ShowMagicToggles();
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (selectChars.Count > 0)
            {
                selectChars[0].IsMagicMode = true;
                selectChars[0].CurMagicCast = selectChars[0].MagicSkills[0];
            }
        }   
    }
    public void SelectSingHero (int i)
    {
        foreach (Character character in selectChars) character.ToggleRingSelection(false);
        selectChars.Clear();
        selectChars.Add(memberChars[i]);
        selectChars[0].ToggleRingSelection(true);
    }
    public void HeroSelectMagicSkill(int i)
    {
        if (selectChars.Count <= 0) return;
        selectChars[0].IsMagicMode = true;
        selectChars[0].CurMagicCast = selectChars[0].MagicSkills[i];
    }
}
