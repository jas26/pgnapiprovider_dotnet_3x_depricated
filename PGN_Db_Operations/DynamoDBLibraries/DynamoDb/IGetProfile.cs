using System;
using System.Threading.Tasks;

namespace DynamoDBLibraries.DynamoDb
{
    public interface IGetProfile
    {
        Task<DynamoTableItems> GetUserProfile(string guid);
    }
}
