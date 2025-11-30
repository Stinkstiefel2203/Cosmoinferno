using UnityEngine;

public class Gegner2 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject[] Kugeln;
    public float speed = 1f;
    private float wann;
    public int hp = 1;
    private bool angekommen = false;
    private DifficultyManager targetScript;
    private float AttackSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Difficulty zeugs:
        targetScript = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>(); //finde das script vom DifficultyManager
        //setze die difficulty werte in den gegner
        hp = targetScript.Gegner2HP;
        AttackSpeed = targetScript.EnemyAttackSpeedMultiplayer;

        wann = Time.time + (2 * AttackSpeed); //wann geschossen wird
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
                if (transform.position.x < player.transform.position.x - 0.1f)   //wenn der spieler rechts vom gegner ist (mit buffer zone von 0.1)
                {
                    transform.position = transform.position + new Vector3(0.05f * speed, 0f, 0f);  //gehe nach rechts
                }
                else if (transform.position.x > player.transform.position.x + 0.1f)  //wenn der spieler links vom gegner ist (mit buffer zone von 0.1)
                {
                    transform.position = transform.position + new Vector3(-0.05f * speed, 0f, 0f);  //gehe nach links
                }

                if (wann <= Time.time) //wenn zwei sekunden vergangen sind
                {
                 wann = Time.time + (2 * AttackSpeed); //wann geschossen wird
                    foreach (GameObject Kugel in Kugeln)      //für jedes objekt in der liste
                    {
                        Instantiate(Kugel, transform.position, transform.rotation);   //erstelle es an der position der Bombe
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
