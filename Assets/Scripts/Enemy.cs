using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public enum EnemyState
{
    idle,
    walk,
    attack,
    staggered
}


public class Enemy : MonoBehaviour
{
    public float health;
    public FloatValue maxHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public EnemyState currentState;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Knock(Rigidbody2D enemy, float knockTime,float dmg)
    {
        StartCoroutine(knockCO(enemy,knockTime));
        TakeDamage(dmg);

    }

    private IEnumerator knockCO(Rigidbody2D enemy,float knockTime)
    {
        if(enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;            
            enemy.GetComponent<Enemy>().currentState = EnemyState.walk;
        }
    }
}
