using UnityEngine;
using UnityEngine.Serialization;

public class BoaHead : MonoBehaviour
{
     public float speed = 2.15f;
     public float rotationSpeed = 170f;

     [FormerlySerializedAs("InputAxis")] public string inputAxis = "Horizontal";
     private float _horizontal = 0f;


    void Update()
    {        
        _horizontal = Input.GetAxisRaw(inputAxis);
    }

    void FixedUpdate(){
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward *  -_horizontal * rotationSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D (Collider2D col){
        Debug.Log(col.name);
        if (col.tag == "deadlyObject"){
            speed = 0;
            rotationSpeed = 0;
            GameManager myManager = GameObject.FindObjectOfType<GameManager>();
            if (myManager.deadPlayers == 3)
                myManager.EndGame();
            else
                myManager.deadPlayers ++;
        }
    }
}
