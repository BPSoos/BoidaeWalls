using UnityEngine;

public class Fej : MonoBehaviour
{
    
    public float speed = 15f;
    public float rotationSpeed = 200f;

    float horizontal = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate ()
    {
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward *  -horizontal * rotationSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log(col.name);
    }

}
