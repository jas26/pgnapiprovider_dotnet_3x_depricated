using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json.Linq;

namespace DynamoDBLibraries.DynamoDb
{
    public class PostData: IPostData
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        public PostData(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }
        public async Task AddPost(JObject postdetails)
        {
            var postRequest = this.RequestBuilder(postdetails);
            await this.PostDataAsync(postRequest);
        }

        private PutItemRequest RequestBuilder(JObject postdetails)
        {
            var items = new Dictionary<string, AttributeValue>
            {
                {"post_id",  new AttributeValue {S = postdetails["post_id"].ToString()}},
                {"UserId",  new AttributeValue {S = postdetails["UserId"].ToString()}},
                {"Text",  new AttributeValue {S = postdetails["Text"].ToString()}},
                {"ImageUrl",  new AttributeValue {S = postdetails["ImageUrl"].ToString()}},
                {"Location",  new AttributeValue {S = postdetails["Location"].ToString()}},
                {"CreatedOn",  new AttributeValue {S = postdetails["CreatedOn"].ToString()}},
                {"UpdatedOn",  new AttributeValue {S = postdetails["UpdatedOn"].ToString()}}
            };
            return new PutItemRequest
            {
                TableName = "post",
                Item = items,

            };
        }
        private async Task PostDataAsync(PutItemRequest request)
        {
            await _dynamoClient.PutItemAsync(request);
        }
    }
}
