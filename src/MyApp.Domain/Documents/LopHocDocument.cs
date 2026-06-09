using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApp.Domain.Documents;

public class LopHocDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

         [BsonElement("Id_LopHoc")]

    public int IdLopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;

    public List<SinhVienDocument> DanhSachSinhVien { get; set; } = new();
}