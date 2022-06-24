using Windows.Kinect;
public class JointData {

    private JointType jointType;
    private float errorValue;
    private bool isCorrect;

    public JointType JointType => jointType;
    public float ErrorValue => errorValue;
    public bool IsCorrect => isCorrect;

    public JointData(JointType jointType, float errorValue, bool isCorrect) {
        this.jointType = jointType;
        this.errorValue = errorValue;
        this.isCorrect = isCorrect;
    }
}