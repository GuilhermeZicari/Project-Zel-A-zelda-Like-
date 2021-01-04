using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private Camera_Movement cam;
    public string nomeMapa;
    public bool show;
    public GameObject canvas;
    public Text conteudoCanvas;
    public Vector2 buffer;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera_Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            buffer = cameraChange;
            cam.minPosition +=  buffer;
            cam.maxPosition +=   buffer;
            other.transform.position += playerChange;
            buffer.x = 0;
            buffer.y = 0;
                if (show)
            {
                StartCoroutine(nomeLugarCO());
            }
        }
        
    }
    private IEnumerator nomeLugarCO()
    {
        canvas.SetActive(true);
        conteudoCanvas.text = nomeMapa;
        yield return new WaitForSeconds(4F);
        canvas.SetActive(false);
           

    }
}
