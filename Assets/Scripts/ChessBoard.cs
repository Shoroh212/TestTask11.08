using UnityEditor.SceneManagement;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [Header("Размер доски")]
    public int width = 8;
    public int height = 8;

    [Header("Размер одной клетки")]
    public float cellSize = 1f;

    [Header("Цвета")]
    public Color lightColor = Color.white;
    public Color darkColor = Color.black;

    

    void Start()
    {
        CreateBoard();
        gameObject.AddComponent<BoxCollider>();

    }

    void CreateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                CreateCell(x, z);
            }
        }
    }

    void CreateCell(int x, int z)
    {
        // Создаём квадрат
        GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cell.name = "Cell_" + x + "_" + z;

        // Позиция клетки
        cell.transform.position = new Vector3(
            x * cellSize,
            0,
            z * cellSize
        );

        // Размер клетки
        cell.transform.localScale = new Vector3(
            cellSize,
            0.1f,
            cellSize
        );

        // Создаём материал
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        // Чередуем цвета
        if ((x + z) % 2 == 0)
        {
            material.color = lightColor;
        }
        else
        {
            material.color = darkColor;
        }

        // Применяем материал
        Renderer renderer = cell.GetComponent<Renderer>();
        renderer.material = material;

        // Делаем клетки дочерними объектами
        cell.transform.SetParent(transform);
    }
}
