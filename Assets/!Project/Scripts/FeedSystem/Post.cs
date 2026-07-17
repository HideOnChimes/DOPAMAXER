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
    public TMP_Text sends;
    public Image postImage;
    public Animator postAnimator;
    public Slider videoPercent;
    private AnimatorOverrideController overrideController;
    private AnimatorStateInfo state;

    private void Awake()
    {
        overrideController = new AnimatorOverrideController(postAnimator.runtimeAnimatorController);
        postAnimator.runtimeAnimatorController = overrideController;
    }

    private void Update()
    {

        if (videoPercent != null)
        {
            state = postAnimator.GetCurrentAnimatorStateInfo(0);
            float percent = Mathf.Repeat(state.normalizedTime, 1f);
            videoPercent.value = percent;
        }

    }


    public void SetPostData(PostData postData, float animatorSpeed)
    {
        user.text = postData.user;
        description.text = postData.description;
        likes.text = ConvertNumber(postData.likes);
        comments.text = ConvertNumber(postData.comments);
        replys.text = ConvertNumber(postData.replys);
        sends.text = ConvertNumber(postData.sends);
        postImage.sprite = postData.postImage;
        postAnimator.speed = animatorSpeed;
        overrideController["PostAnimation"] = postData.postAnimation;
        postAnimator.Play("PostAnimation", 0, 0f);
        state = postAnimator.GetCurrentAnimatorStateInfo(0);
    }

    string ConvertNumber(int number) 
    {
        if (number >= 1000000)
        {
            return (number / 1000000f).ToString("0.#") + "M";
        }
        else if (number >= 9999)
        {
            return (number / 1000f).ToString("0") + "K";
        }
        else
        {
            return number.ToString();
        }   
    }

}

public class PostData
{
    public string user;
    public string description;
    public int likes;
    public int comments;
    public int replys;
    public int sends;
    public Sprite postImage;
    public AnimationClip postAnimation;
    public PostData(string user, string description, int likes, int comments, int replys,int sends, Sprite postImage, AnimationClip postAnimation)
    {
        this.user = user;
        
        this.description = description;
        
        this.likes = likes;
        this.comments = comments;
        this.replys = replys;
        this.sends = sends;
        this.postImage = postImage;
        this.postAnimation = postAnimation;
    }
}
