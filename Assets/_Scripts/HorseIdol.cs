using UnityEngine;

public class HorseIdol : MonoBehaviour
{
    public bool isActive = true;
    public int toState = 1;
    public GameManager manager;
    public GameObject horseModel;
    public AudioSource audioSrc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Advance()
    {
        manager.ChangeGameState(toState);
        if (horseModel != null) horseModel.SetActive(false);
        if (audioSrc != null) audioSrc.Play();
        isActive = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (!other.gameObject.CompareTag("Player")) return;

        if (manager == null) manager = other.GetComponent<GameManager>();
        if (manager.gameState != toState - 1) return;
        Advance();
    }
}
