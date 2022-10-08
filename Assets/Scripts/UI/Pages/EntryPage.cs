using UnityEngine;
using UnityEngine.UI;

public class EntryPage : PageBase
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private Button guestLoginButton;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        loginButton.onClick.RemoveAllListeners();
        signupButton.onClick.RemoveAllListeners();
        guestLoginButton.onClick.RemoveAllListeners();

        loginButton.onClick.AddListener(() => UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<LoginDialog>()));
        signupButton.onClick.AddListener(() => UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<SignupDialog>()));
        guestLoginButton.onClick.AddListener(() =>
        {
            AccountController.LoginAsGuest();
        });
    }

    //private void Update()
    //{
    //    if (Input.anyKey)
    //    {
    //        OpenMainPage();
    //    }
    //}

    //private void Start()
    //{    
        
    //}

    public void OpenMainPage()
    {
        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<GameHUDPage>());
    }
}
