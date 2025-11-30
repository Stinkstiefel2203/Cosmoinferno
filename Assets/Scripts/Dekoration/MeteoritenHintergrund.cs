using UnityEngine;

public class MeteoritenHintergrund : MonoBehaviour
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
        if (transform.position.y > -9)   //wenn die meteoriten noch nicht einmal überm bildschirm geflogen sind
        {
            transform.position = transform.position - new Vector3(0f, speed, 0f);        //setzt die position vom objekt zu weiter nach unten
        }
        else
        {
            Destroy(gameObject); //zerstöre dich wenn du ausserhalb vom bildschirm bist
        }
    }
}
