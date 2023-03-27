using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    public Transform face;
    float speed = 10;
    float rotationX = 0;
    float lookSpeed = 2;
    float lookXLimit = 45;
    float gravity = -9.81f;
    float velocity = 0;
    float jumpForce = 10;
    float timeBetweenShots = 0.5F;
    float nextShot = 0.0F;
    public float distToGround = 1f;
    public bool isGrounded = false;
    AudioSource audioSource;
    //public Text LogCollsiionEnter;

    public GameObject bullet;
    GameObject[] bullets;
    int bulletCounter = 0;

    //Transform muzzle;

    [SerializeField] private RectTransform crosshair;
    public Image[] crosshairImages;
    public float regularFOV = 60f;
    public float aimingFOV = 70f;
    public float currentFOV = 60f;
    public float aimingSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        bullets = new GameObject[100];
        //muzzle = GameObject.Find("muzzle").transform;
        for (int i = 0; i < 100; i++) {
            bullets[i] = Instantiate(bullet);
            bullets[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();
        velocity += gravity * Time.deltaTime;

        float jumpVal = Input.GetAxis("Jump");

        if(isGrounded && jumpVal == 1){
            velocity = jumpForce;
        }

        controller.Move(new Vector3(0, velocity, 0) * Time.deltaTime);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);


        if (Input.GetAxis("Fire1") == 1 && Time.time > nextShot)
        {
            nextShot = Time.time + timeBetweenShots;
            bullets[bulletCounter].SetActive(true);
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            (GameObject.Find("gun")).GetComponent<gunBullets>().shoot();
            bulletCounter++;
        }

        float speedX = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float speedZ = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        Vector3 moveDirection = (forward * speedX) + (right * speedZ);

        controller.Move(moveDirection);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        face.localRotation = Quaternion.Euler(rotationX, 0, 0);

        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        if (crosshair.GetComponent<Crosshair>().Aiming)
            currentFOV = Mathf.Lerp(currentFOV, aimingFOV, Time.deltaTime * aimingSpeed);
        else
            currentFOV = Mathf.Lerp(currentFOV, regularFOV, Time.deltaTime * aimingSpeed);

        (GameObject.Find("Main Camera")).GetComponentInParent<Camera>().fieldOfView = currentFOV;
        //(GameObject.Find("Main Camera")).GetComponent<Camera>().fieldOfView = currentFOV;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 200f))
            if ((hit.transform.gameObject.name == "EnemyObject-1") || (hit.transform.gameObject.name == "EnemyObject-2") || (hit.transform.gameObject.name == "EnemyObject-3"))
                foreach (Image crosshairImage in crosshairImages)
                    crosshairImage.color = new Color(1f, 0f, 0f, 1f);
            else
                foreach (Image crosshairImage in crosshairImages)
                    crosshairImage.color = new Color(0.8f, 0.8f, 0.8f, 1f);
        else
            foreach (Image crosshairImage in crosshairImages)
                crosshairImage.color = new Color(0.8f, 0.8f, 0.8f, 1f);

}

    void groundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            //Debug.Log("Grounded");
            isGrounded = true;
        }
        else {
            //Debug.Log("Not Grounded");
            isGrounded = false;
        }
    }
}