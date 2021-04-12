using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DynamoDBLibraries.DynamoDb
{
    public interface IPostData
    {
        Task AddPost(JObject postdetails);
    }
}
