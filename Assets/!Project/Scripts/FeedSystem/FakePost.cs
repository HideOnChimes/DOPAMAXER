using UnityEngine;


[CreateAssetMenu(fileName = "FakePost", menuName = "FeedSystem/FakePost")]
public class FakePost : ScriptableObject
{
    public string[] postUsers;
    public string[] postDescriptions;
    public AnimationClip video;
}
