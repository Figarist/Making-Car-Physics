using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    //Input keyboard
    public static float verticalAxle;
    public static float horizontalAxle;
    public static bool isBraking;
    //public static bool isRestart;
    public static bool isKinematic;

    [SerializeField] private KeyCode brake;
    [SerializeField] private KeyCode resetPhysix;

    private void Update()
    {
        
        verticalAxle = Input.GetAxis("Vertical");
        horizontalAxle = Input.GetAxis("Horizontal");
        isBraking = Input.GetKey(brake);
        //isRestart = Input.GetKeyDown(KeyCode.R);
        isKinematic = Input.GetKey(resetPhysix);
    }
}