using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public class LeftClick : MonoBehaviour
{
    public static LeftClick instance;
    private Camera cam;
    //[SerializeField]
    //private Character curChar;
    //public Character CurChar { get { return curChar; } } ไม่ใช้แล้ว
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private RectTransform boxSelection;
    private Vector2 oldAnchoredPos;
    private Vector2 startPos;
    void Start()
    {
        instance = this;
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Ground", "Character", "Building", "Item");

        boxSelection = UiManager.instance.SelectionBox;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            ClearEverything();
        }   
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            UpdateSelectionBox(Input.mousePosition);
        }
        //mouse up
        if (Input .GetMouseButtonUp(0))
        {
            ReleaseSelectionBox(Input.mousePosition);
            TrySelect(Input.mousePosition);
        }
    }
    private void SelectCharacter (RaycastHit hit)
    {
        Character hero = hit.collider.GetComponent<Character>();
        Debug.Log("Selected Char" + hit.collider.gameObject);

        PartyManager.instance.SelectChars.Add(hero);
        hero.ToggleRingSelection(true);
        UiManager.instance.ShowMagicToggles();
    }
    private void TrySelect(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Player":
                case "Hero":
                    SelectCharacter(hit);
                    break;
            }
        }
    }

    private void ClearRingSelection()
    {
        foreach (Character h in PartyManager.instance.SelectChars) h.ToggleRingSelection(false);
    }
    private void ClearEverything()
    {
        ClearRingSelection();
        PartyManager.instance.SelectChars.Clear();
    }
   
    private void UpdateSelectionBox(Vector2 mousePos)
    {
        if (!boxSelection.gameObject.activeInHierarchy)
            boxSelection.gameObject.SetActive(true);
        float width = mousePos.x - startPos.x;
        float height = mousePos.y - startPos.y;

        boxSelection.anchoredPosition = startPos + new Vector2(width/2, height/2);

        width = Mathf.Abs(width);
        height = Mathf.Abs(height);

        boxSelection.sizeDelta = new Vector2 (width, height);
        oldAnchoredPos = boxSelection.anchoredPosition;
    }

    private void ReleaseSelectionBox (Vector2 mousePos)
    {
        Vector2 cornor1;
        Vector2 cornor2;

        boxSelection.gameObject.SetActive(false);

        cornor1 = oldAnchoredPos - (boxSelection.sizeDelta / 2);
        cornor2 = oldAnchoredPos + (boxSelection.sizeDelta / 2);

        foreach (Character member in PartyManager.instance.MemberChars)
        {
            Vector2 unitPos = cam.WorldToScreenPoint(member.transform.position);
                if((unitPos.x > cornor1.x && unitPos.x < cornor2.x)&&(unitPos.y > cornor1.y && unitPos.y < cornor2.y))
            {
                PartyManager.instance.SelectChars.Add(member);
                member.ToggleRingSelection(true);
            }
        }
        boxSelection.sizeDelta = new Vector2(0, 0);
    }
}
