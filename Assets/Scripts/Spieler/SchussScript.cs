using UnityEngine;

public class SchussScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f; // Variable die geschwindigkeit vorgibbt von der kugel
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= 6)
        {
            Destroy(gameObject); //wenn die kugel ausserhalb vom bildschirm ist wird sie entfernt um ressourcen zu sparen
        } 
    }
    void FixedUpdate()     //passiert immer in einen bestimmten zeit abstand damit wenn man super viele frames hat nicht zu oft passiert
    {
        rb.linearVelocityY = 1 * speed;      //multipliziert mal geschwindigkeit und setzt vom rigidbody die geschwindigkeit
    }
}
