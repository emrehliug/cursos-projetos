using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Bot.Instagram.Profile
{
    static class Instragram
    {
        public static Profile GetProfileByUser(string userName)
        {
            var profile = new Profile(userName);

            string url = @"https://www.instagram.com/" + userName + "/";
            string html;

            using (WebClient wc = new WebClient())
            {
                html = wc.DownloadString(url);
            }

            HtmlAgilityPack.HtmlDocument conteudoHtml = new HtmlAgilityPack.HtmlDocument();
            conteudoHtml.LoadHtml(html);

            var list = conteudoHtml.DocumentNode.SelectNodes("//meta");

            foreach (var node in list)
            {
                string property = node.GetAttributeValue("property", "");

                if(property == "al:ios:app_name")
                {
                    profile.IosAppName = node.GetAttributeValue("content", "");
                }
                if (property == "al:ios:app_store_id")
                {
                    profile.IosAppId = node.GetAttributeValue("content", "");
                }
                if (property == "al:ios:url")
                {
                    profile.IosUrl = node.GetAttributeValue("content", "");
                }
                if (property == "al:android:package")
                {
                    profile.AndroidAppId = node.GetAttributeValue("content", "");
                }
                if (property == "al:android:url")
                {
                    profile.AndroidUrl = node.GetAttributeValue("content", "");
                }
                if (property == "og:type")
                {
                    profile.Type = node.GetAttributeValue("content", "");
                }
                if (property == "og:image")
                {
                    profile.Image = node.GetAttributeValue("content", "");
                }
                if (property == "og:title")
                {
                    profile.Title = node.GetAttributeValue("content", "");
                }
                if (property == "og:description")
                {
                    profile.Description = node.GetAttributeValue("content", "");
                }
                if (property == "og:url")
                {
                    profile.Url = node.GetAttributeValue("content", "");
                }
            }

            return profile;
        }
    }
}
  