using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MyApp.Domain.Documents;
public class SinhVienDocument
{
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId)]
        public string? Id { get; set; }
             [BsonElement("Id_SinhVien")]
    public int IdSinhVien { get; set; }
    public string TenSinhVien { get; set; } = string.Empty;
    public string MaSoSinhVien { get; set; } = string.Empty;
    public List<LopHocDocument> DanhSachLop { get; set; } = new();
}