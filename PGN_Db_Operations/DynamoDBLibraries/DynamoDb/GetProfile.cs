using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBLibraries.DynamoDb
{
    public class GetProfile: IGetProfile
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        public GetProfile(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }
        public async Task<DynamoTableItems> GetUserProfile(string guid)
        {
            var queryResult = RequestBuilder(guid);
            var result = await ScanAsync(queryResult);
            return new DynamoTableItems
            {
                postItems = result.Items.Select(Map).ToList()
            };
        }
        private LibPostModel Map(Dictionary<string, AttributeValue> result)
        {
            return new LibPostModel
            {
                post_id = result["post_id"].S,
                UserId = result["UserId"].S,
                Text = result["Text"].S,
                ImageUrl = result["ImageUrl"].S,
                Location = result["Location"].S,
                CreatedOn = result["CreatedOn"].S,
                UpdatedOn = result["UpdatedOn"].S
            };
        }
        private async Task<ScanResponse> ScanAsync(ScanRequest request)
        {
            var response = await _dynamoClient.ScanAsync(request);
            return response;
        }
        private ScanRequest RequestBuilder(string guid)
        {
            if (guid !=null)
            {
                return new ScanRequest
                {
                    TableName = "post"
                };
            }
            return new ScanRequest
            {
                TableName = "post",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    {
                        ":v_UserId", new AttributeValue{S = guid }
                    }
                },
                FilterExpression = "UserId = :v_UserId",
                ProjectionExpression = "post_id, UserId, Text, ImageUrl, Location, CreatedOn, UpdatedOn"
            };
        }
    }
    public class DynamoTableItems
    {
        public IEnumerable<LibPostModel> postItems { get; set; }
    }
    public class LibPostModel
    {
        public string post_id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
    }
}
