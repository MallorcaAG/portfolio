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

        
    }

    private void ButtonInit()
    {
        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);

        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);

        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);

        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);

        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);

        linkedinBtn = ui.rootVisualElement.Q("LinkedIn") as Button;
        linkedinBtn.RegisterCallback<ClickEvent>(LinkedInButtonPressed);
    }
    
    private void OnDisable()
    {
        linkedinBtn.UnregisterCallback<ClickEvent>(LinkedInButtonPressed);
    }
    #endregion

    #region Helper Functions
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
