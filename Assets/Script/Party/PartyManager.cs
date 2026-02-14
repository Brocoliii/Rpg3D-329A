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
}
