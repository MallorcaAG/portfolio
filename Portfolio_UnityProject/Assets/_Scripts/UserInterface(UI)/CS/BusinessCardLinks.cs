using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BusinessCardLinks : MonoBehaviour
{
    [SerializeField] private BtnOpensLink LinkedIn, Youtube, Github, Steam, Itchio, Discord;
    private UIDocument ui;

    private Button linkedinBtn, ytBtn, githubBtn, steamBtn, itchioBtn, discordBtn;

    #region Initialization & De-initialization
    private void Awake()
    {
        ui = GetComponent<UIDocument>();

        ButtonInit();
    }
    private void ButtonInit()
    {
        //Registers Button Click events to functions.
        //Add new buttons at the bottom, changing the Button variable, Q UI Toolkit button, and ClickEvent function

        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);

        ytBtn = ui.rootVisualElement.Q("Youtube") as Button;
        ytBtn.RegisterCallback<ClickEvent>(YoutubeButtonPressed);

        githubBtn = ui.rootVisualElement.Q("Github") as Button;
        githubBtn.RegisterCallback<ClickEvent>(GithubButtonPressed);

        steamBtn = ui.rootVisualElement.Q("Steam") as Button;
        steamBtn.RegisterCallback<ClickEvent>(SteamButtonPressed);

        itchioBtn = ui.rootVisualElement.Q("Itchio") as Button;
        itchioBtn.RegisterCallback<ClickEvent>(ItchioButtonPressed);

        discordBtn = ui.rootVisualElement.Q("Discord") as Button;
        discordBtn.RegisterCallback<ClickEvent>(DiscordButtonPressed);
    }
    
    private void OnDisable()
    {
        ButtonDeInit();
    }
    private void ButtonDeInit()
    {
        // Unregisters Button Click events once disabled
        //Add new buttons at the bottom following the format given

        linkedinBtn.UnregisterCallback<ClickEvent>(LinkedInButtonPressed);
        ytBtn.UnregisterCallback<ClickEvent>(YoutubeButtonPressed);
        githubBtn.UnregisterCallback<ClickEvent>(GithubButtonPressed);
        steamBtn.UnregisterCallback<ClickEvent>(SteamButtonPressed);
        itchioBtn.UnregisterCallback<ClickEvent>(ItchioButtonPressed);
        discordBtn.UnregisterCallback<ClickEvent>(DiscordButtonPressed);
    }
    #endregion

    #region Link Openning Functions
    private void OpenLinkViaLinkType(BtnOpensLink btn)
    {
        if(btn.getLinkType() == BtnOpensLink.LinkType.InCurrent)
        {
            OpenLinks.OpenURLInCurrent(btn.getUrl());
        }
        else
        {
            OpenLinks.OpenURLInNew(btn.getUrl());
        }
    }
    #endregion

    #region Button Pressed Functions
    public void LinkedInButtonPressed(ClickEvent e)
    {
        OpenLinkViaLinkType(LinkedIn);
    }
    public void YoutubeButtonPressed(ClickEvent e)
    {
        OpenLinkViaLinkType(Youtube);
    }
    public void GithubButtonPressed(ClickEvent e)
    {
        OpenLinkViaLinkType(Github);
    }
    public void SteamButtonPressed(ClickEvent e)
    {
        OpenLinkViaLinkType(Steam);
    }
    public void ItchioButtonPressed(ClickEvent e)
    {
        OpenLinkViaLinkType(Itchio);
    }
    public void DiscordButtonPressed(ClickEvent e)
    {
        OpenLinkViaLinkType(Discord);
    }
    #endregion

}


[Serializable]
public class BtnOpensLink
{
    [SerializeField] private LinkType linkType;
    [SerializeField] private string url = "https://www.google.com/";

    public LinkType getLinkType() { return linkType; }
    public void setLinkType(LinkType linkType) { this.linkType = linkType; }
    public string getUrl() { return url; }
    public void setUrl(string url) { this.url = url; }

    public enum LinkType
    {
        InCurrent, NewTab
    }
}
