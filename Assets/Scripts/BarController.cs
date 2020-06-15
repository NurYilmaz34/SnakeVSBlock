using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarController : MonoBehaviour
{
    private float distance;
    private float totalDistance;

    public Image colorBarImage;
    public GameObject finishLine;

    public MoveControl MoveControl;
    public GameManager GameManager;

    public static BarController bar = null;
    
    private void Awake()
    {
        if (bar == null)
        {
            bar = this;
        }
        totalDistance = Mathf.Abs(GameManager.spawnPoint.transform.position.y - finishLine.transform.position.y) - 1;
    }

    private void Start()
    {
        ResetBar();
    }

    private void Update()
    {
        SetShowBar();
    }

    public void ResetBar()
    {
        colorBarImage.fillAmount = 0;
    }

    public void FullBar()
    {
        colorBarImage.fillAmount = 1;
    }

    public void SetShowBar()
    {
        distance = Mathf.Abs(MoveControl.bodyParts[0].position.y - finishLine.transform.position.y) - GameManager.circleRadius * 0.5f /*- 1*/;
        float fillAmount = (totalDistance - distance) / totalDistance;

        if (fillAmount < 1)
        {
            colorBarImage.fillAmount = fillAmount;
        }
        else if (fillAmount == 1 || fillAmount > 1)
        {
            FullBar();
        }
    }
}
