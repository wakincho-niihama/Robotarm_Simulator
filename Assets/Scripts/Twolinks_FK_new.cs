using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Twolinks_FK_new : MonoBehaviour
{
    //robot
    private GameObject[] joint = new GameObject[5];
    private GameObject[] arm = new GameObject[2];
    private float[] ArmLength = new float[2];
    private Vector3[] angle = new Vector3[5];

    //UI
    private GameObject[] slider = new GameObject[5];
    private float[] sliderVal = new float[5];
    private GameObject[] angText = new GameObject[5];
    private GameObject[] posText = new GameObject[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //robot
        for (int i = 0; i < joint.Length; i++)
        {
            joint[i] = GameObject.Find("Joint_" + i.ToString());
            if (i <= 1) arm[i] = GameObject.Find("Arm_" + i.ToString());
            //ArmLength[i] = (arm[i].transform.localScale.x / arm[i].transform.localScale.x);get arm length
        }
        ArmLength[0] = 2.5f;
        ArmLength[1] = 3.0f;

        //UI settings
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i] = GameObject.Find("Slider_" + i.ToString());
            angText[i] = GameObject.Find("Angle_" + i.ToString());
            sliderVal[i] = slider[i].GetComponent<Slider>().value;
        }
        posText[0] = GameObject.Find("Pos_X");
        posText[1] = GameObject.Find("Pos_Y");
        posText[2] = GameObject.Find("Pos_Z");
    }

    // Update is called once per frame
    void Update()
    {
        angle[0].z = sliderVal[0];
        angle[1].x = sliderVal[1];
        angle[2].y = sliderVal[2];
        angle[3].z = sliderVal[3];
        angle[4].y = sliderVal[4];

        for (int i = 0; i < slider.Length; i++)
        {
            sliderVal[i] = slider[i].GetComponent<Slider>().value;
            angText[i].GetComponent<TextMeshProUGUI>().text = sliderVal[i].ToString("f2");
            //angle[i].z = sliderVal[i];
            joint[i].transform.localEulerAngles = angle[i];
        }
        

        float px = ArmLength[0] * Mathf.Cos(angle[0].z * Mathf.Deg2Rad) + ArmLength[1] * Mathf.Cos((angle[0].z + angle[1].z) * Mathf.Deg2Rad);
        float py = ArmLength[0] * Mathf.Sin(angle[0].z * Mathf.Deg2Rad) + ArmLength[1] * Mathf.Sin((angle[0].z + angle[1].z) * Mathf.Deg2Rad);
        float pz = px * Mathf.Sin(angle[0].z);

        posText[0].GetComponent<TextMeshProUGUI>().text = px.ToString("f2");
        posText[1].GetComponent<TextMeshProUGUI>().text = py.ToString("f2");
        posText[2].GetComponent<TextMeshProUGUI>().text = pz.ToString("f2");
    }
}
