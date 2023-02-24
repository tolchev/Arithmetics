using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class KeyboardLayout : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField]
    private RectTransform target;

    void Start()
    {
        int columnCount = gridLayoutGroup.constraintCount;
        int rowCount = (int)Mathf.Ceil((float)gridLayoutGroup.transform.childCount / gridLayoutGroup.constraintCount);

        // ������ ����������� �� ������ �����.
        Rect rectHeight = gridLayoutGroup.transform.GetComponent<RectTransform>().rect;
        // ������ ����������� �� ������� �������� ����������.
        Rect rectWidth = target.rect;

        float cellSizeX = (rectWidth.width - (gridLayoutGroup.spacing.x * (columnCount - 1))) / columnCount;
        float cellSizeY = (rectHeight.height - (gridLayoutGroup.spacing.y * (rowCount - 1))) / rowCount;

        gridLayoutGroup.cellSize = new Vector2(cellSizeX, cellSizeY);
    }
}