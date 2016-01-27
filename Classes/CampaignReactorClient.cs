using BitlyDotNET.Exceptions;
using BitlyDotNET.Implementations;
using BitlyDotNET.Interfaces;
using libCampaignReactor.Models;
using Newtonsoft.Json;
using Renci.SshNet;
using RestSharp;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CampaignReactor.Classes {
    public class CampaignReactorClient {
        //private string host = "localhost";
        //private int port = 3579;

        private const string CMD_GET_IPV4_ADDRESSES = "ifconfig | awk -F \"[: ]+\" '/inet addr:/ { if ($4 != \"127.0.0.1\") print $4 }'";
        private const string CMD_GET_IPV6_ADDRESSES = "ifconfig | awk -F \"[: ]+\" '/inet6 addr:/ { if ($4 != \"127.0.0.1\") print $4 }'";
        private const string YAHOO_IMAP_HOST = "imap.mail.yahoo.com";

        public CampaignReactorClient() {

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
            request.RequestFormat = RestSharp.DataFormat.Json;
        }

        public Uri getAPIUri() {
            return new Uri($"{ConfigurationManager.AppSettings["apiUrl"]}");
        }

        public Campaign getCampaignById(int id) {
            Campaign campaign = new Campaign();
            RestClient client = new RestClient(this.getAPIUri());
            
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"campaign/{id}", Method.GET);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method

            

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<Campaign>(response.Content);
        }

        public List<Campaign> getAllCampaigns() {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Campaign>>(response.Content);
        }

        public List<Campaign> getCampaignsByName(string name) {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"campaign/name/{name}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Campaign>>(response.Content);
        }

        public List<Campaign> getEnabledCampaigns() {
            List<Campaign> campaigns = new List<Campaign>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Campaign>>(response.Content);
        }

        public List<Server> getEnabledServers() {
            //List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Server>>(response.Content);
        }

        public List<Host> getEnabledHosts() {
            //List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Host>>(response.Content);
        }

        public List<Bot> getEnabledBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Bot>>(response.Content);
        }

        public List<BitlyAccount> getEnabledBitlyAccounts() {
            List<BitlyAccount> bitlyAccounts = new List<BitlyAccount>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<BitlyAccount>>(response.Content);
        }

        public List<Bot> getAvailableBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/available", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Bot>>(response.Content);
        }

        public List<Subscriber> getEnabledSubscribers() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/enabled", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Subscriber>>(response.Content);
        }

        public int updateCampaign(Campaign campaign) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(campaign);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int updateServer(Server server) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(server);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int updateBot(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int updateBitlyAccount(BitlyAccount bitlyAccount) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(bitlyAccount);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int updateSubscriber(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/update", Method.POST);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int unsubscribe(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/unsubscribe", Method.POST);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int createCampaign(Campaign campaign) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("campaign/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(campaign);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int createServer(Server server) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(server);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int createBot(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int createBitlyAccount(BitlyAccount bitlyAccount) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(bitlyAccount);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int createSubscriber(Subscriber subscriber) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(subscriber);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public int createHost(Host host) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/create", Method.POST);
            this.initHeaders(request);
            request.AddBody(host);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public Bot getBotById(int id) {
            Bot bot = new Bot();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"bot/{id}", Method.GET);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<Bot>(response.Content);
        }

        public BitlyAccount getBitlyAccountById(int id) {
            BitlyAccount bitlyAccount = new BitlyAccount();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"bitly_account/{id}", Method.GET);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<BitlyAccount>(response.Content);
        }

        public BitlyAccount getNextBitlyAccount() {
            BitlyAccount bitlyAccount = new BitlyAccount();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account/next", Method.GET);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<BitlyAccount>(response.Content);
        }

        public Host getNextHost() {
            Host host = new Host();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("host/next", Method.GET);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<Host>(response.Content);
        }

        public List<Bot> getAllBots() {
            List<Bot> bots = new List<Bot>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Bot>>(response.Content);
        }

        public List<BitlyAccount> getAllBitlyAccounts() {
            List<BitlyAccount> bots = new List<BitlyAccount>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bitly_account", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<BitlyAccount>>(response.Content);
        }

        public Subscriber getSubscriberById(int id) {
            Subscriber subscriber = new Subscriber();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/{id}", Method.GET);
            this.initHeaders(request);



            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Subscriber>(response.Content);
        }

        public Server getServerById(int id) {
            Server server = new Server();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"server/{id}", Method.GET);
            this.initHeaders(request);



            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Server>(response.Content);
        }

        public Host getHostById(int id) {
            Host host = new Host();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"host/{id}", Method.GET);
            this.initHeaders(request);
            //request.AddParameter("id", id); // adds to POST or URL querystring based on Method



            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<Host>(response.Content);
        }

        public List<Server> getAllServers() {
            List<Server> servers = new List<Server>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("server", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Server>>(response.Content);
        }

        public List<Subscriber> getAllSubscribers() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("subscriber", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Subscriber>>(response.Content);
        }

        public List<Subscriber> getSubscribersByBotId(int id) {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/bot/{id}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Subscriber>>(response.Content);
        }

        public List<Subscriber> getSendQueue() {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/send_queue", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Subscriber>>(response.Content);
        }

        public List<Subscriber> getSendQueueByBotId(int id) {
            List<Subscriber> subscribers = new List<Subscriber>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"subscriber/send_queue/{id}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<Subscriber>>(response.Content);
        }

        public List<EventLog> getEventLogsBySubscriberId(int id) {
            List<EventLog> eventLogs = new List<EventLog>();
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest($"event_log/subscriber/{id}", Method.GET);
            this.initHeaders(request);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<List<EventLog>>(response.Content);
        }

        public List<Host> getHosts(Server server) {
            List<Host> hosts = new List<Host>();
            SshClient sshClient = new SshClient(server.address, server.username, server.password);
            sshClient.Connect();

            SshCommand command = sshClient.RunCommand(CampaignReactorClient.CMD_GET_IPV4_ADDRESSES);

            string[] addresses = command.Result.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string address in addresses) {
                Host host = new Host();
                host.serverId = server.id;
                host.address = address;

                host = this.getHostById(this.createHost(host));
                hosts.Add(host);
            }

            return hosts;
        }

        public string shortenUrl(BitlyAccount bitlyAccount, string url) {
            
            try{
                IBitlyService bitlyService = new BitlyService(bitlyAccount.username, bitlyAccount.apiKey);
                url = bitlyService.Shorten(url);
            }
            catch (BitlyDotNETException exception) {
                System.Console.WriteLine($"Bitly Exception: {exception.Reason.ToString()}");
            }
            return url;
        }


        public int markBotAsSent(Bot bot) {
            RestClient client = new RestClient(this.getAPIUri());

            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            RestRequest request = new RestRequest("bot/sent", Method.POST);
            this.initHeaders(request);
            request.AddBody(bot);

            // execute the request
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<int>(response.Content);
        }

        public void harvest(Bot bot) {
            try {
                ImapClient client = new ImapClient(YAHOO_IMAP_HOST, 993, bot.emailAddress, bot.password, AuthMethod.Login, true);
                // Returns a collection of identifiers of all mails matching the specified search criteria.
                IEnumerable<uint> uids = client.Search(SearchCondition.Undeleted());
                // Download mail messages from the default mailbox.
                

                foreach (uint uid in uids) {
                    MailMessage message = client.GetMessage(uid);
                    Subscriber subscriber = new Subscriber();
                    string[] nameParts = message.From.DisplayName.Split(null);
                    subscriber.phoneNumber = null;
                    subscriber.firstName = nameParts[0];
                    subscriber.lastName = ((nameParts.Length > 1) ? nameParts[1] : "");
                    subscriber.emailAddress = message.From.Address;
                   
                    subscriber.botId = bot.id;
                    this.createSubscriber(subscriber);
                    client.DeleteMessage(uid);
                }
                client.Dispose();
            }
            catch (SocketException exception) {
                System.Console.WriteLine($"Error: {exception.Message}");
            }

        }


    }
}
