using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int moveSpeed = 7;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Do something
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Do something
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Do something
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // Do something
        }


    }

}
