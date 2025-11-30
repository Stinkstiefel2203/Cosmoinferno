using UnityEngine;

public class Bombe : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f; // Variable die geschwindigkeit vorgibbt von der kugel
    public GameObject[] Kugeln; //liste mit allen kugeln die gespawn werden sollen
    private float wann;
    public int YRichtung;
    public int XRichtung;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wann = Time.time + 1;   //wann die kugel explodieren soll (nach einer sekunde)
    }

    // Update is called once per frame
    void Update()
    {
        if (wann <= Time.time)
        {
            foreach (GameObject Kugel in Kugeln)      //für jedes objekt in der liste
            {
                Instantiate(Kugel, transform.position, transform.rotation);   //erstelle es an der position der Bombe
            }
            Destroy(gameObject); //Bombe wird entfernt
        }
    }
    void FixedUpdate()     //passiert immer in einen bestimmten zeit abstand damit wenn man super viele frames hat nicht zu oft passiert
    {
        if (XRichtung != 0 && YRichtung != 0)   //wenn der schuss diagonal gehen soll
        {
            rb.linearVelocityX = XRichtung * speed * 0.705f;      //multipliziert mal geschwindigkeit und setzt vom rigidbody die geschwindigkeit und bremse ihn aus weil a^2 + b^2 = c^2 damit c genau 1 einheit schnell ist ist weil 0.705^2 + 0.705^2 ist ungefähr 1
            rb.linearVelocityY = YRichtung * speed * 0.705f;      //setzt vom rigidbody die geschwindigkeit
        }
        else   //wenn er nicht diagonal geht
        {
            rb.linearVelocityX = XRichtung * speed;      //multipliziert mal geschwindigkeit und setzt vom rigidbody die geschwindigkeit
            rb.linearVelocityY = YRichtung * speed;      //setzt vom rigidbody die geschwindigkeit
        }
    }
}
