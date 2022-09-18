using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryPage : PageBase
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private Button tapToStartButton;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        loginButton.onClick.RemoveAllListeners();
        signupButton.onClick.RemoveAllListeners();

        loginButton.onClick.AddListener(()=>UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<LoginDialog>()));
        signupButton.onClick.AddListener(()=>UI_Manager.instance.OpenDialog(UI_Manager.instance.GetDialogOfType<SignupDialog>()));
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            OpenMainPage();
        }
    }

    public void OpenMainPage()
    {
        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<GameHUDPage>());
    }
}
