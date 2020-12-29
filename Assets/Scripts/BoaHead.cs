using UnityEngine;
using UnityEngine.Serialization;

public class BoaHead : MonoBehaviour
{
     public float speed = 2.15f;
     public float rotationSpeed = 170f;

     public string inputAxis = "Horizontal";
     private float _horizontal = 0f;
     public BoaPlayer1 myPlayer;
     void Update()
    {        
        _horizontal = GameInputManager.GetAxisRaw(inputAxis);
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
            myPlayer.lost = true;
            myPlayer.myManager.deadPlayers ++;
            if (myPlayer.myManager.deadPlayers == PlayerData.GamePlayers.Count - 1)
                myPlayer.myManager.EndGame();
        }
    }
}
