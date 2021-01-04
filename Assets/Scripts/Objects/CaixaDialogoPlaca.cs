using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CaixaDialogoPlaca : Interactable
{
    public string textoPlaca;   
    public GameObject caixaDeTexto;
    public Text dialogoDaCaixa;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        naArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && naArea)
        {
            if (caixaDeTexto.activeInHierarchy == true)
            {
                  
                caixaDeTexto.SetActive(false);
                
            }
            else
            {
                caixaDeTexto.SetActive(true);
            }
        }

        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger )
        {
            context.Raise();
            naArea = true;
            dialogoDaCaixa.text = textoPlaca;
        }


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            context.Raise();
            naArea = false;
            caixaDeTexto.SetActive(false);
        }
    }


   private IEnumerator ClosCO(bool naArea)
    {
        if(naArea == false)
        {
            caixaDeTexto.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }

    }

}
