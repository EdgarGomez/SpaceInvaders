using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyController enemyController;
    public GameObject turboFireLeft;
    public GameObject turboFireRight;

    void Start()
    {
        turboFireLeft.SetActive(false);
        turboFireRight.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            enemyController.HandleBorderCollision();
        }
    }

    public void ShowTurboFire()
    {
        turboFireLeft.SetActive(true);
        turboFireRight.SetActive(true);
    }

    public void HideTurboFire()
    {
        turboFireLeft.SetActive(false);
        turboFireRight.SetActive(false);

    }
}
