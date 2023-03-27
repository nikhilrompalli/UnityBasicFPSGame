using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 200f))
        {
            Debug.Log("target - " + hit.transform.gameObject.name);
            //if (hit.transform.gameObject.name == "EnemyObject-1") ;
            string hitObjName = hit.transform.gameObject.name;
            Debug.Log("Enemy Object Name - " + hitObjName);
            string[] enemyObjNameArr = hitObjName.Split('-');
            string enemyName = "Enemy-" + enemyObjNameArr[1];
            Debug.Log("Enemy Name - " + enemyName);
            if (hitObjName.Contains("EnemyObject"))
            {
                AudioSource source = GetComponent<AudioSource>();
                source.Play();
                (GameObject.Find(enemyName)).GetComponent<EnemyMovement>().TakeDamage(1);
            }
        }
    }
}
