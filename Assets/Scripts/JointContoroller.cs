using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JointContoroller : MonoBehaviour
{
    //robot
    private GameObject[] Joint = new GameObject[2];
    private GameObject[] Arm = new GameObject[2];
    private float[] ArmLength = new float[2];
    private Vector3[] Angle = new Vector3[2];

    //UI
    private GameObject[] Slider = new GameObject[2];
    private float[] Slider_Val = new float[2];
    private GameObject[] AngText = new GameObject[2];
    private GameObject[] PosText = new GameObject[2];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //robot
        for (int i = 0; i < Joint.Length; i++)
        {
            Joint[i] = GameObject.Find("Joint_" + i.ToString());
            Arm[i] = GameObject.Find("Arm_" + i.ToString());
            ArmLength[i] = Arm[i].transform.localScale.x;//get Arm length
        }
        //UI settings
        for (int i = 0; i < Joint.Length; i++)
        {
            Slider[i] = GameObject.Find("Slider_" + i.ToString());
            AngText[i] = GameObject.Find("Angle_" + i.ToString());
            Slider_Val[i] = Slider[i].GetComponent<Slider>().value;
        }
        PosText[0] = GameObject.Find("Pos_X");
        PosText[1] = GameObject.Find("Pos_Y");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Joint.Length; i++)
        {
            Slider_Val[i] = Slider[i].GetComponent<Slider>().value;
            AngText[i].GetComponent<TextMeshProUGUI>().text = Slider_Val[i].ToString("f2");
            Angle[i].z = Slider_Val[i];
            Joint[i].transform.localEulerAngles = Angle[i];
        }
        float px = ArmLength[0] * Mathf.Cos(Angle[0].z * Mathf.Deg2Rad) + ArmLength[1] * Mathf.Cos((Angle[0].z + Angle[1].z) * Mathf.Deg2Rad);
        float py = ArmLength[0] * Mathf.Sin(Angle[0].z * Mathf.Deg2Rad) + ArmLength[1] * Mathf.Sin((Angle[0].z + Angle[1].z) * Mathf.Deg2Rad);

        PosText[0].GetComponent<TextMeshProUGUI>().text = px.ToString("f2");
        PosText[1].GetComponent<TextMeshProUGUI>().text = py.ToString("f2");
    }
}
