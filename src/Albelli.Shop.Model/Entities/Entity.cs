
using Amazon.DynamoDBv2.DataModel;

namespace Albelli.Shop.Model.Entities
{
    public class Entity
    {
        [DynamoDBProperty]
        public EffectiveStatusCode StatusCode { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; set; }

        [DynamoDBProperty]
        public DateTime? ModifiedAt { get; set; }

        [DynamoDBProperty]
        public string CreatedBy { get; set; }

        [DynamoDBProperty]
        public string ModifiedBy { get; set; }
    }
}
