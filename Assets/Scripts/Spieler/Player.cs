using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Kugel;   //variable für einen schuss
    public float speed = 5f; // Variable die geschwindigkeit vorgibbt vom spieler (public kann im editor bearbeitet werden ohne den code zu öffnen)
    public float diagonalbremse = 0.8f;
    private Vector2 movement; //Variable für später im code
    private Rigidbody2D rb; //Variable für später im code
    private float naechsterschusszeit;
    public GameObject explosion;
    public GameObject gameover;
    public float Attackspeed;
    public int hp = 1;
    private DifficultyManager targetScript;
    public float Deadzone = 0.3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()  // passiert immer beim start vom script
    {
        //Difficulty zeugs:
        targetScript = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>(); //finde das script vom DifficultyManager
        //setze die difficulty werte in den Spieler
        hp = targetScript.PlayerHP;
        Attackspeed = targetScript.PlayerAttackSpeed;

        rb = GetComponent<Rigidbody2D>();               // variable einen wert geben rigidbody = physik vom object
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;             // damit es bei berührungen nicht dreht
    }

    // Update is called once per frame
    void Update()      //passiert bei jeden frame
    {
        if (((Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 1")) && Time.time >= naechsterschusszeit) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))  //guckt ob die space bar gedrückt gehalten wird und 0.5 sekunden vergangen sind seit letzten schuss oder ob die space bar gedrückt wurde funktioniert auch mit x auf einem playstation controller
        {
            Schuss();
        }

        //wenn ein controller benutzt wurde normalisiere auf tastatur inputs:
        float horizontalInput1 = Input.GetAxisRaw("Horizontal");   //wenn die inputs nicht komplett drinne sind werden sie auf eins gesetzt als hätte man sie komplett gedrückt und wenn man nicht genug gedrückt hat wird der input null gesetzt damit keine kommazahlen kommen
        if (horizontalInput1 > Deadzone || horizontalInput1 < -Deadzone)
        {
            if (horizontalInput1 < -Deadzone)
            {
                horizontalInput1 = -1;
            }
            if (horizontalInput1 > Deadzone)
            {
                horizontalInput1 = 1;
            }
        }
        else
        {
            horizontalInput1 = 0;
        }

            float verticalInput1 = Input.GetAxisRaw("Vertical"); //wenn die inputs nicht komplett drinne sind werden sie auf eins gesetzt als hätte man sie komplett gedrückt und wenn man nicht genug gedrückt hat wird der input null gesetzt damit keine kommazahlen kommen
        if (verticalInput1 > Deadzone || verticalInput1 < -Deadzone)
        {
            if (verticalInput1 < -Deadzone)
            {
                verticalInput1 = -1;
            }
            if (verticalInput1 > Deadzone)
            {
                verticalInput1 = 1;
            }
        }
        else
        {
            verticalInput1 = 0;
        }


        //verarbeitung der eingabe:
        float horizontalInput = horizontalInput1 * speed;   //nimmt horizontale inputs wie: a d pfeiltaste links oder pfeiltaste rechts und multipliziert mal geschwindigkeit
        float verticalInput = verticalInput1 * speed;        //nimmt vertikale inputs wie: w s pfeiltaste oben oder pfeiltaste unten und multipliziert mal geschwindigkeit
        if (horizontalInput != 0 && verticalInput != 0)
        {
            movement = new Vector2(horizontalInput * diagonalbremse, verticalInput * diagonalbremse); //wenn man diagonal geht verlangsamen damit die diagonale geschwindigkeit nicht zu schnell ist im gegensatz zu vertikal und horizontal
        }  
        else {
            movement = new Vector2(horizontalInput, verticalInput); //erstell einen vector für die bewegungsrichtung mit dem inputs }
        }     
    }

    void FixedUpdate()     //passiert immer in einen bestimmten zeit abstand damit wenn man super viele frames hat nicht zu oft passiert
    {
        rb.linearVelocity = movement;      //nimmt den input vom spieler und setzt vom rigidbody die geschwindigkeit
    }

    void Schuss()
    {
        Instantiate(Kugel, transform.position, transform.rotation);   //spawnt ein schuss an der position vom spieler
        naechsterschusszeit = Time.time + Attackspeed; //nimmt die jetzige zeit und fügt eine halbe sekunde hinzu und setzt es in eine variable
    }
    private void OnTriggerEnter2D(Collider2D collision)          //wenn eine collision mit dem objekt passiert
    {
        if (collision.gameObject.tag == "SchussB" || collision.gameObject.tag == "Gegner")         // wenn das objekt was den Spieler berührt hat eine kugel oder ein gegner ist
        {
            hp = hp - 1; //entfernt ein hp
            if (collision.gameObject.tag != "Gegner")         // wenn das objekt was den Spieler berührt hat kein gegner ist
            {
                Destroy(collision.gameObject);        //zerstört die kugel
            }
            if (hp <= 0) //wenn der spieler keine leben mehr hat
            {
                Instantiate(explosion, transform.position, transform.rotation);   //spawnt eine explosion am objekt
                Instantiate(gameover, new Vector3(0,9,0), transform.rotation);   //spawnt den game over text
                    Destroy(gameObject);  //zerstört den Spieler
            }
            else //falls der spieler noch leben hat
            {
                gameObject.GetComponent<AudioSource>().Play(); //Spiele den Hit sound ab
            }
        }
    }
}