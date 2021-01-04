using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSkelly : Skelly
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;
    private float temp1 = 7;

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("walk", true);        
    }
    public override void checkDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) >= attackRadius && (currentState == EnemyState.walk || currentState == EnemyState.idle))
        {

            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigid.MovePosition(temp);
            //ChangeState(EnemyState.walk);
            anim.SetBool("walk", true);
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigid.MovePosition(temp);
            }
            else
            {
                changeGoal();
            }


        }

    }
    private void changeGoal()
    {
        if (currentPoint == path.Length - 1)
        {

            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
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

}
