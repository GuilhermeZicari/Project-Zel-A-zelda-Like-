using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerState
{
    walk,
    idle,
    attack,
    interact,
    staggered
}
public class Player_Movement : MonoBehaviour
{
    public float speed ;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue initialPosition;
    
    
   

    // Start is called before the first frame update
    void Start(){
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Movex", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = initialPosition.initialValue;
        
    }

    // Update is called once per frame
    void Update(){
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetKey("space") && currentState != PlayerState.attack && currentState != PlayerState.staggered)
        {
            StartCoroutine(attackCO());
        }

        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }                     
    }

    private IEnumerator attackCO()
    {
        currentState = PlayerState.attack;
        animator.SetBool("Atacking",true);
        yield return null;
        animator.SetBool("Atacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;

    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
           
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
       
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
    
    public void Knock(float knockTime, float dmg)
    {
        currentHealth.RuntimeValue -= dmg;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            
            StartCoroutine(knockCO(knockTime));
        }
        else
        {
            SceneManager.LoadSceneAsync("SampleScene");
            currentHealth.RuntimeValue = currentHealth.initialValue;
            //this.gameObject.SetActive(false);

        }
        

    }

    private IEnumerator knockCO(float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
