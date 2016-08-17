using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FormsDemo.Services
{
    public class Name
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Picture
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }

    public class Person
    {
        public Name Name { get; set; }
        public Picture Picture { get; set; }
    }

    public class Info
    {
        public string Seed { get; set; }
        public int Results { get; set; }
        public int Page { get; set; }
        public string Version { get; set; }
    }

    public class RootObject
    {
        public List<Person> Results { get; set; }
        public Info Info { get; set; }
    }

    public interface IHttpService
    {
        HttpClient HttpClient { get; }

        Task<IList<Person>> DownloadPeople(CancellationToken token);
    }

    class HttpService : IHttpService
    {
        public HttpService()
        {
            HttpClient = new HttpClient();
        }

        public HttpClient HttpClient { get; }

        public async Task<IList<Person>> DownloadPeople(CancellationToken token)
        {
            var responseMessage = await HttpClient.GetAsync("http://api.randomuser.me/?inc=name,picture&results=1000", token).ConfigureAwait(false);

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var responseContent = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            
            var result = JsonConvert.DeserializeObject<RootObject>(responseContent);

            return result.Results;
        }
    }
}