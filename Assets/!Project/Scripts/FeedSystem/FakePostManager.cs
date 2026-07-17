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
    public Vector2 sendsMinAndMax;
    private FakePost previousFakePostData, actualFakePostData, nextFakePostData;
    private PostData previousPostData, actualPostData, nextPostData;
    private bool previousFirst;

    async void Start()
    {

        StartScroll();

    }


    async void StartScroll() 
    {
        actualFakePostData = await NextScrollScreen();
        actualPostData = NextPostDataToFakePostData(actualFakePostData);
        previousFakePostData = await NextScrollScreen();
        previousPostData = NextPostDataToFakePostData(previousFakePostData);
        nextFakePostData = await NextScrollScreen();
        nextPostData = NextPostDataToFakePostData(nextFakePostData);

        previousFirst = true;

        SetPost();

    }
    public void PreviousScroll()
    {
        if (!previousFirst)
        {
            nextFakePostData = actualFakePostData;
            nextPostData = actualPostData;
            actualFakePostData = previousFakePostData;
            actualPostData = previousPostData;
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
        previousFakePostData = actualFakePostData;
        previousPostData = actualPostData;
        actualFakePostData = nextFakePostData;
        actualPostData = nextPostData;
        nextFakePostData = await NextScrollScreen();
        nextPostData = NextPostDataToFakePostData(nextFakePostData);
        SetPost();

    }
    

    public void SetPost()
    {
        previousPost.SetPostData(previousPostData, 0);
        actualPost.SetPostData(actualPostData, animationSpeed);
        nextPost.SetPostData(nextPostData, 0);
    }

    public PostData NextPostDataToFakePostData(FakePost fakePost)
    {
        PostData aux = new PostData(
            fakePost.postUsers[Random.Range(0, fakePost.postUsers.Length)],
            fakePost.postDescriptions[Random.Range(0, fakePost.postDescriptions.Length)],
            (int)Random.Range(likesMinAndMax.x, likesMinAndMax.y),
            (int)Random.Range(commentsMinAndMax.x, commentsMinAndMax.y),
            (int)Random.Range(replyMinAndMax.x, replyMinAndMax.y),
            (int)Random.Range(sendsMinAndMax.x, sendsMinAndMax.y),
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
        while(aux == actualFakePostData)
        {
            aux = fakePosts[Random.Range(0, fakePosts.Count)];
            await UniTask.Delay(1);
        }
        return aux;

    }

}


