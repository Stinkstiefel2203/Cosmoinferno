using UnityEngine;

public class Normal : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f; // Variable die geschwindigkeit vorgibbt von der kugel
    public int YRichtung;
    public int XRichtung;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5); //kugel wird entfernt nach 5 sekunden um ressourcen zu sparen
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()     //passiert immer in einen bestimmten zeit abstand damit wenn man super viele frames hat nicht zu oft passiert
    {
        if (XRichtung != 0 && YRichtung != 0)   //wenn der schuss diagonal gehen soll
        {
            rb.linearVelocityX = XRichtung * speed * 0.705f;      //multipliziert mal geschwindigkeit und setzt vom rigidbody die geschwindigkeit und bremse ihn aus weil a^2 + b^2 = c^2 damit c genau 1 einheit schnell ist ist weil 0.705^2 + 0.705^2 ist ungefähr 1
            rb.linearVelocityY = YRichtung * speed * 0.705f;      //setzt vom rigidbody die geschwindigkeit multipliziert auch mit der richtung damit es in die richtige richtung geht
        }
        else   //wenn er nicht diagonal geht
        {
            rb.linearVelocityX = XRichtung * speed;      //multipliziert mal geschwindigkeit und setzt vom rigidbody die geschwindigkeit
            rb.linearVelocityY = YRichtung * speed;      //setzt vom rigidbody die geschwindigkeit
        }
    }
}