using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public TMP_Text user;
    public TMP_Text description;
    public TMP_Text likes;
    public TMP_Text comments;
    public TMP_Text replys;
    public Image postImage;
    public Animator postAnimator;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        overrideController = new AnimatorOverrideController(postAnimator.runtimeAnimatorController);
        postAnimator.runtimeAnimatorController = overrideController;
    }


    public void SetPostData(PostData postData, float animatorSpeed)
    {
        user.text = postData.user;
        description.text = postData.description;
        likes.text = postData.likes.ToString();
        comments.text = postData.comments.ToString();
        replys.text = postData.replys.ToString();
        postImage.sprite = postData.postImage;
        postAnimator.speed = animatorSpeed;
        overrideController["PostAnimation"] = postData.postAnimation;
        postAnimator.Play("PostAnimation", 0, 0f);
    }

}

public class PostData
{
    public string user;
    public string description;
    public int likes;
    public int comments;
    public int replys;
    public Sprite postImage;
    public AnimationClip postAnimation;
    public PostData(string user, string description, int likes, int comments, int replys, Sprite postImage, AnimationClip postAnimation)
    {
        this.user = user;
        Debug.Log(this.user);
        this.description = description;
        Debug.Log(this.description);
        this.likes = likes;
        Debug.Log(this.likes);
        this.comments = comments;
        Debug.Log(this.comments);
        this.replys = replys;
        Debug.Log(this.replys);
        this.postImage = postImage;
        Debug.Log(this.postImage.name);
        this.postAnimation = postAnimation;
        Debug.Log(this.postAnimation.name);
    }
}
