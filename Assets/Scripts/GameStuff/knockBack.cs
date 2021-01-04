using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
     

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("break") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<pot>().quebra();
        }

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {

            
            Rigidbody2D body = other.GetComponent<Rigidbody2D>();
            if(body != null) {

                Vector2 diference = body.transform.position - transform.position;
                diference = diference.normalized * thrust;
                body.AddForce(diference, ForceMode2D.Impulse);

                if (body.gameObject.CompareTag("Enemy") && other.isTrigger){

                    body.GetComponent<Enemy>().currentState = EnemyState.staggered;                
                    other.GetComponent<Enemy>().Knock(body, knockTime, damage);
                }

                if (body.gameObject.CompareTag("Player")  )
                {
                    if(other.GetComponent<Player_Movement>().currentState != PlayerState.staggered) { 
                        body.GetComponent<Player_Movement>().currentState = PlayerState.staggered;
                        other.GetComponent<Player_Movement>().Knock(knockTime,damage);
                    }
                }
               
                
                


            }
        }
    }

}
