using Windows.Kinect;
public class JointData {

    private JointType jointType;
    private float errorValue;

    public JointType JointType => jointType;
    public float ErrorValue => errorValue;

    public JointData(JointType jointType, float errorValue) {
        this.jointType = jointType;
        this.errorValue = errorValue;
    }
}
