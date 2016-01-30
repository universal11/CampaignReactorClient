using libCampaignReactor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CampaignReactorClient {
    public class CampaignReactorClient {
        //private string host = "localhost";
        //private int port = 3579;


        public CampaignReactorClient() {

        }

        /*
        public void init(string host, int port) {
            this.host = host;
            this.port = port;
            
        }
        */

            /*
        public void initHeaders(RestRequest request) {
            // easily add HTTP Headers
            //request.AddHeader("header", "value");
            //request.RequestFormat = RestSharp.DataFormat.Json;
        }
        */

        public string getAPIUri() {
            return $"http://192.168.1.146:3579";
            //return new Uri($"http://localhost:3579");

        }

        /*
        public Campaign getCampaignById(int id) {
            Campaign campaign = new Campaign();
            RestClient client = new RestClient(this.getAPIUri());
            
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"campaign/{id}", HttpMethod.Get);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method

            

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<Campaign>(CampaignReactor.getResponse(response.RawBytes));
        }

        public static string getResponse(byte[] data) {
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public List<Campaign> getAllCampaigns() {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign", HttpHttpMethod.Get);
            this.initHeaders(request);

            // execute the request

            client.ExecuteAsync(request, response => {
                return JsonConvert.DeserializeObject<List<Campaign>>(response.Content);
            });

            var task = client.ExecuteAsync(request, response => {
                JsonConvert.DeserializeObject<List<Campaign>>(response.Content);
            }).WebRequest;

            task.WebRequest.wait();

            return task.Result;

        }

        public List<Campaign> getCampaignsByName(string name) {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"campaign/name/{name}", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Campaign>>(CampaignReactor.getResponse(response.RawBytes));
        }
        */
        public List<Campaign> getEnabledCampaigns() {
            List<Campaign> campaigns = new List<Campaign>();

            HttpResponseMessage httpResponseMessage = this.httpGet($"{this.getAPIUri()}/campaign/enabled");
            if (httpResponseMessage.IsSuccessStatusCode) {
                campaigns = JsonConvert.DeserializeObject<List<Campaign>>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            return campaigns;
             

            /*
            EveClient client = new EveClient();
            client.BaseAddress = this.getAPIUri();
            client.ResourceName = "campaign/enabled";
            campaigns = client.GetAsync<Campaign>().Result;
            foreach (Campaign campaign in campaigns) {
                System.Diagnostics.Debug.WriteLine($"Campaign | Name : {campaign.name}");
            }
            return campaigns;
            */

        }

        public HttpResponseMessage httpPost(string url, string data) {
            HttpClient client = new HttpClient();
            return client.PostAsync(url, new StringContent(data)).Result;
        }

        public HttpResponseMessage httpGet(string url) {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            try{
                response = client.GetAsync(url).Result;
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
            }
            return response;
        }

 

        
        public List<Server> getEnabledServers() {
            List<Server> servers = new List<Server>();

            HttpResponseMessage httpResponseMessage = this.httpGet($"{this.getAPIUri()}/server/enabled");
            if (httpResponseMessage.IsSuccessStatusCode) {
                servers = JsonConvert.DeserializeObject<List<Server>>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            return servers;
        }

        /*
        public List<Host> getEnabledHosts() {
            //List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/enabled", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Host>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Bot> getEnabledBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/enabled", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Bot>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<BitlyAccount> getEnabledBitlyAccounts() {
            List<BitlyAccount> bitlyAccounts = new List<BitlyAccount>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/enabled", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<BitlyAccount>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Bot> getAvailableBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/available", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Bot>>(CampaignReactor.getResponse(response.RawBytes));
        }
        */
        public List<Subscriber> getEnabledSubscribers() {

            List<Subscriber> subscribers = new List<Subscriber>();

            HttpResponseMessage httpResponseMessage = this.httpGet($"{this.getAPIUri()}/subscriber/enabled");
            if (httpResponseMessage.IsSuccessStatusCode) {
                subscribers = JsonConvert.DeserializeObject<List<Subscriber>>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            return subscribers;
        }

        /*
        public int updateCampaign(Campaign campaign) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/update", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(campaign);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateServer(Server server) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/update", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(server);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateBot(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/update", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateBitlyAccount(BitlyAccount bitlyAccount) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/update", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(bitlyAccount);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateSubscriber(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/update", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int unsubscribe(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/unsubscribe", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }
        */
        public int createCampaign(Campaign campaign) {
            int response = 0;

            HttpResponseMessage httpResponseMessage = this.httpPost($"{this.getAPIUri()}/campaign/create", JsonConvert.SerializeObject(campaign));
            if (httpResponseMessage.IsSuccessStatusCode) {
                response = JsonConvert.DeserializeObject<int>(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            return response;
        }

        /*

        public int createServer(Server server) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/create", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(server);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createBot(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/create", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createBitlyAccount(BitlyAccount bitlyAccount) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/create", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(bitlyAccount);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createSubscriber(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/create", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createHost(Host host) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/create", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(host);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Bot getBotById(int id) {
            Bot bot = new Bot();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"bot/{id}", HttpMethod.Get);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<Bot>(CampaignReactor.getResponse(response.RawBytes));
        }

        public BitlyAccount getBitlyAccountById(int id) {
            BitlyAccount bitlyAccount = new BitlyAccount();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"bitly_account/{id}", HttpMethod.Get);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<BitlyAccount>(CampaignReactor.getResponse(response.RawBytes));
        }

        public BitlyAccount getNextBitlyAccount() {
            BitlyAccount bitlyAccount = new BitlyAccount();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/next", HttpMethod.Get);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<BitlyAccount>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Host getNextHost() {
            Host host = new Host();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/next", HttpMethod.Get);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<Host>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Bot> getAllBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Bot>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<BitlyAccount> getAllBitlyAccounts() {
            List<BitlyAccount> bots = new List<BitlyAccount>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<BitlyAccount>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Subscriber getSubscriberById(int id) {
            Subscriber subscriber = new Subscriber();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/{id}", HttpMethod.Get);
            this.initHeaders(request);



            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<Subscriber>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Server getServerById(int id) {
            Server server = new Server();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"server/{id}", HttpMethod.Get);
            this.initHeaders(request);



            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<Server>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Host getHostById(int id) {
            Host host = new Host();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"host/{id}", HttpMethod.Get);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<Host>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Server> getAllServers() {
            List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Server>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getAllSubscribers() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getSubscribersByBotId(int id) {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/bot/{id}", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getSendQueue() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/send_queue", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getSendQueueByBotId(int id) {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/send_queue/{id}", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<EventLog> getEventLogsBySubscriberId(int id) {
            List<EventLog> eventLogs = new List<EventLog>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"event_log/subscriber/{id}", HttpMethod.Get);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<EventLog>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int markBotAsSent(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/sent", HttpMethod.Post);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        */
    }
}
