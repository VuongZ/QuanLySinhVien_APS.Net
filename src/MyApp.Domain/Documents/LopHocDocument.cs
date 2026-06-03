using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApp.Domain.Documents;

public class LopHocDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public int Id_LopHoc { get; set; }
    public string TenLop { get; set; } = string.Empty;
    public string Phong { get; set; } = string.Empty;

    public List<SinhVienDocument> DanhSachSinhVien { get; set; } = new();
}