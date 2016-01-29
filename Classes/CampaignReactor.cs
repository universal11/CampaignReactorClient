using libCampaignReactor.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;


namespace CampaignReactorClient.Classes {
    public class CampaignReactor {
        //private string host = "localhost";
        //private int port = 3579;


        public CampaignReactor() {

        }

        /*
        public void init(string host, int port) {
            this.host = host;
            this.port = port;
            
        }
        */

        public void initHeaders(RestRequest request) {
            // easily add HTTP Headers
            //request.AddHeader("header", "value");
            //request.RequestFormat = RestSharp.DataFormat.Json;
        }

        public Uri getAPIUri() {
            return new Uri($"http://localhost:3579");
        }

        public Campaign getCampaignById(int id) {
            Campaign campaign = new Campaign();
            RestClient client = new RestClient(this.getAPIUri());
            
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"campaign/{id}", Method.GET);
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
            RestRequest request = new RestRequest("campaign", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Campaign>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Campaign> getCampaignsByName(string name) {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"campaign/name/{name}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Campaign>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Campaign> getEnabledCampaigns() {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Campaign>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Server> getEnabledServers() {
            //List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Server>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Host> getEnabledHosts() {
            //List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Host>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Bot> getEnabledBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Bot>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<BitlyAccount> getEnabledBitlyAccounts() {
            List<BitlyAccount> bitlyAccounts = new List<BitlyAccount>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<BitlyAccount>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Bot> getAvailableBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/available", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Bot>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getEnabledSubscribers() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateCampaign(Campaign campaign) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(campaign);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateServer(Server server) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(server);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateBot(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateBitlyAccount(BitlyAccount bitlyAccount) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(bitlyAccount);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int updateSubscriber(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int unsubscribe(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/unsubscribe", Method.POST);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createCampaign(Campaign campaign) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(campaign);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createServer(Server server) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(server);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createBot(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createBitlyAccount(BitlyAccount bitlyAccount) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(bitlyAccount);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createSubscriber(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int createHost(Host host) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/create", Method.POST);
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
            RestRequest request = new RestRequest($"bot/{id}", Method.GET);
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
            RestRequest request = new RestRequest($"bitly_account/{id}", Method.GET);
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
            RestRequest request = new RestRequest("bitly_account/next", Method.GET);
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
            RestRequest request = new RestRequest("host/next", Method.GET);
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
            RestRequest request = new RestRequest("bot", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Bot>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<BitlyAccount> getAllBitlyAccounts() {
            List<BitlyAccount> bots = new List<BitlyAccount>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<BitlyAccount>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Subscriber getSubscriberById(int id) {
            Subscriber subscriber = new Subscriber();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/{id}", Method.GET);
            this.initHeaders(request);



            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<Subscriber>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Server getServerById(int id) {
            Server server = new Server();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"server/{id}", Method.GET);
            this.initHeaders(request);



            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<Server>(CampaignReactor.getResponse(response.RawBytes));
        }

        public Host getHostById(int id) {
            Host host = new Host();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"host/{id}", Method.GET);
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
            RestRequest request = new RestRequest("server", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Server>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getAllSubscribers() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;

            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getSubscribersByBotId(int id) {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/bot/{id}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getSendQueue() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/send_queue", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<Subscriber> getSendQueueByBotId(int id) {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/send_queue/{id}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<Subscriber>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public List<EventLog> getEventLogsBySubscriberId(int id) {
            List<EventLog> eventLogs = new List<EventLog>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"event_log/subscriber/{id}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<List<EventLog>>(CampaignReactor.getResponse(response.RawBytes));
        }

        public int markBotAsSent(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/sent", Method.POST);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request).Result;
            return JsonConvert.DeserializeObject<int>(CampaignReactor.getResponse(response.RawBytes));
        }


    }
}
