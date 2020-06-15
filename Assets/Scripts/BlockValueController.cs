using UnityEngine;
using TMPro;

public class BlockValueController : MonoBehaviour
{
    public int amountCollision;
    public TextMeshPro blockValueText;

    public MoveControl MoveControl;
    public GameManager GameManager;

    private void Awake()
    {
        MoveControl = FindObjectOfType<MoveControl>();
        GameManager = FindObjectOfType<GameManager>();
        blockValueText = GetComponentInChildren<TextMeshPro>();

        SetValue();
    }

    public void SetValue()
    {
        int counter = 1;

        for (int i = 0; i < GameManager.blockPositionList.Count; i++)
        {
            if (GameManager.blockPositionList[i].y == 3 && counter > 0)
            {
                amountCollision = Random.Range(1, 3);
                counter--;
            }
            else
            {
                amountCollision = Random.Range(1, 8);
            }
            SetTextBlockValue(amountCollision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Circle"))
        {
            amountCollision--;
            SetTextBlockValue(amountCollision);
            if (amountCollision == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTextBlockValue(int blockValue)
    {
        blockValueText.SetText(blockValue.ToString());
    }
}
