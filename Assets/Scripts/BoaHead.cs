using UnityEngine;

public class BoaHead : MonoBehaviour
{
     public float speed = 2.15f;
     public float rotationSpeed = 170f;

     public string InputAxis = "Horizontal";
     private float horizontal = 0f;


    void Update()
    {        
        horizontal = Input.GetAxisRaw(InputAxis);
    }

    void FixedUpdate(){
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward *  -horizontal * rotationSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D (Collider2D col){
        Debug.Log(col.name);
        if (col.tag == "deadlyObject"){
            speed = 0;
            rotationSpeed = 0;
            GameManager my_manager = GameObject.FindObjectOfType<GameManager>();
            if (my_manager.dead_players == 3)
                my_manager.EndGame();
            else
                my_manager.dead_players ++;
        }
    }
}
