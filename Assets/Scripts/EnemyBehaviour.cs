using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    // How many times should I be hit before I die
    public int health = 2;

    // Explosion prefab to play, when enemy dies
    public Transform explosion;


    AudioSource audio;
    public AudioClip hitSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        // Uncomment this line to check for collision
        //Debug.Log("Hit"+ theCollision.gameObject.name);

        // this line looks for "laser" in the names of 
        // anything collided.
        if (theCollision.gameObject.name.Contains("laser"))
        {
            LaserBehaviour laser = theCollision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(theCollision.gameObject);
            audio.PlayOneShot(hitSound);
        }

        if (health <= 0)
        {
            Destroy(this.gameObject);
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, transform.position, transform.rotation)).gameObject;
                exploder.GetComponent<AudioSource>().Play();
                Destroy(exploder, 2.0f);
            }
            GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("GameController") as GameController;
            controller.KilledEnemy();
            controller.IncreaseScore(10);
        }
    }
}
