# **HỆ THỐNG QUẢN LÝ ĐĂNG KÝ CA HỌC SINH VIÊN**

## **1. Giới thiệu**

Đây là một dự án nhỏ nhằm xây dựng hệ thống quản lý đăng ký ca học cho sinh viên. Hệ thống được thiết kế để cung cấp một giao diện hiệu quả cho sinh viên đăng ký các ca học, đồng thời hỗ trợ quản lý các thông tin liên quan đến khóa học, giảng viên và thời khóa biểu. Dự án này tập trung vào việc áp dụng các công nghệ hiện đại và kiến trúc phần mềm tiên tiến để đảm bảo tính mở rộng, bảo trì và hiệu suất cao.

## **2. Công nghệ sử dụng**

Dự án được phát triển với sự kết hợp của các công nghệ backend và frontend mạnh mẽ:

### **2.1. Backend**

*   **ASP.NET Core 8**: Framework mã nguồn mở, đa nền tảng của Microsoft để xây dựng các ứng dụng web và API hiệu suất cao.
*   **Linq (Language Integrated Query)**: Cung cấp một cú pháp thống nhất để truy vấn dữ liệu từ các nguồn khác nhau trong .NET.
*   **EF Core (Entity Framework Core) - DB First**: Một ORM (Object-Relational Mapper) cho phép làm việc với cơ sở dữ liệu bằng các đối tượng .NET, với cách tiếp cận DB First để tạo các lớp mô hình từ cơ sở dữ liệu hiện có.
*   **SQL Server**: Hệ quản trị cơ sở dữ liệu quan hệ được sử dụng để lưu trữ dữ liệu chính của hệ thống.
*   **MongoDB**: Cơ sở dữ liệu NoSQL được sử dụng cho các dữ liệu phi cấu trúc hoặc yêu cầu tính linh hoạt cao.
*   **Mediator in C#**: Một mẫu thiết kế hành vi giúp giảm sự phụ thuộc trực tiếp giữa các đối tượng bằng cách giới thiệu một đối tượng trung gian (mediator).
*   **Clean Architecture**: Một kiến trúc phần mềm tập trung vào việc tách biệt các mối quan tâm, giúp mã nguồn dễ kiểm thử, bảo trì và mở rộng.
*   **CQRS (Command Query Responsibility Segregation)**: Một mẫu kiến trúc tách biệt các hoạt động đọc (queries) và ghi (commands) dữ liệu, giúp tối ưu hóa hiệu suất và khả năng mở rộng.
*   **RabbitMQ**: Một message broker mã nguồn mở, được sử dụng để xử lý các tác vụ bất đồng bộ và giao tiếp giữa các dịch vụ.
*   **Validator in C#**: Thư viện hoặc cơ chế để xác thực dữ liệu đầu vào, đảm bảo tính toàn vẹn và hợp lệ của dữ liệu.
*   **API GRPC**: Một framework RPC (Remote Procedure Call) hiệu suất cao, đa ngôn ngữ, được sử dụng để giao tiếp giữa các dịch vụ.

### **2.2. Frontend**

*   **React**: Thư viện JavaScript phổ biến để xây dựng giao diện người dùng (UI) tương tác và hiệu quả.

## **3. Kiến trúc hệ thống**

Dự án tuân thủ theo **Clean Architecture** và áp dụng mẫu **CQRS** để quản lý luồng dữ liệu và logic nghiệp vụ. Điều này giúp hệ thống có cấu trúc rõ ràng, dễ dàng phát triển, kiểm thử và bảo trì. RabbitMQ được sử dụng để xử lý các sự kiện và tác vụ bất đồng bộ, đảm bảo tính nhất quán và khả năng mở rộng của hệ thống.

## **4. Cài đặt và chạy dự án**

Để cài đặt và chạy dự án, bạn cần thực hiện các bước sau:

### **4.1. Yêu cầu hệ thống**

*   .NET SDK 8.0 trở lên
*   Node.js và npm/yarn
*   SQL Server
*   MongoDB
*   RabbitMQ

### **4.2. Cài đặt Backend**

1.  Clone repository:
    ```bash
    git clone <URL_REPOSITORY_BACKEND>
    cd <TEN_THU_MUC_BACKEND>
    ```
2.  Cập nhật chuỗi kết nối cơ sở dữ liệu trong `appsettings.json`.
3.  Chạy migration (nếu cần) và cập nhật cơ sở dữ liệu:
    ```bash
    dotnet ef database update
    ```
4.  Chạy ứng dụng backend:
    ```bash
    dotnet run
    ```

### **4.3. Cài đặt Frontend**

1.  Di chuyển đến thư mục frontend:
    ```bash
    cd <TEN_THU_MUC_FRONTEND>
    ```
2.  Cài đặt các dependency:
    ```bash
    npm install
    # hoặc
    yarn install
    ```
3.  Chạy ứng dụng frontend:
    ```bash
    npm start
    # hoặc
    yarn start
    ```

## **5. Sử dụng**

Sau khi cả backend và frontend đều đang chạy, bạn có thể truy cập ứng dụng qua trình duyệt tại địa chỉ `http://localhost:3000` (hoặc cổng mặc định của ứng dụng React ).

## **6. Đóng góp**

Mọi đóng góp đều được hoan nghênh! Vui lòng tạo một pull request hoặc mở một issue nếu bạn có bất kỳ đề xuất hoặc phát hiện lỗi nào.

## **7. Giấy phép**

Dự án này được cấp phép theo Giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.

---

**Manus AI**
