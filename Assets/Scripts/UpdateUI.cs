using System;
using TMPro;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rpmMotorText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI rpmWheelsText;
    [SerializeField] private TextMeshProUGUI upTo100Text;
    [SerializeField] private SimpleCarController simpleCarController;

    private float _startTimeForUpTo100;
    private float _endTimeForUpTo100;

    private void Update()
    {
        rpmMotorText.text = "Motor RPM : " + Mathf.RoundToInt(simpleCarController.RpmMotor);
        rpmWheelsText.text = "Wheels RPM : " + simpleCarController.wheelCollidersRpm.GetValue(0);
        upTo100Text.text = "Up to 100 : " + Math.Round(UpTo100(Mathf.RoundToInt(simpleCarController.Speed)), 3);
        speedText.text = "Speed : " + Mathf.RoundToInt(simpleCarController.Speed);
    }

    private float UpTo100(float speed)
    {
        if (Mathf.RoundToInt(speed) == 0) _startTimeForUpTo100 = Time.time;
        if (Mathf.RoundToInt(speed) == 100) _endTimeForUpTo100 = Time.time;
        if (Mathf.RoundToInt(speed) > 100) return _endTimeForUpTo100 -_startTimeForUpTo100;
        return 0;
    }
}