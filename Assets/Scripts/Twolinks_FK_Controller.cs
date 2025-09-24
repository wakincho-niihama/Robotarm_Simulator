using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Armlinks
{
    public string name;
    public float length;
}

public class Twolinks_FK_Controller : MonoBehaviour
{
    //robot
    private GameObject[] joint = new GameObject[2];
    private GameObject[] arm = new GameObject[2];
    private float[] ArmLength = new float[2];
    private Vector3[] angle = new Vector3[2];

    //UI
    private GameObject[] slider = new GameObject[2];
    private float[] sliderVal = new float[2];
    private GameObject[] angText = new GameObject[2];
    private GameObject[] posText = new GameObject[2];

    [Header("アームのリンク長設定")]
    public Armlinks upperarm;
    public Armlinks lowerarm;

    void Start()
    {
        //robot
        for (int i = 0; i < joint.Length; i++)
        {
            joint[i] = GameObject.Find("Joint_" + i.ToString());
            arm[i] = GameObject.Find("Arm_" + i.ToString());
            //ArmLength[i] = (arm[i].transform.localScale.x / arm[i].transform.localScale.x);get arm length
        }
        ArmLength[0] = 2.5f;
        ArmLength[1] = 3.0f;

        //UI settings
        for (int i = 0; i < joint.Length; i++)
        {
            slider[i] = GameObject.Find("Slider_" + i.ToString());
            angText[i] = GameObject.Find("Angle_" + i.ToString());
            sliderVal[i] = slider[i].GetComponent<Slider>().value;
        }
        posText[0] = GameObject.Find("Pos_X");
        posText[1] = GameObject.Find("Pos_Y");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < joint.Length; i++)
        {
            sliderVal[i] = slider[i].GetComponent<Slider>().value;
            angText[i].GetComponent<TextMeshProUGUI>().text = sliderVal[i].ToString("f2");
            angle[i].z = sliderVal[i];
            joint[i].transform.localEulerAngles = angle[i];

        }
        float px = ArmLength[0] * Mathf.Cos(angle[0].z * Mathf.Deg2Rad) + ArmLength[1] * Mathf.Cos((angle[0].z + angle[1].z) * Mathf.Deg2Rad);
        float py = ArmLength[0] * Mathf.Sin(angle[0].z * Mathf.Deg2Rad) + ArmLength[1] * Mathf.Sin((angle[0].z + angle[1].z) * Mathf.Deg2Rad);

        posText[0].GetComponent<TextMeshProUGUI>().text = px.ToString("f2");
        posText[1].GetComponent<TextMeshProUGUI>().text = py.ToString("f2");
    }
}
