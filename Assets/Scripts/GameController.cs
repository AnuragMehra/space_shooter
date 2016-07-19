using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{

    // The values we'll be printing
    private int score = 0;
    private int waveNumber = 0;

    // The actual GUI text
    public Text scoreText;
    public Text waveText;

    // Our enemy to spawn
    public Transform enemy;

    // We want to delay our code at certain times
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Coroutine used to spawn enemies
    IEnumerator SpawnEnemies()
    {
        // Give the player time before we start the game
        yield return new WaitForSeconds(timeBeforeSpawning);

        // After timeBeforeSpawning has elapsed, we will enter this loop
        while (true)
        {
            // Don't spawn anything new until all the previous
            // wave's enemies are dead
            if (currentNumberOfEnemies <= 0)
            {
                waveNumber++;
                waveText.text = "Wave: " + waveNumber;
                float randDirection;
                float randDistance;

                //Spawn 10 enemies in a random position
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    // We want the enemies to be off screen
                    // (Random.Range gives us a number between the 
                    // first and second parameter)
                    randDistance = Random.Range(10, 25);

                    // Enemies can come from any direction
                    randDirection = Random.Range(0, 360);

                    // Using the distance and direction we set the position
                    float posX = this.transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
                    float posY = this.transform.position.y + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);

                    // Spawn the enemy and increment the number of 
                    // enemies spawned 
                    // (Instantiate Makes a clone of the first parameter 
                    // and places it at the second with a rotation of 
                    // the third.)
                    Instantiate(enemy, new Vector3(posX, posY, 0), this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            // How much time to wait before checking if we need to 
            // spawn another wave
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    // Allows classes outside of GameController to say when we killed 
    // an enemy.
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = "Score: " + score;
    }
}
