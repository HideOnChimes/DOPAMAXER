using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public TMP_Text user;
    public TMP_Text description;
    public TMP_Text likes;
    public TMP_Text comments;
    public TMP_Text reposts;
    public TMP_Text sends;
    public Image postImage;
    public Animator postAnimator;
    public Slider videoPercent;
    public PostSpriteBank postSpriteBank;
    public Image likeImage;
    public DOTweenAnimation DotLikeAnim;
    private AnimatorOverrideController overrideController;
    private AnimatorStateInfo state;
    private bool isLiked;
    private int currentLikes;
    private int currentComments;
    private int currentReposts;
    private int currentSends;

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
    public void LikeSpriteButton()
    {
        if (!isLiked && !DotLikeAnim.gameObject.activeInHierarchy)
        {
            DotLikeAnim.gameObject.SetActive(true);
            DotLikeAnim.DORestart();
            DotLikeAnim.tween.onComplete = (() => ChangeSprite("like", isLiked));
        }
        else 
        {
            DotLikeAnim.tween.Complete();
            DotLikeAnim.gameObject.SetActive(false);
            ChangeSprite("like", isLiked);
        }
        
    }

    public void ChangeSprite(string tag, bool mode) 
    {
        if (!mode)
        {
            OnSprite(tag);
            
        }
        else 
        {
            OffSprite("un" + tag);
        }
    }

    public void OnSprite(string tag) 
    {
        if (!isLiked) 
        {
            currentLikes++;
            likes.text = ConvertNumber(currentLikes);
        }
        isLiked = true;
        likeImage.sprite = postSpriteBank.GetSprite(tag);
        Debug.Log("Sprite changed to: " + tag);

    }

    public void OffSprite(string tag)
    {
        
        if(isLiked)
        {
            currentLikes--;
            likes.text = ConvertNumber(currentLikes);
        }
        isLiked = false;
        likeImage.sprite = postSpriteBank.GetSprite(tag);
    }




    public void SetPostData(PostData postData, float animatorSpeed)
    {
        user.text = postData.user;
        description.text = postData.description;
        likes.text = ConvertNumber(postData.likes);
        currentLikes = postData.likes;
        comments.text = ConvertNumber(postData.comments);
        currentComments = postData.comments;
        reposts.text = ConvertNumber(postData.reposts);
        currentReposts = postData.reposts;
        sends.text = ConvertNumber(postData.sends);
        currentSends = postData.sends;
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
    public int reposts;
    public int sends;
    public Sprite postImage;
    public AnimationClip postAnimation;
    public PostData(string user, string description, int likes, int comments, int reposts,int sends, Sprite postImage, AnimationClip postAnimation)
    {
        this.user = user;
        
        this.description = description;
        
        this.likes = likes;
        this.comments = comments;
        this.reposts = reposts;
        this.sends = sends;
        this.postImage = postImage;
        this.postAnimation = postAnimation;
    }
}
