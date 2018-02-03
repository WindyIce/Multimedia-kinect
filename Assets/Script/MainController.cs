using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

    [SerializeField] private TextMesh textMesh;
    [SerializeField] private TextMesh gestureMesh;

    private int score;

    private double timeFromInit;
    private int round;
    private const double BPM = (60.0/71.0)*2.0;

    public GameObject Hip_Center;
    public GameObject Spine;
    public GameObject Shoulder_Center;
    public GameObject Head;
    public GameObject Collar_Left;
    public GameObject Shoulder_Left;
    public GameObject Elbow_Left;
    public GameObject Wrist_Left;
    public GameObject Hand_Left;
    public GameObject Collar_Right;
    public GameObject Shoulder_Right;
    public GameObject Elbow_Right;
    public GameObject Wrist_Right;
    public GameObject Hand_Right;
    public GameObject Hip_Override;
    public GameObject Hip_Left;
    public GameObject Knee_Left;
    public GameObject Ankle_Left;
    public GameObject Foot_Left;
    public GameObject Hip_Right;
    public GameObject Knee_Right;
    public GameObject Ankle_Right;
    public GameObject Foot_Right;

    // Use this for initialization
    void Start () {
        KinectModelControllerV2 kinectController = GetComponent<KinectModelControllerV2>();

        score = 0;
        round = 0;
        
        Hip_Center = kinectController.Hip_Center;
        Spine = kinectController.Spine;
        Shoulder_Center = kinectController.Shoulder_Center;
        Head = kinectController.Head;
        Collar_Left = kinectController.Collar_Left;
        Shoulder_Left = kinectController.Shoulder_Left;
        Elbow_Left = kinectController.Elbow_Left;
        Wrist_Left = kinectController.Wrist_Left;
        Hand_Left = kinectController.Hand_Left;
        Collar_Right = kinectController.Collar_Right;
        Shoulder_Right = kinectController.Shoulder_Right;
        Elbow_Right = kinectController.Elbow_Right;
        Wrist_Right = kinectController.Wrist_Right;
        Hand_Right = kinectController.Hand_Right;
        Hip_Override = kinectController.Hip_Override;
        Hip_Left = kinectController.Hip_Left;
        Knee_Left = kinectController.Knee_Left;
        Ankle_Left = kinectController.Ankle_Left;
        Foot_Left = kinectController.Foot_Left;
        Hip_Right = kinectController.Hip_Right;
        Knee_Right = kinectController.Knee_Right;
        Ankle_Right = kinectController.Ankle_Right;
        Foot_Right = kinectController.Foot_Right;
        timeFromInit = 0;

        
	}
	
	// Update is called once per frame
	void Update () {
        timeFromInit += Time.deltaTime;
        Vector3 headPos = Head.transform.position;
        Vector3 rightHandPos = Hand_Right.transform.position;
        Vector3 leftHandPos = Hand_Left.transform.position;
        Vector3 rightElbowPos = Elbow_Right.transform.position;
        Vector3 leftElbowPos = Elbow_Left.transform.position;
        Vector3 leftHipPos = Hip_Left.transform.position;
        Vector3 rightHipPos = Hip_Right.transform.position;
        setGestureText(round2Ges());
        // 0 左  1  上  2  右  3  下
        if (timeFromInit > (double)round * BPM)
        {
            Debug.Log("round*BPM: " + (double)round*BPM);
            int gesture = round2Ges();
            if (gesture == 0) {
                // 左
                if (leftHandPos.x < leftElbowPos.x||
                    rightHandPos.x<headPos.x) { hit(); }
            }
            if (gesture == 1)
            {
                // 上
                if (leftHandPos.y > headPos.y||
                    rightHandPos.y > headPos.y) { hit(); }
            }
            if (gesture == 2)
            {
                // 右
                if(rightHandPos.x>rightElbowPos.x||
                    leftHandPos.x > headPos.x) { hit(); }
            }
            if (gesture == 3)
            {
                // 下
                if(rightHandPos.y<rightHipPos.y||
                    leftHandPos.y < leftHipPos.y) { hit(); }
            }
        }
    }

    // 命中
    private void hit()
    {
        ++score;
        textMesh.text = "Score: "+score + "";
        round = ((int)(timeFromInit/BPM)+1)>(round+1)? ((int)(timeFromInit / BPM)):(round+1);
    }

    private int round2Ges()
    {
        return round % 3;
    }

    private void setGestureText(int gesture)
    {
        
        if (gesture == 0)
        {
            // 左
            gestureMesh.text = "left";
        }
        if (gesture == 1)
        {
            // 上
            gestureMesh.text = "up";
        }
        if (gesture == 2)
        {
            // 右
            gestureMesh.text = "right";
        }
        if (gesture == 3)
        {
            // 下
            gestureMesh.text = "down";
        }
    }
}
