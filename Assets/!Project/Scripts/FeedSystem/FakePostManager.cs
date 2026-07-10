using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;

public class FakePostManager : MonoSingleton<FakePostManager>
{
    public float scrollCount; //TODO trocar para a variavel real dps
    public List<FakePost> fakePosts = new List<FakePost>();
    public Post previousPost, actualPost, nextPost;
    public float animationSpeed;
    public Vector2 likesMinAndMax;
    public Vector2 commentsMinAndMax;
    public Vector2 replyMinAndMax;
    private FakePost previousPostData, actualPostData, nextPostData;
    private bool previousFirst;

    private void Start()
    {
        StartScroll();
    }


    async void StartScroll() 
    {
        nextPostData = await NextScrollScreen();
        NextScroll();
        previousFirst = true;

    }
    public void PreviousScroll()
    {
        if (!previousFirst)
        {
            actualPostData = previousPostData;
            nextPostData = actualPostData;
            previousFirst = true;
            SetPost();
        }
        else
        {
            Debug.Log("Evento de Recarregar");
            //TODO - Evento de Recarregar
        }
    }

    public async void NextScroll()
    {
        scrollCount += previousFirst ? 0 : 1;
        previousFirst = false;
        previousPostData = actualPostData;
        actualPostData = nextPostData;
        nextPostData = await NextScrollScreen();
        SetPost();

    }
    

    public void SetPost()
    {
        previousPost.SetPostData(PostDataToFakePostData(previousPostData), animationSpeed);
        actualPost.SetPostData(PostDataToFakePostData(actualPostData), animationSpeed);
        nextPost.SetPostData(PostDataToFakePostData(nextPostData), animationSpeed);
    }

    public PostData PostDataToFakePostData(FakePost fakePost)
    {
        PostData aux = new PostData(
            fakePost.postUsers[Random.Range(0, fakePost.postUsers.Length)],
            fakePost.postDescriptions[Random.Range(0, fakePost.postUsers.Length)],
            (int)Random.Range(likesMinAndMax.x, likesMinAndMax.y),
            (int)Random.Range(commentsMinAndMax.x, commentsMinAndMax.y),
            (int)Random.Range(replyMinAndMax.x, replyMinAndMax.y),
            fakePost.previewImage,
            fakePost.video
            );

        return aux;
    }

    async UniTask<FakePost> NextScrollScreen()
    {
        if(fakePosts.Count == 1)
        {
            return fakePosts[0];
        }
        FakePost aux = fakePosts[Random.Range(0, fakePosts.Count)];
        while(aux == actualPost)
        {
            aux = fakePosts[Random.Range(0, fakePosts.Count)];
            await UniTask.Delay(1);
        }
        return aux;

    }

}


