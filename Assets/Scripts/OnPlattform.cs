using UnityEngine;

public class OnPlattform : MonoBehaviour
{
    
    
    [SerializeField] private Transform platform;
    [SerializeField] private GameObject hijo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y > platform.position.y +  0.2f)
            {
                other.transform.SetParent(platform);
                hijo.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            hijo.SetActive(false);
        }
    }
}
