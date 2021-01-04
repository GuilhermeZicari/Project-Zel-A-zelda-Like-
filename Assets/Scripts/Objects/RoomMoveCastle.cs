using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMoveCastle : MonoBehaviour
{
    public Vector2 cameraChange1;
    public Vector3 playerChange1;
    private Camera_Movement cam1;
    public string nomeMapa1;
    public bool show1;
    public GameObject canvas1;
    public Text conteudoCanvas1;



    // Start is called before the first frame update
    void Start()
    {
        cam1 = Camera.main.GetComponent<Camera_Movement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            
            cam1.maxPosition += cameraChange1;
            other.transform.position += playerChange1;
            if (show1)
            {
                StartCoroutine(nomeLugarCO1());
            }
        }

    }
    private IEnumerator nomeLugarCO1()
    {
        canvas1.SetActive(true);
        conteudoCanvas1.text = nomeMapa1;
        yield return new WaitForSeconds(4F);
        canvas1.SetActive(false);

    }
}
