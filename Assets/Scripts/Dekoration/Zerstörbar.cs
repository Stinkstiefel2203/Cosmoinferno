using UnityEngine;

public class Zerstörbar : MonoBehaviour
{
    public GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            Instantiate(explosion, transform.position, transform.rotation);   //spawnt eine explosion am objekt
            Destroy(gameObject);  //zerstört das objekt
        }
    }
}
