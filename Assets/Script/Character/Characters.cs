using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public enum CharState
{
    Idle, Walk, Attack, Hit, Die, WalkToEnemy , WalkToMagicCast, MagicCast
}


public abstract class Character : MonoBehaviour
{
    protected NavMeshAgent navAgent;

    protected Animator anim;
    public Animator Anim { get { return anim; } }

    [SerializeField]
    protected CharState state;
    public CharState State { get { return state; } }
    [Header("Character Setting")]
    [SerializeField]
    protected int curHP = 10;
    public int HP { get { return curHP; } }
    [SerializeField]
    protected Character curCharTarget;
    public Character CurCharTarget { get { return curCharTarget; } set { curCharTarget = value; } }
    [SerializeField]
    protected float attackRange = 2f;
    public float AttackRange { get { return attackRange; } }
    [SerializeField]
    protected float attackCoolDown = 2f;
    [SerializeField]
    protected float attackTimer = 0f;
    [SerializeField]
    protected int atkDamage = 3;
    [SerializeField]
    protected float findingRange = 20f;
    //[SerializeField]
    //protected Vector3 MagicPoint;
    public float FindingRange { get { return findingRange; } }
    [SerializeField]
    protected List<Magic> magicSkills = new List<Magic>();
    public List<Magic> MagicSkills { get { return magicSkills; } set { magicSkills = value; } }
    [SerializeField]
    protected Magic curMagicCast = null;
    public Magic CurMagicCast { get { return curMagicCast; } set { curMagicCast = value; } }
    [SerializeField]
    protected bool isMagicMode = false;
    public bool IsMagicMode { get { return isMagicMode; } set { isMagicMode = value; } }

    [Header ("Inventory")]
    [SerializeField]
    protected Item[] inventoryitems;
    public Item[] InventoryItens { get { return inventoryitems; } set { inventoryitems = value; } }

    [SerializeField]
    protected Item mainWeapon;
    public Item MainWeapon { get { return mainWeapon; } set { mainWeapon = value; } }

    [SerializeField]
    protected Item shield;
    public Item Shield {get { return shield; }  set { shield = value; } }




    protected VFXManager vfxManager;
    protected UiManager uiManager;
    protected InventoryManager invManager;


    [SerializeField]
    protected GameObject ringSelection;
    public GameObject RingSelection { get { return ringSelection; } }

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {

    }
    void Update()
    {
        
    }
    public void SetState(CharState s)
    {
        state = s;
        if (state == CharState.Idle)
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
        }
    }
    public void WalkToPosition(Vector3 dest)
    {
        if (navAgent != null)
        {
            navAgent.SetDestination(dest);
            navAgent.isStopped = false;
        }
        SetState(CharState.Walk);
    }
    public void ToAttackCharacter(Character target)
    {
        if (curHP <= 0 || state == CharState.Die) return;
        curCharTarget = target;
        navAgent.SetDestination(target.transform.position);
        navAgent.isStopped = false;
        if (isMagicMode) SetState(CharState.WalkToMagicCast);
        else SetState(CharState.WalkToEnemy);



    }

    protected void WalkToEnemyUpdate()
    {
        if (curCharTarget == null)
        {
            SetState(CharState.Idle);
            return;
        }
        navAgent.SetDestination(curCharTarget.transform.position);
        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);
        if (distance <= attackRange)
        {
            SetState(CharState.Attack);
            Attack();
        }
    }
    public void WalkUpdate()
    {
        float distance = Vector3.Distance(transform.position, navAgent.destination);
        Debug.Log(distance);
        if (distance <= navAgent.stoppingDistance)
        {
            SetState(CharState.Idle);
        }
    }
    public void ToggleRingSelection(bool flag)
    {
        ringSelection.SetActive(flag);
    }
    protected void Attack()
    {
        transform.LookAt(curCharTarget.transform);

        anim.SetTrigger("Attack");

        AttackLogic();
    }
    protected void AttackUpdate()
    {
        if (curCharTarget == null) return;
        if (curCharTarget.curHP <= 0)
        {
            SetState(CharState.Idle);
            return;
        }
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            attackTimer = 0f;
            Attack();
        }
        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);
        if (distance > attackRange)
        {
            SetState(CharState.WalkToEnemy);
            navAgent.SetDestination(curCharTarget.transform.position);
            navAgent.isStopped = false;
        }

    }
    protected virtual IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    protected virtual void Die()
    {
        navAgent.isStopped = true;
        SetState(CharState.Die);
        anim.SetTrigger("Die");
        invManager.SpawnDropInventory(inventoryitems, transform.position);
        StartCoroutine(DestroyObj());
    }
    public void ReceiveDamage(int damage)
    {
        if (curHP <= 0 || state == CharState.Die) return;

        curHP -= damage;
        if (curHP <= 0)
        {
            curHP = 0;
            Die();
        }
    }
    protected void AttackLogic()
    {
        Character target = curCharTarget.GetComponent<Character>();
        if (target != null) target.ReceiveDamage(atkDamage);

    }
    protected void MagicCastLogic(Magic magic)
    {
        Character target = curCharTarget.GetComponent<Character>();
        if (target != null)
            target.ReceiveDamage(magic.Power);
    }
    private IEnumerator ShootMagicCast(Magic curMagicCast)
    {
        
        if (vfxManager != null) vfxManager.ShootMagic(curMagicCast.ShootID, transform.position, curCharTarget.transform.position + new Vector3 (0,1.2f,0), curMagicCast.ShootTime);
        yield return new WaitForSeconds(curMagicCast.ShootTime);
        MagicCastLogic(curMagicCast);
        isMagicMode = false;
        SetState(CharState.Idle);
        if (uiManager != null) uiManager.IsOncurToggleMagic(false);
    }
    private IEnumerator LoadMagicCast(Magic curMagicCast)
    {
        if (vfxManager != null) vfxManager.LoadMagic(curMagicCast.LoadID, transform.position, curMagicCast.LoadTime);
        yield return new WaitForSeconds(curMagicCast.LoadTime);
        StartCoroutine(ShootMagicCast(curMagicCast));


    }

    public bool IsMyEnemt(string targetTag)
    {
        string myTag = gameObject.tag;
        if ((myTag == "Hero" || myTag == "Player") && targetTag == "Enemy")
            return true;
        if ((myTag == "Enemy" && ( targetTag == "Hero") || targetTag == "Player"))
            return true;
        return false;
    }
    public void charInit(VFXManager vfxm , UiManager uiM, InventoryManager invM)
    {
        vfxManager = vfxm;
        uiManager = uiM;
        invManager = invM;

        inventoryitems = new Item[InventoryManager.MAXSLOT];
    }
    private void MagicCast(Magic curMagicCast)
    {
        transform.LookAt(curCharTarget.transform);
        anim.SetTrigger("MagicAttack");
        StartCoroutine(LoadMagicCast(curMagicCast));
    }
    protected void WalkToMagicCastUpdate()
    {
        if (curCharTarget == null || curMagicCast == null)
        {
            SetState(CharState.Idle);
            return;
        }
        navAgent.SetDestination(curCharTarget.transform.position);

        float disrance = Vector3.Distance(transform.position, curCharTarget.transform.position);

        if (disrance <= curMagicCast.Range)
        {
            navAgent.isStopped = true;
            SetState(CharState.MagicCast);

            MagicCast(curMagicCast);
        }

    }
}
