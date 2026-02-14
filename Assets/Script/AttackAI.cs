using UnityEngine;

public class AttackAI : MonoBehaviour
{
    private Character myChar;
    [SerializeField] private Character curEnemy;
    private void FindAndAttackEnemt()
    {
        if(myChar.CurCharTarget == null)
        {
            curEnemy = Formula.FindClosestEnemyChar(myChar);
            if (curEnemy == null) return;
            if (myChar.IsMyEnemt(curEnemy.gameObject.tag))
                myChar.ToAttackCharacter(curEnemy);
        }

        
    }
     void Start()
    {
        myChar = GetComponent<Character>();
        if (myChar != null) InvokeRepeating("FindAndAttackEnemt", 0f, 1f);
    }
   
}
