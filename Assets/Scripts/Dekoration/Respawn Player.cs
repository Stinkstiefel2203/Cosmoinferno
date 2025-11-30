using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    public GameObject player;
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
        if (transform.position.y <= -3)         //wenn der nichtbewegbarer spieler zu weit unten ist dann geh nach oben
        {
            transform.position = new Vector3(0f, transform.position.y + 0.05f, 0f);
        }
        else
        {
            Instantiate(player, transform.position, transform.rotation);  //wenn der nicht bewegbarer spieler angekommen ist wechsel mit dem richtigen spieler
            Destroy(gameObject);
        }
    }
}
