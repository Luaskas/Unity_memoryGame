using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory_Card : MonoBehaviour
{
    public float targetHeight = 0.05f;
    public float targetRotation = -90f;
    public int ID;

    private Vector3 testVector = new Vector3(0, 0, 0);

    public void OnMouseDown()
    {
        
        if (FindObjectOfType<Game_Manager>().clickCounter == 0)
        {
            FindObjectOfType<Game_Manager>().ClickCard(this);
        }
        else if (FindObjectOfType<Game_Manager>().clickCounter == 1)
        {
            FindObjectOfType<Game_Manager>().ClickCard(this);
        }
    }
    private void Awake()
    {
        for (int i = 0; i < FindObjectOfType<Game_Manager>().position.Length; i++)
        {
            if (FindObjectOfType<Game_Manager>().position[i] == testVector)
            {
                FindObjectOfType<Game_Manager>().position[i] = transform.position;
                Debug.Log(name + "-Position saved at Index " + i + " with Vector " + transform.position);
                break;
            }
        }        
    }
    private void Start()
    {
        FindObjectOfType<Game_Manager>().SetNewPosition(this);
    }

    private void Update()
    {
        // Move Card up/down after clickOnCard
        float heightValue = Mathf.MoveTowards(transform.position.y, targetHeight, 0.5f * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, heightValue, transform.position.z);

        // Rotate card after clikcOnCard -> GameManager modifies targetRotation after first Click and second Click
        Quaternion rotationValue = Quaternion.Euler(targetRotation, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationValue, 10 * Time.deltaTime);
    }
}
