#region License

// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------

#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Webcrawler
{
    public class WebCrawler
    {
        public bool Crawling;
        public string PageToRead;

        private readonly HashSet<string> pagesVisited = new HashSet<string>();
        private readonly HashSet<string> pagesToVisit = new HashSet<string>();

        private readonly HttpClient httpClient = new HttpClient();

        private readonly Regex regexLink = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");

        public WebCrawler()
            : this("http://www.google.com")
        {
        }


        public WebCrawler(string pageToRead)
        {
            this.Crawling = true;
            this.pagesToVisit.Add(pageToRead);
            Crawl();
        }


        public void Crawl()
        {
            while (this.Crawling)
            {
                GetHtmlDocuments();               
            }
        }


        public void GetHtmlDocuments()
        {
            Console.WriteLine("Beginning GetHtmlDocuments Batch this might take a while...");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var stack = new ConcurrentStack<string>();

            this.pagesToVisit.AsParallel().ForAll(page =>
            {
                try
                {   
                    var responseResult = this.httpClient.GetAsync(page);
                    using (var memStream = responseResult.Result.Content.ReadAsStreamAsync().Result)
                    {
                        if(responseResult.Result.Content.Headers.ContentType.MediaType == "text/html") 
                        using (var reader = new StreamReader(memStream, Encoding.UTF8, true, 4096))
                        {
                            var data = reader.ReadToEnd();
                            stack.Push(data);

                            Console.WriteLine("Thread {0} : {1}", Thread.CurrentThread.ManagedThreadId, page);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            });
      
            this.pagesVisited.UnionWith(this.pagesToVisit);
            this.pagesToVisit.Clear();
            GetNewLinks(stack);
            if (this.pagesToVisit.Count == 0)
            {
                this.Crawling = false;
                Console.WriteLine("No more pages to crawl on :(");
                Console.WriteLine("I found " + this.pagesVisited.Count + " pages and they are");
                foreach (var page in this.pagesVisited)
                {
                    Console.WriteLine(page);
                }
            }
        }


        public void GetNewLinks(ConcurrentStack<string> content)
        {
            Console.WriteLine("Beginning GetNewLinks(): ");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (content == null)
                return;
            while (content.TryPop(out var page))
            {
                foreach (var match in regexLink.Matches(page))
                {
                    if (!this.pagesToVisit.Contains(match.ToString()) && !this.pagesVisited.Contains(match.ToString()))
                        this.pagesToVisit.Add(match.ToString());
                }
            }
            stopwatch.Stop();
            Console.WriteLine("GetNewLinksTook: " + stopwatch.ElapsedMilliseconds + "ms");
        }
    }
}