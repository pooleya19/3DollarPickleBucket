using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D body;
    public float bulletSpeed = 10.0f;
    public float lifetime = 1.0f;

    public GameObject explosion_effect;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HAVE A TASTE OF MY FIREBALL!");
    
        body = GetComponent<Rigidbody2D>();

        Vector2 mouse = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 fireballPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 cursorDirection = worldPosition - fireballPosition;
        cursorDirection.Normalize();

        body.AddForce(cursorDirection * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0) {
            Instantiate(explosion_effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Instantiate(explosion_effect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
