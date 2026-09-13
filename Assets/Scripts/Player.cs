using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [Tooltip("Скорость плавного поворота персонажа")]
    [SerializeField] private float rotationSpeed = 720f;

    private void Update()
    {
        if (Keyboard.current == null) return;

        Vector3 moveDirection = Vector3.zero;

        // Считываем ввод с клавиатуры
        if (Keyboard.current.wKey.isPressed)
            moveDirection += Vector3.forward;
        if (Keyboard.current.sKey.isPressed)
            moveDirection += Vector3.back;
        if (Keyboard.current.aKey.isPressed)
            moveDirection += Vector3.left;
        if (Keyboard.current.dKey.isPressed)
            moveDirection += Vector3.right;

        // Если игрок куда-то движется (вектор не равен нулю)
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();

            // 1. Плавный поворот в сторону движения
            // Рассчитываем, куда персонаж должен смотреть
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Плавно вращаем персонажа от текущего поворота к целевому
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            // 2. Движение вперед (теперь можно двигать объект просто через Vector3.forward в локальных координатах)
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
