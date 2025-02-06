using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject shield;
    private float coolDown;
    public bool isOpen;
    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        coolDown = 0f;
        isOpen = false;
    }
    void Update()
    {
        if (isOpen == false)
        {
            coolDown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && coolDown <= 0f)
        {
            isOpen = true;
            coolDown = 15f;
            GameObject newShield = Instantiate(shield, transform.position, transform.rotation);
            newShield.GetComponent<BubbleShield>().player = gameObject;
        }
    }
}
