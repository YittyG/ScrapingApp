using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using AngleSharp.Dom;

namespace Scraper.Data
{
    public class Article
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }

    }

    public class SData
    {
        public IEnumerable<Article> GetAll(string url)
        {
            var client = new WebClient();
            string site = url;
            var articleText = client.DownloadString(site);
            var parser = new HtmlParser();
            var jibberish = parser.Parse(articleText);
            var articleList = jibberish.QuerySelectorAll("div.post");
            return articleList.Select(GetArticle);


        }

        private Article GetArticle(IElement post)
        {
            var article = new Article();
            var title = post.QuerySelector("a");
            article.Title = title.TextContent;
            article.Url = title.Attributes["href"].Value;

            var pic = post.QuerySelector("img");
            article.Image = pic.Attributes["src"].Value;

            var text = post.QuerySelector("p").TextContent;
            if(text.Length > 11)
            {
                article.Text = (text.Contains("Read more") ? text.Remove(text.Length - 11) : text);

            }

            return article;

            

            
        }
    }
}
