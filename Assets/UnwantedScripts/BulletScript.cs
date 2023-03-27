using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "EnemyObject")
        {
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            (GameObject.Find("Enemy")).GetComponent<EnemyMovement>().TakeDamage(1);
        }
    }

    //void shoot()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, 200f))
    //    {
    //        Debug.Log("target - " + hit.transform.gameObject.name);
    //        if (hit.transform.gameObject.name == "EnemyObject-1")
    //        {
    //            AudioSource source = GetComponent<AudioSource>();
    //            source.Play();
    //            (GameObject.Find("Enemy-1")).GetComponent<EnemyMovement>().TakeDamage(1);
    //        }
    //    }
    //}
}
