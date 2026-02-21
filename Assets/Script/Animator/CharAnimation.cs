using UnityEngine;

public class CharAnimation : MonoBehaviour
{
    private Character character;
    

     void Start()
    {
        character = GetComponent<Character>();
    }

     void Update()
    {
        ChooseAnimator(character);
    }

    private void ChooseAnimator(Character c)
    {
        c.Anim.SetBool("isIdle", false);
        
        c.Anim.SetBool("isWalk", false);

        switch (c.State)
        {
            case CharState.Idle:
                c.Anim.SetBool("isIdle", true);
                break;
            case CharState.Walk:
            case CharState.WalkToEnemy:
  case CharState.WalkToMagicCast:
                c.Anim.SetBool("isWalk", true);
                break;
                

        }
    }
}
