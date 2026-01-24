using UnityEngine;

public class TestScenes : MonoBehaviour
{
    [SerializeField] private Character[] characters;
    public void SetIdle()  
    {
        for(int i = 0; i < characters.Length; i++)
        {
            characters[i].SetState(CharState.Idle);
        }
    }
    public void setWalk()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetState(CharState.Walk);
        }
    }

    public void setAttack()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetState(CharState.Attack);
            characters[i].Anim.SetTrigger("Attack");
        }
    }

    public void SetDie ()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetState (CharState.Die);
            characters[i].Anim.SetTrigger("Die");
        }
    }
}
