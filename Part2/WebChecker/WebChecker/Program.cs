using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace WebChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking if the website is responsing.");
            try
            {

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://wnbart-bun-test-app01.azurewebsites.net/");
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine("\r\nYour webb app is up and running. StatusDescription: {0}", webResponse.StatusDescription);
                }
                else
                {
                    Console.WriteLine("\r\nYour web app is having some issues. Please check asap.\r\nStatus Code is {0} and StatusDescription is {1}",
                        webResponse.StatusCode, webResponse.StatusDescription);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("\r\nWebChecker run into a problem. Message: {0}", ex.Message);
            }
        }

    }
}
