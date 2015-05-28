using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace rmaWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        public class CompEntity : TableEntity
        {
            public CompEntity(string compName, string Date)
            {
                this.PartitionKey = compName;
                this.RowKey = Date;
            }
            public CompEntity() { }
            public string Amount { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string compName = "1000003455";
            string Date = "201501";
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnectionString"]);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the table.
            CloudTable rmaCompTable = tableClient.GetTableReference("rmaForecastingAmerica");

            // Create a retrieve operation that takes a entity.
            TableOperation retrieveComp = TableOperation.Retrieve<CompEntity>(compName, Date);

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<CompEntity> query = new TableQuery<CompEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, compName));

            foreach (CompEntity entity in rmaCompTable.ExecuteQuery(query))
            {
                //Response.Write("<br />" + entity.PartitionKey);
                Response.Write("<br />" + entity.RowKey);
                Response.Write("<br />" + entity.Amount);
            }

            //Response.Write("Hello");
        }
    }
}