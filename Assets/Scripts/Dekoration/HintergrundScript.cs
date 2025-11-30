using UnityEngine;

public class HintergrundScript : MonoBehaviour
{
    public float speed = 0.1f;   //Variable für die geschwindigkeit nach unten
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = transform.position - new Vector3(0f, speed, 0f);        //setzt die position vom objekt zu weiter nach unten
    }
}
