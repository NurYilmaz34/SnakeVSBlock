using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public List<Transform> bodyParts; 

    public float ratio;
    public float speed = 2.8f;
    public float distance = 0.5f;
    public float rotationSpeed = 80f;

    public float lerpTimeX = 0.0805f;
    public float lerpTimeY = 0.0805f;

    private float deltaMousePosition;
    
    private Vector3 mouseFirstPosition;
    private Vector3 mouseCurrentPosition;

    private Transform currentBodyPart;
    private Transform targetBodyPart;

    private bool isPressed = false;

    public UIManager UIManager;
    public GameManager GameManager;
    public CircleControl CircleControl;

    private void Awake()
    {
        bodyParts = new List<Transform>();
        ratio = Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    void Update()
    {
        if (bodyParts.Count > 0 && UIManager.isActiveGame)
        {
            bodyParts[0].Translate(Vector2.up * speed * Time.smoothDeltaTime);
        }
        
        if (bodyParts[0].position.x > ratio)
        {
            bodyParts[0].position = new Vector3(ratio - 0.5f, bodyParts[0].position.y, bodyParts[0].position.z);
        }
        else if (bodyParts[0].position.x < -ratio)
        {
            bodyParts[0].position = new Vector3(-ratio + 0.5f, bodyParts[0].position.y, bodyParts[0].position.z);
        }

        TouchControl();

        FollowHeadBodyPart();
    }

    private void TouchControl()
    {
        if (UIManager.isActiveGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPressed = false;
                mouseFirstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                isPressed = true;
                mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isPressed = false;
            }

            HeadBodyPartMoveControl();
        }
    }

    private void HeadBodyPartMoveControl()
    {
        if (isPressed)
        {
            if (bodyParts.Count > 0 && Mathf.Abs(bodyParts[0].position.x) < ratio)
            {
                deltaMousePosition = Mathf.Abs(mouseCurrentPosition.x - mouseFirstPosition.x);
                float sign = Mathf.Sign(mouseFirstPosition.x - mouseCurrentPosition.x);
                bodyParts[0].Translate(Vector3.right * rotationSpeed * Time.deltaTime * deltaMousePosition * -sign);
                mouseFirstPosition = mouseCurrentPosition;
            }
        }
    }

    private void FollowHeadBodyPart()
    {
        for (int i = 1; i < bodyParts.Count; i++)
        {
            if (UIManager.isActiveGame)
            {
                currentBodyPart = bodyParts[i];
                targetBodyPart = bodyParts[i - 1];

                distance = Vector3.Distance(currentBodyPart.position, targetBodyPart.position);

                Vector3 newPosition = targetBodyPart.position;
                Vector3 currentPosition = currentBodyPart.position;

                if (!isPressed)
                {
                    lerpTimeX = lerpTimeY = 0.0805f;
                }
                else if (isPressed && deltaMousePosition > 0.08f)
                {
                    lerpTimeX = lerpTimeY = 0.13f;
                }
                currentPosition.x = Mathf.Lerp(currentPosition.x, newPosition.x, lerpTimeX);
                currentPosition.y = Mathf.Lerp(currentPosition.y, newPosition.y, lerpTimeY);
            
                float restrict = Mathf.Clamp(distance, 0.8f, 0.9f);

                currentBodyPart.position = new Vector3(currentPosition.x, currentPosition.y, bodyParts[0].position.z);
            }
        }
    }
}

