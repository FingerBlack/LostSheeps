using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    // Start is called before the first frame update
    
    public class Bullet : MonoBehaviour
    {   
        public int damage = 10;
        public GameObject Enemy;
        public float movementSpeed=0.15f;
        Vector3 direction;
        void Start()
        {
            //CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {   
            Debug.Log("Ontrigger started");
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("collided");
                //Get the enemy's HP component and decrease it by the bullet's damage
                int enemyHP = collision.GetComponent<Enemy>().HP;
                enemyHP -= damage; //assuming the bullet's damage is 10

                //Check if the enemy's HP is less than or equal to 0
                if (enemyHP <= 0)
                {
                    //Destroy the enemy object
                    Destroy(collision.gameObject);
                    Debug.Log("destroied");
                }
            }
            Destroy(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            //transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            direction = (Enemy.transform.position - transform.position).normalized;
            transform.position += direction * Time.deltaTime * movementSpeed;
        }
    }
