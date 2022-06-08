using System.Collections.Generic;
public class PoseData {
    private List<JointData> jointDatas;

    public List<JointData> JointDatas => jointDatas;

    public PoseData(List<JointData> jointDatas) {
        this.jointDatas = jointDatas;
    }
}