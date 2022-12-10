using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ConsoleApp4
{
    class Program
    {
        public static string HostUrl = "http://localhost:4040/";

        static void Main()
        {
            TcpListener server = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), 4040);
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a connection");

                using Socket connection = server.AcceptSocket();
                using Stream request = new NetworkStream(connection);
                using StreamReader sr = new StreamReader(request);

                try
                {
                    //Request
                    string? pageRequest = sr.ReadLine();

                    string[] tokens = pageRequest!.Split(' ');
                    string? usedBrowser = "";


                    var i = 0;
                    string? regel = sr.ReadLine();
                    while (!string.IsNullOrEmpty(regel) && !sr.EndOfStream)
                    {
                        regel = sr.ReadLine();
                        if (i == 0) usedBrowser = regel;
                        i++;
                    }

                    string page = tokens[1];

                    switch (page)
                    {
                        case "/teller":
                            Sender.SendHtml(connection, CounterPage.GetHtml());
                            break;
                        case var val when new Regex("(/add\\?)").IsMatch(val):
                            Sender.SendHtml(connection, SevenPage.GetHtml(page));
                            break;
                        case "/":
                            Sender.SendHtml(connection, Default.GetHtml(usedBrowser!));
                            break;
                        case "/contact":
                            Sender.SendHtml(connection, Contact.GetHtml());
                            break;
                        case "/login":
                            Sender.SendHtml(connection, Login.GetHtml());
                            break;
                        case "/main":
                            Sender.SendHtml(connection, TestPage.GetHtml());
                            break;
                        case var val when new Regex("/mijnteller").IsMatch(val):
                            Sender.SendHtml(connection, Teller.GetHtml(page));
                            break;
                        default:
                            Sender.SendHtml(connection, NotFound.GetHtml());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }

    class Sender
    {
        public static void SendHtml(Socket connection, String html)
        {
            connection.Send(Encoding.ASCII.GetBytes(
                "HTTP/1.0 200 OK\r\nContent-Type: text/html\r\nContent-Length: 11\r\n\r\n" +
                html));
        }
    }

    class Teller
    {
        public static string GetHtml(string url)
        {
            var count = getCount(Program.HostUrl + url);

            Console.WriteLine(count);

            return
                $"<p>De Teller staat op: {count}</p><p><a href='{Program.HostUrl + "mijnteller?" + (count + 1)}'> klik hier om te verhogen</a></p>";
        }

        private static int getCount(String input)
        {
            try
            {
                var httpValueCollection = HttpUtility.ParseQueryString(new Uri(input).Query);

                return Convert.ToInt32(httpValueCollection[0]);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    class CounterPage
    {
        private static int Counter;

        public static string GetHtml()
        {
            Counter++;

            return $"<p>{Counter}</p>";
        }
    }

    class SevenPage
    {
        public static string GetHtml(string page)
        {
            return $"<p>{getRegexSum(page)}</p>";
        }
        
        //sum from regex
        private static int getRegexSum(String input)
        {
            string[] t = input.Split("?");

            string pattern = "(?<==)(\\d{1,2})(?!\\d)";
            Regex regex = new Regex(pattern);
            Match[] matches = regex.Matches(t[1]).ToArray();

            List<int> result = new List<int>();

            foreach (var match in matches)
            {
                result.Add(Convert.ToInt32(match.Value));
            }

            int output = result.Sum();
            
            return output;
        }

        //sum from parameter queries
        private static int getQuerySum(String input)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(new Uri(input).Query);

            int result = 0;
            
            foreach (var o in httpValueCollection)
            {
                foreach (var value in httpValueCollection.GetValues((string?)o)!)
                {
                    try
                    {
                        result += Convert.ToInt32(value);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid query added");
                    }
                }
            }

            return result;
        }
    }

    class Contact
    {
        public static string GetHtml()
        {
            return "<META HTTP-EQUIV='Refresh' CONTENT='0; URL=https://google.com/'>";
        }
    }


    class NotFound
    {
        public static string GetHtml()
        {
            return "<h1>404</h1>";
        }
    }
}