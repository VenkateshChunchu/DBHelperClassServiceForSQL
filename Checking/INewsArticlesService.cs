using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Checking
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace="Employee")]
    public interface INewsArticlesService
    {
        [OperationContract, WebGet(UriTemplate = "/NewsArticles", ResponseFormat = WebMessageFormat.Json)]
        List<NewsArticle> GetAllArticles();
        
        [OperationContract, WebGet(UriTemplate = "/NewsArticles/{searchTerm}", ResponseFormat = WebMessageFormat.Json)]
        List<NewsArticle> GetAllArticlesBySearch(string searchTerm);
        
        [OperationContract, WebGet(RequestFormat = WebMessageFormat.Json,UriTemplate = "/ViewArticle/{id}", ResponseFormat = WebMessageFormat.Json)]
        NewsArticle GetNewsArticleByID(string id);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/AddArticle", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void AddArticle(NewsArticle newsArticle);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateArticle", Method = "PUT", ResponseFormat = WebMessageFormat.Json)]
        void UpdateArticle(NewsArticle newsArticle);

        [OperationContract, WebInvoke(UriTemplate = "/DeleteArticle/{id}", Method = "DELETE", ResponseFormat = WebMessageFormat.Json)]
        void DeleteArticleByID(string id);
        
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json,UriTemplate = "/Add/{a}/{b}", Method = "GET", ResponseFormat = WebMessageFormat.Xml)]
        int Addition(string a, string b);
    }
}
