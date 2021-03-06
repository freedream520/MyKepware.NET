﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEPConfApiHelper
{
   public static class KepConfApiH
    {
        public static string kepConfApiUrl;
        public static HttpBasicAuthenticator kepAuth;

        public static int createCh(KepTemplate keptemplate, string name)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/");
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("authorization", "Basic QWRtaW5pc3RyYXRvcjo=");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json",  keptemplate.getChJson(name,"5000"), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (int)response.StatusCode;

        }


        public static int createDev(KepTemplate keptemplate, string chname, string name,string hostid,string port)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname + "/devices/");
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", keptemplate.getDevJson(name, hostid,port), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (int)response.StatusCode;

        }

        public static int createTags(KepTemplate keptemplate,string chname, string devname, List<Tuple<string, string, int>> tags)
        {


            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname + "/devices/" + devname + "/tags");
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.POST);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

         
           var tagsJson = keptemplate.getTagsJson(tags);
            request.AddParameter("application/json", tagsJson, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return (int)response.StatusCode;

        }


      public static int deleteTag(string chname, string devname, string tname)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname + "/devices/" + devname + "/tags/" + tname);
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            IRestResponse response = client.Execute(request);
            return (int)response.StatusCode;

        }

        public static int deleteDev(string chname, string devname)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname + "/devices/" + devname);
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            IRestResponse response = client.Execute(request);
            return (int)response.StatusCode;

        }

        public static int deleteCh(string chname)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname);
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");

            IRestResponse response = client.Execute(request);
            return (int)response.StatusCode;

        }


        public static string getChListJson()
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/");
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);
            return response.Content;

        }
        public static string getDevListJson(string chname)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname + "/devices");
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);
            return response.Content;

        }

        public static string getTagListJson(string chname, string devname)
        {
            var client = new RestClient(kepConfApiUrl + "/config/v1/project/channels/" + chname + "/devices/" + devname + "/tags");
            client.Authenticator = kepAuth;
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            IRestResponse response = client.Execute(request);
            return response.Content;

        }
    }
}
