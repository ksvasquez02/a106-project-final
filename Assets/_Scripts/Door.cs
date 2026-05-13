using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    //public bool open = false;
    public int openState = 0;
    private bool animating = false;
    public float speed = 90f;
    public float targetAngle = 90f;
    public bool isDouble = false;
    private float[] startingAngles;
    private float currentAngle = 0f;

    public float closeTimeout = 1f;
    private float currentTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.childCount > 0)
        {
            startingAngles = new float[transform.childCount];
            for (int i = 0; i < startingAngles.Length; i++)
            {
                startingAngles[i] = transform.GetChild(i).eulerAngles.y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animating && openState != 0)
        {
            // Rotate door by speed degrees per second
            currentAngle = currentAngle + speed * Time.deltaTime * openState;

            // Door has opened
            if (openState > 0 && Mathf.Abs(currentAngle) >= Mathf.Abs(targetAngle) && Mathf.Sign(currentAngle) == Mathf.Sign(targetAngle))
            {
                //Debug.Log("OPENED");
                animating = false;
                openState = 0;
                currentAngle = targetAngle;
            }
            // Door has closed
            if (openState < 0 && (Mathf.Sign(targetAngle) > 0 ? currentAngle <= 0 : currentAngle >= 0))
            {
                //Debug.Log("CLOSED");
                animating = false;
                openState = 0;
                currentAngle = 0;
            }

            // Rotate door
            foreach (Transform child in transform)
            {
                int index = child.GetSiblingIndex();
                float start = startingAngles[index];
                child.eulerAngles = new Vector3(0, start + currentAngle * (index > 0 && isDouble ? -1 : 1), 0);
            }
        }
        // Time still remaining on closing timeout
        if (currentTime > 0)
        {
            // Update time
            currentTime -= Time.deltaTime;

            // Trigger if timed out
            if (currentTime <= 0)
            {
                currentTime = 0;
                animating = true;
                openState = -1;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        animating = true;
        openState = 1;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        currentTime = closeTimeout; // Delay closing
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (!other.gameObject.CompareTag("Player")) return;
    //    currentTime = closeTimeout; // Delay closing
    //}
}
