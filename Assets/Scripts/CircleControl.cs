using UnityEngine;

public class CircleControl : MonoBehaviour
{
    public int forageAmount;

    public UIManager UIManager;
    public GameManager GameManager;
    public MoveControl MoveControl;

    public GameObject circleDestroyParticleEffect;

    private void Awake()
    {
        MoveControl = FindObjectOfType<MoveControl>();
        GameManager = FindObjectOfType<GameManager>();
        UIManager = FindObjectOfType<UIManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Block"))
        {
            if (MoveControl.bodyParts.Count != 1)
            {
                MoveControl.bodyParts.Remove(gameObject.transform);
                Destroy(gameObject);
                Destroy(Instantiate(circleDestroyParticleEffect, gameObject.transform.position, Quaternion.identity), 0.2f);
            }
            else if (MoveControl.bodyParts.Count == 1)
            {
                UIManager.ShowContinueGame();
                UIManager.isActiveGame = false;
                UIManager.isLevelUp = false;
            }
        }
        if (collision.collider.CompareTag("Forage"))
        {
            forageAmount = collision.collider.GetComponent<ForageValueControler>().forageValue;
            Destroy(collision.gameObject);
            GameManager.GetNewBodyPart(forageAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishLine"))
        {
            UIManager.ShowLevelCompletedText();
            if (MoveControl.bodyParts.Count != 1)
            {
                UIManager.isLevelUp = true;
                MoveControl.bodyParts.RemoveAt(0);
                Destroy(gameObject);
                Destroy(Instantiate(circleDestroyParticleEffect, gameObject.transform.position, Quaternion.identity), 0.2f);
            }
            else if (MoveControl.bodyParts.Count == 1)
            {
                UIManager.isActiveGame = false;
                UIManager.NextLevelButton.SetActive(true);
            }
        }
    }
}
