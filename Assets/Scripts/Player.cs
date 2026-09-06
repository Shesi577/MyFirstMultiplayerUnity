using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed = 5;

    private void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
            moveDirection += Vector3.forward;
        if (Input.GetKeyDown(KeyCode.S))
            moveDirection += Vector3.back;
        if (Input.GetKeyDown(KeyCode.A))
            moveDirection += Vector3.left;
        if (Input.GetKeyDown(KeyCode.D))
            moveDirection += Vector3.right;

        moveDirection.Normalize();
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }
}