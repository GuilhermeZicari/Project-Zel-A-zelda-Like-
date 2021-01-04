using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyElite : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePos;
    public Animator anim;
    public Rigidbody2D myRigid;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody2D>();
        currentState = EnemyState.idle;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkDistance();   
        
    }

    public virtual void checkDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) >= attackRadius && (currentState == EnemyState.walk || currentState == EnemyState.idle))
        {

            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigid.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("walk", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {

            anim.SetBool("walk", false);
            ChangeState(EnemyState.idle);

        }
        void changeAnim(Vector2 dir)
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0)
                {
                    SetAnimFloat(Vector2.right);
                }
                else if (dir.x < 0)
                {
                    SetAnimFloat(Vector2.left);
                }

            }
            else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
            {
                if (dir.y > 0)
                {
                    SetAnimFloat(Vector2.up);
                }
                else if (dir.y < 0)
                {
                    SetAnimFloat(Vector2.down);
                }

            }

        }
        void SetAnimFloat(Vector2 setter)
        {
            anim.SetFloat("MoveX", setter.x);
            anim.SetFloat("MoveY", setter.y);
        }
        void ChangeState(EnemyState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
            }
        }

    }
}
