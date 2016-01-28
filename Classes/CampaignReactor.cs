using BitlyDotNET.Exceptions;
using BitlyDotNET.Implementations;
using BitlyDotNET.Interfaces;
using libCampaignReactor.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using Newtonsoft.Json;
using Renci.SshNet;
using RestSharp;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using S22.Imap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

namespace CampaignReactorClient.Classes {
    public class CampaignReactor {
        //private string host = "localhost";
        //private int port = 3579;

        private const string CMD_GET_IPV4_ADDRESSES = "ifconfig | awk -F \"[: ]+\" '/inet addr:/ { if ($4 != \"127.0.0.1\") print $4 }'";
        private const string CMD_GET_IPV6_ADDRESSES = "ifconfig | awk -F \"[: ]+\" '/inet6 addr:/ { if ($4 != \"127.0.0.1\") print $4 }'";
        private const string YAHOO_IMAP_HOST = "imap.mail.yahoo.com";

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

        public List<Host> getHosts(Server server) {
            List<Host> hosts = new List<Host>();
            SshClient sshClient = new SshClient(server.address, server.username, server.password);
            sshClient.Connect();

            SshCommand command = sshClient.RunCommand(CampaignReactor.CMD_GET_IPV4_ADDRESSES);

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
                System.Diagnostics.Debug.WriteLine($"Bitly Exception: {exception.Reason.ToString()}");
            }
            return url;
        }

        private async void sendEmail(libCampaignReactor.Models.Campaign campaign, libCampaignReactor.Models.Bot bot, libCampaignReactor.Models.BitlyAccount bitlyAccount, libCampaignReactor.Models.Host host, libCampaignReactor.Models.Subscriber subscriber) {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress($"{bot.firstName} {bot.lastName}", bot.emailAddress));
            message.To.Add(new MailboxAddress(subscriber.fullName, subscriber.emailAddress));
            message.Subject = "How you doin'?";

            int sendTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string campaignUrl = this.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/event_log/click/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");
            string campaignImageUrl = this.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/event_log/open/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");
            string campaignUnsubscribeUrl = this.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/event_log/unsubscribe/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");
            string campaignUnsubscribeImageUrl = this.shortenUrl(bitlyAccount, $"http://campaignreactor.com:3579/campaign/unsubscribe_image/?cid={campaign.id}&bid={bot.id}&hid={host.id}&svid={host.serverId}&sid={subscriber.id}&uid=1&st={sendTime}");

            message.Body = new BodyBuilder() {
                TextBody = campaign.subject,
                HtmlBody = $"<center><a href=\"{campaignUrl}\">{campaign.subject}</a><br/><br/><a href=\"{campaignUrl}\"><img style=\"width=\"300px\" height=\"200px\" src=\"{campaignImageUrl}\" /></a><br/><br/><a href=\"{campaignUnsubscribeUrl}\"><img style=\"width=\"300px\" height=\"200px\" src=\"{campaignUnsubscribeImageUrl}\" /></a></center>"
            }.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.mail.yahoo.com", 587, false);

            // Note: since we don't have an OAuth2 token, disable
            // the XOAUTH2 authentication mechanism.
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate("joey", "password");

            client.Send(message);
            client.Disconnect(true);

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

        public void harvest(Bot bot) {
            try {
                ImapClient client = new ImapClient();
                client.Connect(YAHOO_IMAP_HOST, 993, true);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(bot.emailAddress, bot.password);

                // The Inbox folder is always available on all IMAP servers...
                IMailFolder inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadWrite);

                System.Diagnostics.Debug.WriteLine("Total messages: {0}", inbox.Count);
                System.Diagnostics.Debug.WriteLine("Recent messages: {0}", inbox.Recent);

                IList<UniqueId> uids = inbox.Search(SearchQuery.NotDeleted);

                foreach (UniqueId uid in uids) {
                    MimeMessage message = inbox.GetMessage(uid);
                    Subscriber subscriber = new Subscriber();
                    //InternetAddressList fromAddresses = message.From.ToArray()[0].Name;
                    //string[] nameParts = message.From.ToArray()[0].Name.Split(null);
                    subscriber.phoneNumber = null;
                    //subscriber.firstName = nameParts[0];
                    //subscriber.lastName = ((nameParts.Length > 1) ? nameParts[1] : "");
                    subscriber.emailAddress = message.From.ToArray()[0].Name;

                    subscriber.botId = bot.id;
                    this.createSubscriber(subscriber);

                    inbox.AddFlags(uid, MessageFlags.Deleted, true, System.Threading.CancellationToken.None);
                    //var email = new Email(date: message.Date, id: message.MessageId, messageBody: message.TextBody, references: message.References);

                }

                
                inbox.Expunge();
                
                client.Disconnect(true);

                /*
                ImapClient client = new ImapClient(YAHOO_IMAP_HOST, 993, bot.emailAddress, bot.password, AuthMethod.Login, true);
                // Returns a collection of identifiers of all mails matching the specified search criteria.

                IEnumerable <uint> uids = client.Search(SearchCondition.Undeleted());
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
                */
            }

            catch (Exception exception) {
                System.Diagnostics.Debug.WriteLine($"Error: {exception.Message}");
            }
            
        }


    }
}
