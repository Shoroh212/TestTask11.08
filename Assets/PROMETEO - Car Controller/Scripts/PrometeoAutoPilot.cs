using UnityEngine;

public class PrometeoAutoPilot : MonoBehaviour
{
    [Header("CAR")]
    [Tooltip("Перетащи сюда объект машины с компонентом PrometeoCarController")]
    public PrometeoCarController carController;


    [Header("CAMERAS")]
    [Tooltip("Камера, которая используется во время автопилота")]
    public Camera autoPilotCamera;

    [Tooltip("Камера игрока, находящаяся за машиной")]
    public Camera manualCamera;


    [Header("CONTROLS")]
    [Tooltip("Клавиша переключения автопилота")]
    public KeyCode toggleKey = KeyCode.Space;


    [Header("START SETTINGS")]
    [Tooltip("Запускать игру с включенным автопилотом")]
    public bool startWithAutoPilot = true;


    private void Start()
    {
        // Если машина не указана вручную,
        // пытаемся найти её на этом объекте.
        if (carController == null)
        {
            carController = GetComponent<PrometeoCarController>();
        }

        // Устанавливаем начальный режим.
        SetAutoPilot(startWithAutoPilot);
    }


    private void Update()
    {
        // Переключение автопилота по Space.
        if (Input.GetKeyDown(toggleKey))
        {
            SetAutoPilot(!carController.autoPilot);
        }
    }


    private void SetAutoPilot(bool enabled)
    {
        if (carController == null)
        {
            Debug.LogError(
                "PrometeoAutoPilot: PrometeoCarController не найден!"
            );

            return;
        }


        // Меняем состояние автопилота.
        carController.autoPilot = enabled;


        // ==========================================
        // КАМЕРЫ
        // ==========================================

        if (autoPilotCamera != null)
        {
            autoPilotCamera.enabled = enabled;
        }

        if (manualCamera != null)
        {
            manualCamera.enabled = !enabled;
        }


        // ==========================================
        // ЛОГ
        // ==========================================

        if (enabled)
        {
            Debug.Log("AUTOPILOT ON");
        }
        else
        {
            Debug.Log("AUTOPILOT OFF - MANUAL CONTROL");
        }
    }
}
