using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    private Player _player;

    private bool isfacingRight;

    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();   
        //transform.Rotate(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;

        //transform.position += new Vector3(direction, 0, 0) * Time.deltaTime * speed;


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Fire Trigger with " + other.gameObject.name);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _player.currentStamina += 25f;
            _player.staminaBar.SetStamina(_player.currentStamina);
        }
        else if(collision.gameObject.tag == "Ground")
        {
            Debug.Log("Fire Collision with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
