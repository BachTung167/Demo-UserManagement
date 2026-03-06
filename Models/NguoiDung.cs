using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models;

public class NguoiDung
{
    [Key]
    public int Ma { get; set; }

    [Required(ErrorMessage = "Họ tên không được để trống")]
    public string HoTen { get; set; }

    [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
    [DataType(DataType.Date)]
    // Kiểm tra ngày sinh trong Controller hoặc dùng Custom Attribute
    public DateTime? NgaySinh { get; set; }

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Số điện thoại phải từ 10-11 chữ số")]
    public string DienThoai { get; set; }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [RegularExpression(@"^Số nhà .+, tên đường .+, .+/.+$",
        ErrorMessage = "Định dạng đúng: Số nhà..., tên đường..., quận/huyện...")]
    public string DiaChi { get; set; }
}