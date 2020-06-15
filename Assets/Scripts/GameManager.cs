using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public float circleRadius;
    
    public int blockAmount = 3;
    public int forageAmount = 3;
    public int bodyPartAmount = 3;

    private int totalLevelCount = 50;
    public static int currentLevel = 1;

    public GameObject spawnPoint;

    public GameObject circlePrefab;
    public GameObject blockPrefab;
    public GameObject foragePrefab;

    public UIManager UIManager;
    public MoveControl MoveControl;
    public CircleControl CircleControl;

    public List<Vector3> blockPositionList;
    private List<Vector3> foragePositionList;

    private List<GameObject> blockList = new List<GameObject>();
    private List<GameObject> forageList = new List<GameObject>();

    private void Awake()
    {

        blockPositionList = new List<Vector3>();
        foragePositionList = new List<Vector3>();

        CircleControl = FindObjectOfType<CircleControl>();
        circleRadius = circlePrefab.GetComponent<CircleCollider2D>().radius * 2 + 0.5f;

        PrepareGame();
    }

    public void PrepareGame()
    {
        GetPrepareAllBlock();
        GetPrepareAllForage();
        GetPrepareBodyPartList();
    }
    
    public void GetPrepareBodyPartList()
    {
        for (int i = 0; i < bodyPartAmount; i++)
        {
            if (i == 0)
            {
                Transform circleObjectHead = (Instantiate(circlePrefab, spawnPoint.transform.position, Quaternion.identity) as GameObject).transform;
                circleObjectHead.name = "HeadCircle";
                MoveControl.bodyParts.Add(circleObjectHead);
                circleObjectHead.SetParent(transform);
            }
            else if (i > 0)
            {
                Transform otherCircleObject = (Instantiate(circlePrefab, new Vector3(MoveControl.bodyParts.Last().position.x,
                    MoveControl.bodyParts.Last().position.y - circleRadius, MoveControl.bodyParts.Last().position.z),
                    Quaternion.identity) as GameObject).transform;

                otherCircleObject.name = "Circle_" + i.ToString();
                MoveControl.bodyParts.Add(otherCircleObject);
                otherCircleObject.SetParent(transform);
            }
        }
    }

    public void GetNewBodyPart(int amount)
    {
        int currentAmount = MoveControl.bodyParts.Count;
        for (int i = 1; i < amount + 1; i++)
        {
            Transform otherCircleObject = (Instantiate(circlePrefab, new Vector3(MoveControl.bodyParts.Last().position.x,
                     MoveControl.bodyParts.Last().position.y - circleRadius, MoveControl.bodyParts.Last().position.z),
                    Quaternion.identity) as GameObject).transform;
            int numberId = MoveControl.bodyParts.Count + i;
            otherCircleObject.name = "Circle" + numberId.ToString();
            MoveControl.bodyParts.Add(otherCircleObject);
            otherCircleObject.SetParent(transform);
        }
    }

    public void GetPrepareAllBlock()
    {
        for (int i = 0; i < blockAmount; i++)
        {
            GameObject block = Instantiate(blockPrefab, GetBlockPosition(), Quaternion.identity);
            block.name = "Block" + i;
            blockList.Add(block);
        }
    }

    public Vector3 GetBlockPosition()
    {
        int[] xValues = { -3, -2, -1, 2, 1, 0 };
        int[] yValues = { 3, 8, 13, 18 };

        int xPos = Mathf.RoundToInt(xValues[Random.Range(0, 6)]);
        int yPos = Mathf.RoundToInt(yValues[Random.Range(0, 4)]);
        Vector3 order = new Vector3((xPos + 0.5f), yPos, 0);

        if (!blockPositionList.Contains(order))
        {
            blockPositionList.Add(order);
            return order;
        }
        else
            return GetBlockPosition();
    }

    public void GetPrepareAllForage()
    {
        for (int i = 0; i < forageAmount; i++)
        {
            Vector3 foragePosition = GetForagePosition();
            GameObject forage = Instantiate(foragePrefab, foragePosition, Quaternion.identity);
            forage.name = "Forage" + i;
            forageList.Add(forage);
        }
    }

    public Vector3 GetForagePosition()
    {
        int[] xValues = { -3, -2, -1, 0, 1, 2};
        int[] yValues = { 2, 6, 12, 15};

        Vector3 order = new Vector3(Mathf.RoundToInt(xValues[Random.Range(0, 6)]) + 0.5f, Mathf.RoundToInt(yValues[Random.Range(0, 4)]), 0);
        if (!foragePositionList.Contains(order))
        {
            foragePositionList.Add(order);
            return order;
        }
        else
            return GetForagePosition();
    }

    public void ResetObjectOnScene()
    {
        MoveControl.bodyParts.Clear();
        blockPositionList.Clear();
        foragePositionList.Clear();
        for (int i = 0; i < blockList.Count; i++)
        {
            Destroy(blockList[i]);
        }
        for (int i = 0; i < forageList.Count; i++)
        {
            Destroy(forageList[i]);
        }

        ClearCircleObject();
    }

    public void ClearCircleObject()
    {
        Transform[] transform = FindObjectsOfType<Transform>();
        for (int i = 0; i < transform.Length; i++)
        {
            if (transform[i].gameObject.activeInHierarchy)
            {
                if (transform[i].CompareTag("Circle"))
                {
                    Destroy(transform[i].gameObject);
                }
            }
        }
    }

    public void UpdateLevel()
    {
        if (UIManager.isLevelUp)
        {
            if (PlayerPrefs.GetInt("LEVELNUM") != totalLevelCount)
            {
                PlayerPrefs.SetInt("LEVELNUM", (PlayerPrefs.GetInt("LEVELNUM") + 1));
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt("LEVELNUM", currentLevel);
            }
        }
        else if (!UIManager.isLevelUp)
        {
            PlayerPrefs.SetInt("LEVELNUM", (PlayerPrefs.GetInt("LEVELNUM")));
            PlayerPrefs.Save();
        }
    }
}
