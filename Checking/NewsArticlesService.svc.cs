using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Checking
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [System.Web.Script.Services.ScriptService]
    public class NewsArticlesService : INewsArticlesService
    {
        public List<NewsArticle> GetAllArticles()
        {
            var listOfNewsArticle = new List<NewsArticle>();
            //var dt = dbHelper.GetResultSetFromDB("select * from NewsArticle");
            var dt = DBHelperClass.GetResultedTableWithQuery("select * from NewsArticle");
            var sdr = dt.CreateDataReader();
            while (sdr.Read())
            {
                var newArticle = new NewsArticle();
                newArticle.ID = Convert.ToInt32(sdr["ID"]);
                newArticle.Title = sdr["Title"].ToString();
                newArticle.Description = sdr["Description"].ToString();
                newArticle.ImageURL = sdr["ImageUrl"].ToString();
                listOfNewsArticle.Add(newArticle);
            }
            return listOfNewsArticle.ToList();
        }
        public List<NewsArticle> GetAllArticlesBySearch(string searchTerm)
        {
            var listOfNewsArticle = new List<NewsArticle>();
            //var dt = dbHelper.GetResultSetFromDB("select * from NewsArticle where title like '%" + searchTerm + "%' or description like '%" + searchTerm + "%'");
            var dt = DBHelperClass.GetResultedTableWithQuery("select * from NewsArticle where title like '%" + searchTerm + "%' or description like '%" + searchTerm + "%'");
            var sdr = dt.CreateDataReader();
            while (sdr.Read())
            {
                var newArticle = new NewsArticle();
                newArticle.ID = Convert.ToInt32(sdr["ID"]);
                newArticle.Title = sdr["Title"].ToString();
                newArticle.Description = sdr["Description"].ToString();
                newArticle.ImageURL = sdr["ImageUrl"].ToString();
                listOfNewsArticle.Add(newArticle);
            }
            return listOfNewsArticle.ToList();
        }
        public NewsArticle GetNewsArticleByID(string id)
        {
            //var dt = dbHelper.GetResultSetFromDB("select * from NewsArticle Where ID= " + Convert.ToInt32(id));
            var dt = DBHelperClass.GetResultedTableWithQuery("select * from NewsArticle Where ID= " + Convert.ToInt32(id));
            if (dt.Rows.Count < 0)
                return null;
            var dr = dt.Rows[0];
            return new NewsArticle()
            {
                ID = Convert.ToInt32(dr["ID"]),
                Title = dr["Title"].ToString(),
                Description = dr["Description"].ToString(),
                ImageURL = dr["ImageUrl"].ToString()
            };

        }
        public void AddArticle(NewsArticle newsArticle)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("Insert into NewsArticle (ID,Title,Description,ImageUrl) Values");
            sqlQuery.AppendFormat("('{0}',{1},{2},{3})", newsArticle.ID, newsArticle.Title, newsArticle.Description, newsArticle.ImageURL);
            //dbHelper.ExecuteSqlQuery(sqlQuery.ToString());
            DBHelperClass.ExecuteNonQuery(sqlQuery.ToString());
        }
        public void UpdateArticle(NewsArticle newsArticle)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("Update NewsArticle Set ");
            sqlQuery.AppendFormat("(Title={1},Description={2},ImageUrl={3} where ID='{0}')",
                newsArticle.ID, newsArticle.Title, newsArticle.Description, newsArticle.ImageURL);
            DBHelperClass.ExecuteNonQuery(sqlQuery.ToString());
        }
        public void DeleteArticleByID(string id)
        {
            DBHelperClass.ExecuteNonQuery("delete from NewsArticle where ID= " + Convert.ToInt32(id));
        }
        public int Addition(string a, string b)
        {
            return Convert.ToInt32(a) + Convert.ToInt32(b);
        }
    }
}