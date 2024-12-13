﻿using Connect.DNN.Powershell.Common;
using Connect.DNN.Powershell.Data;
using Connect.DNN.Powershell.Framework.Models;
using System.IO;
using System.Net;

namespace Connect.DNN.Powershell.Framework
{
    public class DnnPromptController
    {
        public static Site CurrentSite { get; set; }
        public static Portal CurrentPortal { get; set; }

        public static ServerResponse GetToken(string siteUrl, string username, string password)
        {
            var url = string.Format("{0}/DesktopModules/JwtAuth/API/mobile/login", siteUrl);
            var request = WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = WebRequestMethods.Http.Post;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"u\":\"" + username + "\"," +
                              "\"p\":\"" + password + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string tokenText;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    tokenText = sr.ReadToEnd();
                }
                return new ServerResponse()
                {
                    Contents = tokenText,
                    Status = ServerResponseStatus.Success
                };
            }
            catch (WebException ex)
            {
                switch (((HttpWebResponse)ex.Response).StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        return new ServerResponse()
                        {
                            Status = ServerResponseStatus.Failed
                        };
                    default:
                        return new ServerResponse()
                        {
                            Status = ServerResponseStatus.Error
                        };
                }
            }
        }

        public static ServerResponse RenewToken(Site site)
        {
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtToken>(site.Token.Decrypt());
            var url = string.Format("{0}/DesktopModules/JwtAuth/API/mobile/extendtoken", site.Url);
            var request = WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add("Authorization", "Bearer " + token.accessToken);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = "{\"rtoken\":\"" + token.renewalToken + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string tokenText;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    tokenText = sr.ReadToEnd();
                }
                return new ServerResponse()
                {
                    Contents = tokenText,
                    Status = ServerResponseStatus.Success
                };
            }
            catch (WebException ex)
            {
                switch (((HttpWebResponse)ex.Response).StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        return new ServerResponse()
                        {
                            Status = ServerResponseStatus.Failed
                        };
                    default:
                        return new ServerResponse()
                        {
                            Status = ServerResponseStatus.Error
                        };
                }
            }
        }

        public static ServerResponse ProcessCommand(Site site, int retry, string commandLine)
        {
            return ProcessCommand(site, -1, retry, commandLine, -1);
        }
        public static ServerResponse ProcessCommand(Site site, int portalId, int retry, string commandLine)
        {
            return ProcessCommand(site, portalId, retry, commandLine, -1);
        }
        public static ServerResponse ProcessCommand(Site site, int portalId, int retry, string commandLine, int currentPage)
        {
            var res = new ServerResponse();
            if (retry == 0)
            {
                res.Status = ServerResponseStatus.Error;
                return res;
            }
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtToken>(site.Token.Decrypt());
            var promptUrl = string.Format("{0}/API/PersonaBar/Command/Cmd", site.Url);
            if (portalId > -1)
            {
                promptUrl += string.Format("/{0}", portalId);
            }
            var request = WebRequest.Create(promptUrl);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add("Authorization", "Bearer " + token.accessToken);
            var reqCmd = new PromptCommand()
            {
                CmdLine = commandLine,
                CurrentPage = currentPage
            };
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(reqCmd);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    res.Contents = sr.ReadToEnd();
                }
                return res;
            }
            catch (WebException ex)
            {
                switch (((HttpWebResponse)ex.Response).StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        var renew = RenewToken(site);
                        if (renew.Status != ServerResponseStatus.Success)
                        {
                            res.Status = renew.Status;
                            return res;
                        }
                        site.Token = renew.Contents.Encrypt();
                        var sites = SiteList.Instance();
                        var listKey = "";
                        foreach (var s in sites.Sites)
                        {
                            if (s.Value.Url == site.Url)
                            {
                                listKey = s.Key;
                            }
                        }
                        if (!string.IsNullOrEmpty(listKey))
                        {
                            sites.SetSite(listKey, site.Url, renew.Contents);
                        }
                        return ProcessCommand(site, portalId, retry - 1, commandLine, currentPage);
                    default:
                        res.Status = ServerResponseStatus.Error;
                        return res;
                }
            }
        }

        public static ServerResponse ProcessApi(Site site, int retry, int tabId, int moduleId, string moduleName, string controller, string action, string httpMethod, string payload)
        {
            var res = new ServerResponse();
            if (retry == 0)
            {
                res.Status = ServerResponseStatus.Error;
                return res;
            }
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<JwtToken>(site.Token.Decrypt());
            var url = string.Format("{0}/DesktopModules/{1}/API/{2}/{3}", site.Url, moduleName, controller, action);
            var request = WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8";
            request.Method = httpMethod;
            request.Headers.Add("Authorization", "Bearer " + token.accessToken);
            request.Headers.Add("Tabid", tabId.ToString());
            request.Headers.Add("Moduleid", moduleId.ToString());
            if (httpMethod == WebRequestMethods.Http.Post)
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    //string json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                    string json = payload;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    res.Contents = sr.ReadToEnd();
                }
                return res;
            }
            catch (WebException ex)
            {
                switch (((HttpWebResponse)ex.Response).StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        var renew = RenewToken(site);
                        if (renew.Status != ServerResponseStatus.Success)
                        {
                            res.Status = renew.Status;
                            return res;
                        }
                        site.Token = renew.Contents.Encrypt();
                        var sites = SiteList.Instance();
                        var listKey = "";
                        foreach (var s in sites.Sites)
                        {
                            if (s.Value.Url == site.Url)
                            {
                                listKey = s.Key;
                            }
                        }
                        if (!string.IsNullOrEmpty(listKey))
                        {
                            sites.SetSite(listKey, site.Url, renew.Contents);
                        }
                        return ProcessApi(site, retry - 1, tabId, moduleId, moduleName, controller, action, httpMethod, payload);
                    default:
                        res.Status = ServerResponseStatus.Error;
                        return res;
                }
            }
        }
    }
}
