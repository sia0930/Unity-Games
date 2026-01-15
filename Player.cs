using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    //Allows the chracter to move!

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;
    //Defining weapon!

    [SerializeField] private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    [SerializeField]
    private float burstShootInterval = 0.02f;

    private bool isBurstActive = false;
    private float burstEndTime = 0f;

    //Shoot Interval Control!

    // Update is called once per frame
    void Update()
    {
        //float horizontalInput = Input.GetAxisRaw("Horizontal");
        ////float verticalInput = Input.GetAxisRaw("Vertical");
        //Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        //transform.position += moveTo * moveSpeed * Time.deltaTime;

        //Moving up & down!

        Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= moveTo;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += moveTo;

            //Horizontal movemenmt only!
        }

        Debug.Log(Input.mousePosition);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePos);

        //Shows Mouse Point Graph!


        //float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        //transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        //Makes mousepoint as controller!

        if (GameManager.instance.isGameOver == false)
        {

            Shoot();
        }

        if (isBurstActive && Time.time >= burstEndTime)
        {
            isBurstActive = false;
        }
    }

    void Shoot()
    {

        float currentInterval = isBurstActive ? burstShootInterval : shootInterval;

        if (Time.time - lastShotTime > currentInterval)
        {

            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);

            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            //Debug.Log("Game Over");
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Coin")
        {
            //Debug.Log("Coin +1");
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }


    public void Upgrade()
    {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length - 1;
        }
    }

    public void ActivateBurst(float duration)
    {
        if (duration <= 0f)
        {
            return;
        }

        isBurstActive = true;
        burstEndTime = Mathf.Max(burstEndTime, Time.time + duration);
    }

}
