using UnityEngine;

public class Gegner3 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Kugel;
    public GameObject[] Kugel2;
    private float wann;
    public int hp = 1;
    private bool rechts = true;
    private bool angekommen = false;
    private DifficultyManager targetScript;
    private float AttackSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Difficulty zeugs:
        targetScript = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>(); //finde das script vom DifficultyManager
        //setze die difficulty werte in den gegner
        hp = targetScript.Gegner3HP;
        AttackSpeed = targetScript.EnemyAttackSpeedMultiplayer;

        wann = Time.time + (2 * AttackSpeed); //wann geschossen wird zwei sekunden multipliziert mit der attackspeed nach dem spawnen
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!angekommen) //wenn er noch nicht angekommen ist (damit wenn er angekommen ist die variable nicht die ganze zeit auf true gesetzt wird)
        {
            if (transform.position.y > 3.05f)   //wenn der gegner zu weit oben ist
            {
                transform.position = transform.position + new Vector3(0f, -0.1f, 0f);  //gehe nach unten
            }
            else
            {
                angekommen = true;   //variable er ist angekommen
            }
        }

        if (angekommen)  //wenn der gegner innerhalb des bildschirms ist
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");  //finde den spieler mit hilfe von einem tag und packe ihn in eine variable

            if (player != null) //wenn der spieler exestiert
            {
                if (rechts)   //wenn er nach rechts gehen soll
                {
                    transform.position = transform.position + new Vector3(0.05f, 0f, 0f);  //gehe nach rechts
                }
                else  //wenn er nicht nach rechts gehen soll (also nach links)
                {
                    transform.position = transform.position + new Vector3(-0.05f, 0f, 0f);  //gehe nach links
                }

                if (transform.position.x >= 7.5) //wenn er am rechten rand ist setze rechts false
                { rechts = false; }

                if (transform.position.x <= -7.5) //wenn er am linken rand ist setze rechts true
                { rechts = true; }


                if (wann <= Time.time) //wenn zwei sekunden vergangen sind
                {
                    wann = Time.time + (2 * AttackSpeed); //wann geschossen wird
                    Instantiate(Kugel, transform.position, transform.rotation);   //spawnt ein schuss an der position vom gegner
                    foreach (GameObject Kugel in Kugel2)      //für jedes objekt in der liste
                    {
                        Instantiate(Kugel, transform.position, transform.rotation);   //erstelle es an der position des gegners
                    }
                }
            }
            else if (transform.position.y < 9.00)   //wenn kein spieler existiert checke ob der gegner noch nicht ausserhalb vom bildschirm ist
            {
                transform.position = transform.position + new Vector3(0f, 0.1f, 0f);  //gehe nach oben
            }
            else
            {
                Destroy(gameObject);   //wenn ausserhalb des bildschirm entferne dich
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)          //wenn eine collision mit dem objekt passiert
    {
        if (collision.gameObject.tag == "Schussgut")         // wenn das objekt was das objekt berührt hat eine kugel ist
        {
            hp = hp - 1;
            Destroy(collision.gameObject);        //zerstört die kugel
            if (hp <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);   //spawnt eine explosion am objekt
                Destroy(gameObject);  //zerstört das objekt
            }
        }
    }
}
