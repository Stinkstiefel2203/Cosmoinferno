using System.Collections;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class Boss : MonoBehaviour
{
    public GameObject Credits;
    private DifficultyManager targetScript;
    public int hp = 10;
    private float wann = 0;
    private bool rechts = true;
    public GameObject[] Explosionen;
    private bool Tot = false;
    public GameObject Bombe;
    public GameObject WarnungVertikal;
    public GameObject AudioLaser;
    public GameObject Laser;
    private bool Attacke2bool = true;
    public GameObject PolishCow;
    private bool Attacke2bool2 = true;
    private bool Verfolgen;
    private GameObject Warnung;
    private GameObject AudioLaser1;
    private GameObject Laser1;
    public GameObject[] Gegners;
    private bool Attacke3bool = true;
    private int Attacke4Phase = 1;
    public GameObject WarnungHorizontal;
    private GameObject Warnung1;
    private bool AttackeAngefangen = false;
    private System.Action[] attacken;
    private int NaechsteAttacke;
    private int anzahl = -1;
    private float attackendelay = 0;
    private bool Angekommen = false;
    private GameObject HealthBar;
    private Canvas HBCanvas;
    private Slider HealthbarSlider;
    private float AttackSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Difficulty zeugs:
        targetScript = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>(); //finde das script vom DifficultyManager
        //setze die difficulty werte in den boss
        hp = targetScript.BossHP;
        AttackSpeed = targetScript.EnemyAttackSpeedMultiplayer;


        //Health bar zeugs:
        HealthBar = GameObject.Find("Canvas");   //Finde das GameObject von der Canvas
        HBCanvas = HealthBar.GetComponent<Canvas>();   //Finde die Canvas im game object
        HealthbarSlider = HealthBar.GetComponent<Slider>(); //finde den slider in dem Canvas
        HBCanvas.enabled = true; //schalte das rendern von der Health bar an
        HealthbarSlider.maxValue = hp;  //gebe der healthbar den maximum wert vom boss hp
        HealthbarSlider.value = hp; //setze den wert der health bar zur hp vom boss


        gameObject.name = "Boss"; //damit wenn der boss gesucht wird von der polish cow das er gefunden wird weil er sonst (cloned) im namen hat
        GetComponent<AudioSource>().Play();  //spiele die boss musik ab
        GameObject.Find("Hintergrundmusik").GetComponent<AudioSource>().Stop(); //Stoppe die hintergrundmusik
        attacken = new System.Action[]  //packe alle Attacken in ein Array
        {
                Attacke1,
                VorbereitungAttacke2,
                () => StartCoroutine(Attacke3()),  //weil es eine IEnumeration ist muss () => am anfang hin um es zu einer funktion zu machen
                () => StartCoroutine(Attacke4())
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)          //wenn eine collision mit dem objekt passiert
    {
        if (collision.gameObject.tag == "Schussgut")         // wenn das objekt was das objekt berührt hat eine kugel ist
        {
            Destroy(collision.gameObject);        //zerstört die kugel
            hp = hp - 1;
            HealthbarSlider.value = hp; //Setze das hp zur Health Bar
            if (hp <= 0 && !Tot)      //wenn der boss keine leben mehr hat und noch nicht tot ist
            {
                Tot = true;      //mach ihn tot
                StartCoroutine(selbstzerstoerung());     //starte die selbstzerstörung vom boss
            }
        }
    }

    private void FixedUpdate()
    {
        if (Angekommen) //wenn der boss angekommen ist
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");  //finde den spieler mit hilfe von einem tag und packe ihn in eine variable

            if (player != null) //wenn der spieler exestiert
            {
                if (!Tot)   //wenn der boss nicht tot ist
                {
                    if (!AttackeAngefangen) //wenn der boss grad keine attacke macht
                    {
                        NaechsteAttacke = Random.Range(0, 4); //wähle eine von den 4 attacken als nächstes aus
                        AttackeAngefangen = true;
                    }
                    else if (attackendelay < Time.time)  //wenn der boss gerade eine attacke macht und das delay vergangen ist
                    {
                        attacken[NaechsteAttacke](); //mache die zuvor ausgewählte attacke
                    }
                }
            }
            else if (transform.position.y < 9.00)   //wenn kein spieler existiert checke ob der gegner noch nicht ausserhalb vom bildschirm ist
            {
                if (Warnung == null && Laser1 == null && AudioLaser1 == null && Warnung1 == null) //wenn der boss nicht während einer attacke ist
                {
                    transform.position = transform.position + new Vector3(0f, 0.2f, 0f);  //gehe nach oben
                }
            }
            else //wenn der boss ausserhalb vom bildschirm ist
            {
                GameObject.Find("Hintergrundmusik").GetComponent<AudioSource>().Play(); //Startet die hintergrundmusik wieder
                StartCoroutine(BossZerstören()); //zerstöre den boss
            }
        }
        else //wenn nicht dann gehe zur startposition
        {
            Startpos();
        }
    }

    IEnumerator selbstzerstoerung()    //erstellt 5 verschiedene explosionen am boss und zerstört ihn dann
    {
        int wiederholungen = 0;      //setzt die wiederholungen auf 0
        while (wiederholungen < 5)    //wenn es sich noch nicht 5 mal wiederholt hat
        {
            wiederholungen++;
            yield return new WaitForSecondsRealtime(1);   //warte 1 sekunde multipliziert mit der attackspeed (funktioniert nur in IEnumerator funktionen)
            int i = 0;                  //index wird 0 gesetzt
            int ig = Random.Range(1, 3);    //gibt entweder 1 oder 2 und setzt den richtigen index um zu bestimmen welche explosion benutzt wird
            foreach (GameObject explosion in Explosionen)    //für jede explosion in dem explosionen array wird das folgende ausgeführt
            {
                i++;               //index geht höher um zu sehen welcher wert das in dem array Explosionen ist
                if (i == ig)   //wenn der index der explosione den gewünschten index hat
                {
                    float x = transform.position.x + Random.Range(-5f, 5f);  //setzt die zukünftige x koordinate in einem bereich zwischen -5 und 4.9999999 vom boss. dies ist anders als davor weil es floats sind und nicht intiger
                    float y = transform.position.y + Random.Range(-1.5f, 1.5f);  //setzt die zukünftige y koordinate in einem bereich zwischen -1.5 und 1.4999999 vom Boss
                    Instantiate(explosion, new Vector3(x, y, 0), transform.rotation);  //spawnt eine explosion an den koordinaten und dabei die explosione die den index von der zufällig generierten zahl davor hat
                }  //damit es nicht mehrere male die gleiche explosion ist
            }
        }
        Instantiate(Credits, new Vector3(0, 30, 0), transform.rotation);  //spawnt die credits
        StartCoroutine(BossZerstören()); //zerstöre den boss
    }

    private void Attacke1()   //Atacke 1 schieße 3 Bomben und bewege dich rechts und links
    {
        if (anzahl == -1)   //damit anzahl nicht die ganze zeit 0 gesetzt wird und es unendlich oft schießt
        {
            anzahl = 0;  
        }
            if (rechts)   //wenn er nach rechts gehen soll
            {
                transform.position = transform.position + new Vector3(0.05f, 0f, 0f);  //gehe nach rechts
            }
            else  //wenn er nicht nach rechts gehen soll (also nach links)
            {
                transform.position = transform.position + new Vector3(-0.05f, 0f, 0f);  //gehe nach links
            }

        if (transform.position.x >= 4) //wenn er am rechten rand ist setze rechts false
        { rechts = false; }

        if (transform.position.x <= -4) //wenn er am linken rand ist setze rechts true
        { rechts = true; }


        if (wann <= Time.time) //wenn eine sekunde vergangen ist spawne 3 bomben die wie eine schrotflinte sind
        {
            anzahl++;
           wann = Time.time + (AttackSpeed * 1f); //wann geschossen wird   
           Instantiate(Bombe, transform.position, transform.rotation);   //spawnt eine Bombe an der position vom gegner weil die normale bombe immer nach unten geht muss nichts verändert werden
            GameObject Bombe1 = Instantiate(Bombe, transform.position, transform.rotation);   //spawnt eine Bombe an der position vom gegner
            Bombe1.GetComponent<Bombe>().XRichtung = 1; //setze die X richtung in dem script zu 1 damit die bombe sich nach rechts bewegt
            GameObject Bombe2 = Instantiate(Bombe, transform.position, transform.rotation);   //spawnt eine Bombe an der position vom gegner
            Bombe2.GetComponent<Bombe>().XRichtung = -1; //setze die X richtung in dem script zu -1 damit die bombe sich nach links bewegt
        }
        if (anzahl >= 5) //wenn der boss 5 mal geschossen hat beende die attacke
        {
            attackendelay = Time.time + 2 * AttackSpeed;  //setze die wartezeit der nächsten attacke zu 2 sekunden multipliziert mal der attackspeed damit man ersmal die bomben ausweichen kann
            AttackeAngefangen = false;
            anzahl = -1;  //setze die anzahl wiederr -1 damit es beim nächsten mal wieder funktioniert
        }
    }

    private void VorbereitungAttacke2()     //bewege dich hin über den spieler
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");  //finde den spieler mit hilfe von einem tag und packe ihn in eine variable
        if (Attacke2bool) //wenn es zum ersten mal passiert
        {
            Verfolgen = true;    //der boss soll den spieler verfolgen
            Attacke2bool2 = true;  //die attacke wurde noch nicht gemacht
            Attacke2bool = false; //damit es nicht ein zweites mal passiert
            wann = Time.time + (AttackSpeed * 3);  //setze wann die attacke gemacht wird also 3 sekunden in der zukunft multipliziert mal der attackspeed
        }
        if (player != null && Verfolgen) //wenn der spieler exestiert und der gegner den spieler verfolgen soll
        {
            if (transform.position.x < player.transform.position.x - 0.15f)   //wenn der spieler rechts vom gegner ist (mit buffer zone von 0.15)
            {
                transform.position = transform.position + new Vector3(0.1f, 0f, 0f);  //gehe nach rechts
            }
            else if (transform.position.x > player.transform.position.x + 0.15f)  //wenn der spieler links vom gegner ist (mit buffer zone von 0.15)
            {
                transform.position = transform.position + new Vector3(-0.1f, 0f, 0f);  //gehe nach links
            }
        }
        if (wann < Time.time && Attacke2bool2)   //wenn die attacke gemacht werden soll uns sie noch nicht gemacht wurde
        {
            Verfolgen = false;  //mach das der boss stehen bleibt
            Attacke2bool2 = false; //damit es nicht ein zweites mal passiert
            StartCoroutine(Attacke2());   //starte die attacke
        }
    }

    IEnumerator Attacke2()   //schießt einen laser von oben auf dem spieler
    {
        Warnung = Instantiate(WarnungVertikal, transform.position + new Vector3(0,-5,0), transform.rotation); //spawnt eine warnung für den spieler wo die attacke gleich passiert
        AudioLaser1 = Instantiate(AudioLaser, transform.position, transform.rotation); //spawnt Das audio von dem Laser
        if (Random.Range(1, 3) == 1)  //50% der zeit spawnt die kuh
        {
            Instantiate(PolishCow, transform.position + new Vector3(0, -25, 0), transform.rotation); //spawnt Die Polish Cow
        }
        yield return new WaitForSecondsRealtime(0.65f); //warte 0.65 sekunden (funktioniert nur in IEnumerator funktionen)
        Destroy(Warnung.gameObject); //entferne die warnung
        Laser1 = Instantiate(Laser, transform.position + new Vector3(0, -5, 0), transform.rotation); //spawnt die Attacke }
        yield return new WaitForSecondsRealtime(2.6f); //warte 2.6 sekunden (funktioniert nur in IEnumerator funktionen)
        Destroy(Laser1.gameObject); //entferne den laser
        Destroy(AudioLaser1.gameObject); //entferne das audio
        Attacke2bool = true; //damit die attacke beim nächsten mal funktioniert
        AttackeAngefangen = false;
    }

    IEnumerator BossZerstören()  //zertsört den Boss wenn er nicht beschäftigt ist
    {
        yield return new WaitUntil(() => Warnung == null && Laser1 == null && AudioLaser1 == null && Warnung1 == null);  //warte bis keine warnung laser oder audio gespawnt ist
        {
            HBCanvas.enabled = false; //schalte das rendern der Health bar aus
            Destroy(gameObject);  //zerstört den boss
        }
    }
IEnumerator Attacke3()   //spawne 3 random gegner
    {
        if (Attacke3bool)  //wenn noch keine gegner gespawnt wurden
        {
            Attacke3bool = false;
            int Wiederholungen = 0;
            while (Wiederholungen < 3) //wiederhole 3 mal
            {
                yield return new WaitForSecondsRealtime(AttackSpeed * 0.5f); //warte 0.5 sekunden multipliziert mal der attackspeed (funktioniert nur in IEnumerator funktionen) zwischen spawns
                Wiederholungen++;   //erhöht die wiederholungen um 1
                int i = 0; //index = 0
                int ig = Random.Range(1, 4); //setzt den gewünschten index zwischen 1 und 3
                foreach (GameObject Gegner in Gegners)    //für jeden gegner in der liste
                {
                    i++; //erhöht den index um 1
                    if (i == ig) //wenn der index der gewünschte index ist
                    {
                        Instantiate(Gegner, new Vector3(Random.Range(-75, 76) / 10, 9, 0), transform.rotation);  //erstelle den gegner über dem bildschirm und auf der x koordinate verschoben zwischen koordinate x -7.5 und 7.5
                    }
                }
            }
            yield return new WaitForSecondsRealtime(AttackSpeed * 5); //warte 5 sekunden multipliziert mal der difficulty (funktioniert nur in IEnumerator funktionen) damit man zeit hat die gegner zu töten
            Attacke3bool = true; //damit es beim nächsten mal gegner spawnen kann
            AttackeAngefangen = false;
        }
    }

    IEnumerator Attacke4() //geht aus dem bildschirm auf der höhe des spielers und schwingt auf die andere seite um den spieler zu treffen
    {
        if (Attacke4Phase == 1) //wenn der boss in der ersten phase von der attacke ist
        {
            if (transform.position.x < 15)   //wenn der gegner noch nicht ausserhalb vom bildschirm ist
            {
                transform.position = transform.position + new Vector3(0.5f, 0f, 0f);  //gehe nach rechts
            }
            else //wenn der boss ausserhalb ist
            {
                Attacke4Phase = 0; //setze die phase der attacke zu 0 damit der boss eine kurze pause macht um den spieler zeit zum ausweichen zu geben
                GameObject player = GameObject.FindGameObjectWithTag("Player"); //finde den Spieler
                float playerY = player.transform.position.y; //finde die y koordinate vom spieler
                GameObject Warnung1 = Instantiate(WarnungHorizontal, new Vector3(0, playerY, 0), transform.rotation); //spawne die warnung für die attacke an der höhe vom spieler
                transform.position = new Vector3(15, playerY, 0); //packt den boss auf der höhe des spielers
                yield return new WaitForSecondsRealtime(0.5f); //warte 0.5 sekunden (funktioniert nur in IEnumerator funktionen) um den spieler reaktionszeit zu geben
                Attacke4Phase = 2; //setze die phase der attacke zu 2
                Destroy(Warnung1); //entferne die warnung
            }
        }
        else if (Attacke4Phase == 2)  //wische einmal über den bildschirm wenn der boss in der zweiten phase von attacke 4 ist
        {
            if (transform.position.x > -15)   //wenn der gegner noch nicht Über den bildschirm gewischt ist
            {
                transform.position = transform.position + new Vector3(-0.5f, 0f, 0f);  //gehe nach links
            }
            else //wenn doch
            {
                Attacke4Phase = 3; //setze die phase der attacke zu 3
                transform.position = new Vector3(0, 9, 0); //packt den boss wieder über dem bildschirm
            }
        }
        else if (Attacke4Phase == 3) //packt den boss wieder auf dem bildschirm wenn er in der dritten phase von attacke 4 ist
        {
            Angekommen = false;
        }
    }

    private void Startpos() //geht zur startposition wieder zurück
    {
        if (transform.position.y > 3.6) //wenn der boss zu weit oben ist
        {
            transform.position = new Vector3(0, transform.position.y - 0.1f, 0); //gehe nach unten
        }
        else
        {
            transform.position = new Vector3(0, 3.6f, 0); //setze den Boss genau auf die höhe 3.6 wegen float errors damit er genau am gleichen ort immer landet
            Attacke4Phase = 1; //setze die phase wieder zu 1 nötig falls in attacke 4 benutzt
            AttackeAngefangen = false;  //nötig falls in attacke 4 benutzt damit die nächste attacke beginnen kann
            Angekommen = true;  //setze angekommen so dass er mit den attacken beginnen kann oder weitemachen kann
        }
    }
}