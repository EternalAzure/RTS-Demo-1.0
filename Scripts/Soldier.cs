using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public event System.Action OnAttack;

    private IUnitController uc;
    [SerializeField]  private float hp;
    public bool alive = true;
    public Swordmen stats;
    [SerializeField] private string enemyTag;
    [SerializeField] private List<GameObject> enemiesInRange;
    [SerializeField] private float cooldown;

    private void Start()
    {
        cooldown = stats.cd;
        hp = stats.hp;

        enemiesInRange = new List<GameObject>();
        GetComponent<SphereCollider>().radius = stats.range;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) Attack();
    }

    public void Attack()
    {
        ClearList();
        if (enemiesInRange.Count <= 0) return;
        Soldier s = enemiesInRange[0].GetComponent<Soldier>();
        if (s.alive)
        {
            transform.LookAt(s.transform);
            OnAttack?.Invoke();
            cooldown = stats.cd;
            s.TakeDamage(stats.damage);
        }
    }
    private void ClearList()
    {
        List<GameObject> removed = new List<GameObject>();
        foreach (GameObject enemy in enemiesInRange)
        {
            if (enemy == null) removed.Add(enemy);
        }
        foreach (GameObject gameObject in removed)
        {
            enemiesInRange.Remove(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(enemyTag))
            enemiesInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(enemyTag))
        {
            if (enemiesInRange.Contains(other.gameObject))
                enemiesInRange.Remove(other.gameObject);
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.range);

    }*/
    public void TakeDamage(int damage)
    {
        int hitChance = Random.Range(1, 10);
        if (hitChance > stats.parry) hp -= damage;
        
        if (hp > 0) return;

        alive = false;
        uc.OnDeath(this);
        Destroy(this.gameObject);
    }
    public void SetUC(IUnitController uc)
    {
        this.uc = uc;
    }
    public UnitController GetUC()
    {
        return (UnitController) uc;
    }
}
