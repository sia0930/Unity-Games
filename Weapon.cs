using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]

    //Allows only edit from UNITY edit!

    private float moveSpeed = 10f;
    public float damage = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 1f);

        //Game Object disappears after 1 sec. Only detroy will make it disappear immediately!
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        //Makes weapon/game object to shoot up/down, etc.!
    }
}
