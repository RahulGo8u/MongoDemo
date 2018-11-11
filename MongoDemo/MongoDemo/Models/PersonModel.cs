using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MongoDemo.Models
{
    public class PersonModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("FullName")]
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [BsonElement("NickName")]
        [DisplayName("Nick Name")]
        public string NickName { get; set; }
        [BsonElement("HomeAdrs")]
        [Required]
        [DisplayName("Home Address")]
        public string HomeAdrs { get; set; }
        [BsonElement("HomePhone")]
        [DisplayName("Home Name")]
        public string HomePhone { get; set; }        
        [BsonElement("MobPhone")]
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [DisplayName("Mobile No")]
        public string MobPhone { get; set; }
        [BsonElement("HomeFax")]
        [DisplayName("Home Fax")]
        public string HomeFax { get; set; }
        [BsonElement("HomeEmail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [DisplayName("Home Email")]
        public string HomeEmail { get; set; }
        [BsonElement("BirthDay")]
        [Required]
        [DisplayName("Birth Date")]
        public string BirthDate { get; set; }
        [BsonElement("SSN")]
        public string SSN { get; set; }
        [BsonElement("PassportNo")]
        [DisplayName("Passport No")]
        public string PassportNo { get; set; }
        [BsonElement("DLNo")]
        [DisplayName("Driver license number")]
        public string DLNo { get; set; }        
    }
}