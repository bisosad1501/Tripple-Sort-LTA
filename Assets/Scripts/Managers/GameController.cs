using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif


public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public MoneyEffectController moneyEffectController;
    public UseProfile useProfile;
    public DataContain dataContain;
    public float progressLoading;
    public bool isShowOpen;
    [HideInInspector] public SceneType currentScene;

    protected void Awake()
    {
        Input.multiTouchEnabled = false;
        Instance = this;
        Init();
        DontDestroyOnLoad(this);



#if UNITY_IOS
    if(ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == 
    ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
    {

        ATTrackingStatusBinding.RequestAuthorizationTracking();

    }

#endif
    }


    public void Init()
    {
        useProfile.CurrentLevelPlay = UseProfile.CurrentLevel;
        //useProfile.IsRemoveAds = true;
        LoadSceneDone("Level");
    }


    public void LoadSceneDone(string sceneName)
    {
        AsyncOperation loadscene = SceneManager.LoadSceneAsync(sceneName);
    }
}

public enum SceneType
{
    StartLoading = 0,
    MainHome = 1
}
