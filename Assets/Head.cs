using UnityEngine;

public class Head : MonoBehaviour
{
     public float speed = 2.15f;
     public float rotationSpeed = 170f;

     public string InputAxis = "Horizontal";
     private float horizontal = 0f;

    // Update is called once per frame
    void Update()
    {        
        horizontal = Input.GetAxisRaw(InputAxis);
    }

    void FixedUpdate(){
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward *  -horizontal * rotationSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D (Collider2D col){
        if (col.tag == "deadlyObject"){
            speed = 0;
            rotationSpeed = 0;
            GameObject.FindObjectOfType<GameManager>().EndGame();
        }
    }
}
