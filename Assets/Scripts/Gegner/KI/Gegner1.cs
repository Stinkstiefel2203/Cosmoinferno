using UnityEngine;

public class Gegner1 : MonoBehaviour
{
    public GameObject explosion;
    public GameObject Kugel;
    public int hp = 1;
    private float wann;
    private float startx;
    private bool rechts = true;
    private DifficultyManager targetScript;
    private float AttackSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Difficulty zeugs:
        targetScript = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>(); //finde das script vom DifficultyManager
        //setze die difficulty werte in den gegner
        hp = targetScript.Gegner1HP;
        AttackSpeed = targetScript.EnemyAttackSpeedMultiplayer;

        wann = Time.time + (2 * AttackSpeed); //wann geschossen wird
        startx = transform.position.x;  //speichert die ursprungskoordinate vom x punkt
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");  //finde den spieler mit hilfe von einem tag und packe ihn in eine variable

        if (player != null) //wenn der spieler exestiert
        {
            if (transform.position.y > -6)   //wenn der gegner noch nicht auserhalb vom bildschirm ist
            {
                transform.position = transform.position + new Vector3(0f, -0.02f, 0f);  //gehe nach unten
            }
            else
            {
                Destroy(gameObject); //zerstört den gegner wenn er vom bildschirm runter ist
            }

            if (rechts)   //wenn er nach rechts gehen soll
            {
                transform.position = transform.position + new Vector3(0.05f, 0f, 0f);  //gehe nach rechts
            }
            else  //wenn er nicht nach rechts gehen soll (also nach links)
            {
                transform.position = transform.position + new Vector3(-0.05f, 0f, 0f);  //gehe nach links
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

        if (transform.position.x >= startx + 2f)   //wenn der gegner 2 einheiten von seinem startpunkt nach rechts entfernt ist setze rechts false
        { rechts = false; }

        if (transform.position.x <= startx - 2f)   //wenn der gegner 2 einheiten von seinem startpunkt nach links entfernt ist setze rechts true
        { rechts = true; }

        if (wann <= Time.time && (transform.position.y <= 5)) //wenn zwei sekunden vergangen sind und der gegner auf dem bildschirm ist
        {
            wann = Time.time + (2 * AttackSpeed); //wann geschossen wird
            Instantiate(Kugel, transform.position, transform.rotation);   //spawnt ein schuss an der position vom gegner
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
