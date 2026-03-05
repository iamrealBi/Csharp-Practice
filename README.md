# 📚 Side Projects — Thực hành tổng hợp ASP.NET Core

> **Mục tiêu:** Thực hành tổng hợp tất cả kiến thức đã học từ các khóa:
> - C# OOP (7 modules)
> - Introduction to Relational Databases (IBM - 4 modules)
> - Dr. Chuck's PostgreSQL (4 modules)
> - Web Application Development with ASP.NET Core (4 modules)

---

## 📋 Tổng quan 3 Side Projects

| # | Project | Độ khó | Mô tả ngắn |
|---|---------|--------|------------|
| 1 | [TaskFlow — Personal Task Manager](#project-1-taskflow) | ⭐ Beginner | Quản lý công việc cá nhân |
| 2 | [BookHive — Book Review Platform](#project-2-bookhive) | ⭐⭐ Intermediate | Nền tảng review sách |
| 3 | [ShopCore — Mini E-Commerce](#project-3-shopcore) | ⭐⭐⭐ Advanced | Cửa hàng trực tuyến mini |

---

## 🧠 Skill Matrix — Mỗi project thực hành những gì?

| Kỹ năng | Project 1 | Project 2 | Project 3 |
|---------|:---------:|:---------:|:---------:|
| **C# Classes & OOP** | ✅ | ✅ | ✅ |
| **Inheritance / Polymorphism** | 🔸 cơ bản | ✅ | ✅ nâng cao |
| **Interfaces / Abstract Classes** | ❌ | ✅ | ✅ |
| **Collections & LINQ** | ✅ cơ bản | ✅ nâng cao | ✅ nâng cao |
| **Exception Handling** | 🔸 | ✅ | ✅ |
| **SQL CRUD** | ✅ (qua EF) | ✅ | ✅ |
| **Relationships (1:N)** | ✅ | ✅ | ✅ |
| **Relationships (M:N)** | ❌ | ✅ | ✅ |
| **JOINs / GROUP BY** | 🔸 | ✅ | ✅ |
| **Normalization / Constraints** | 🔸 | ✅ | ✅ |
| **MVC Pattern** | ✅ | ✅ | ✅ |
| **Razor Views** | ✅ | ✅ | ✅ |
| **Web API (REST)** | ❌ | ✅ | ✅ |
| **EF Core (ORM)** | ✅ | ✅ | ✅ |
| **Migrations** | ✅ | ✅ | ✅ |
| **Model Binding / Validation** | ✅ | ✅ | ✅ |
| **Identity (Register/Login)** | ✅ | ✅ | ✅ |
| **JWT Authentication** | ❌ | ❌ | ✅ |
| **Role-based Authorization** | 🔸 | ✅ | ✅ |
| **Claims/Policy Authorization** | ❌ | 🔸 | ✅ |
| **AJAX** | 🔸 | ✅ | ✅ |
| **Pagination / Filtering** | ❌ | ✅ | ✅ |
| **Repository Pattern** | ❌ | ✅ | ✅ |
| **Data Seeding** | ✅ | ✅ | ✅ |
| **Security (CSRF, XSS)** | 🔸 | ✅ | ✅ |
| **Caching** | ❌ | ❌ | ✅ |
| **Logging / Auditing** | ❌ | 🔸 | ✅ |
| **Data Encryption** | ❌ | ❌ | ✅ |

> ✅ = đầy đủ &nbsp; 🔸 = cơ bản &nbsp; ❌ = không có trong project này

---

## Project 1: TaskFlow

### 📌 Mô tả
Ứng dụng quản lý công việc cá nhân (Todo App). Người dùng có thể đăng ký tài khoản, đăng nhập, tạo/sửa/xóa tasks, phân loại tasks theo categories, và đánh dấu hoàn thành.

### 🎯 Yêu cầu chức năng

1. **Authentication:** Đăng ký, đăng nhập, đăng xuất (Identity Framework)
2. **Task CRUD:** Tạo, xem, sửa, xóa task
3. **Categories:** Phân loại tasks (Work, Personal, Study, etc.)
4. **Status:** Đánh dấu task Pending / In Progress / Completed
5. **Priority:** Gán mức ưu tiên (Low, Medium, High)
6. **Due Date:** Đặt deadline cho task
7. **My Tasks:** Mỗi user chỉ thấy tasks của mình
8. **Dashboard:** Trang chủ hiển thị tổng quan tasks

### 🗄️ Database Schema

```
┌──────────────┐       ┌──────────────┐
│ AspNetUsers   │       │  Categories  │
│ (Identity)    │       ├──────────────┤
│               │       │ Id (PK)      │
│ Id            │       │ Name         │
│ Email         │       │ Color        │
│ UserName      │       └──────┬───────┘
└──────┬───────┘              │
       │                      │
       │ 1:N                  │ 1:N
       │                      │
┌──────┴──────────────────────┴───────┐
│             TodoItems               │
├─────────────────────────────────────┤
│ Id (PK)                             │
│ Title                               │
│ Description                         │
│ DueDate                             │
│ Priority (enum: Low/Medium/High)    │
│ Status (enum: Pending/InProgress/   │
│         Completed)                  │
│ CreatedAt                           │
│ UserId (FK → AspNetUsers)           │
│ CategoryId (FK → Categories)        │
└─────────────────────────────────────┘
```

### 📂 Cấu trúc thư mục mong muốn
```
project-1-taskflow/
├── Controllers/
│   ├── HomeController.cs
│   └── TodoController.cs
├── Models/
│   ├── TodoItem.cs
│   ├── Category.cs
│   └── Enums.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Views/
│   ├── Home/
│   ├── Todo/
│   └── Shared/
├── wwwroot/css/
├── Program.cs
└── appsettings.json
```

### 💡 Gợi ý khi thực hành
- Bắt đầu từ Model → DbContext → Migration → Controller → View
- Không cần tối ưu ngay, viết code đơn giản nhất có thể
- Sau khi hoàn thành, tự review lại xem đâu có thể cải thiện

---

## Project 2: BookHive

### 📌 Mô tả
Nền tảng review sách cho phép users duyệt danh sách sách, viết đánh giá (review), đánh điểm (rating), và admin quản lý sách/tác giả. Có cả MVC frontend lẫn Web API endpoints.

### 🎯 Yêu cầu chức năng

1. **Authentication & Roles:**
   - 3 roles: Admin, Reviewer, Reader
   - Admin: quản lý sách, tác giả, categories
   - Reviewer: viết reviews, rating
   - Reader: chỉ xem

2. **Book Management (Admin):**
   - CRUD sách (title, description, ISBN, cover image, publish date)
   - Gán tác giả, categories cho sách
   - Data seeding: 20+ sách mẫu

3. **Author Management (Admin):**
   - CRUD tác giả (name, bio, nationality)
   - Quan hệ M:N: một tác giả → nhiều sách, một sách → nhiều tác giả

4. **Categories:**
   - Quan hệ M:N: sách ↔ thể loại (Fiction, Sci-Fi, Programming, etc.)

5. **Reviews & Ratings (Reviewer):**
   - Viết review cho sách (text + rating 1-5 sao)
   - Mỗi reviewer chỉ review 1 lần/sách
   - Hiển thị average rating

6. **Search & Filter:**
   - Tìm kiếm sách theo title, author
   - Filter theo category, rating
   - Pagination (10 sách/trang)

7. **Web API:**
   - `/api/books` — GET, POST, PUT, DELETE
   - `/api/books/{id}/reviews` — GET, POST
   - Swagger documentation

8. **AJAX:**
   - Load reviews bằng AJAX (không reload page)
   - Submit review form bằng AJAX

### 🗄️ Database Schema
```
Users ──1:N──▶ Reviews
Books ──1:N──▶ Reviews
Books ◀──M:N──▶ Authors    (qua BookAuthors junction table)
Books ◀──M:N──▶ Categories (qua BookCategories junction table)
```

### 💡 Gợi ý khi thực hành
- Dùng Repository Pattern + Service Layer
- Implement IBookRepository, IReviewRepository interfaces
- Dùng AutoMapper để map Entity ↔ DTO
- Thực hành LINQ nâng cao: GroupBy, Join, Aggregation

---

## Project 3: ShopCore

### 📌 Mô tả
Cửa hàng trực tuyến mini với đầy đủ chức năng: browse sản phẩm, giỏ hàng, đặt hàng, quản lý đơn hàng (admin), và bảo mật nâng cao. Đây là project tổng hợp cao nhất.

### 🎯 Yêu cầu chức năng

1. **Dual Authentication:**
   - MVC: Identity (cookie-based) cho web frontend
   - API: JWT token cho mobile/SPA clients

2. **Product Management (Admin):**
   - CRUD products (name, description, price, stock, images)
   - Categories (hierarchical: Electronics → Phones → iPhone)
   - Product variants (size, color)

3. **Shopping Cart:**
   - Thêm/xóa/cập nhật số lượng
   - Persistent cart (database-backed cho logged-in users)
   - Session-based cart cho guests

4. **Order Management:**
   - Checkout flow: Cart → Shipping Info → Review → Place Order
   - Order status tracking (Pending → Processing → Shipped → Delivered)
   - Order history cho users
   - Admin: quản lý tất cả orders

5. **Reviews & Ratings:**
   - Chỉ users đã mua mới được review
   - Rating 1-5 sao + text review

6. **Admin Dashboard:**
   - Thống kê: total orders, revenue, top products
   - Charts (dùng Chart.js)
   - User management

7. **Security Nâng cao:**
   - Data encryption cho sensitive fields (email, address)
   - Auditing: log mọi data changes
   - Security headers (CSP, HSTS, X-Content-Type-Options)
   - Rate limiting cho API endpoints
   - Anti-CSRF tokens

8. **Performance:**
   - Caching (memory cache cho product catalog)
   - Eager loading (Include) cho related data
   - Pagination cho tất cả danh sách
   - AsNoTracking cho read-only queries

9. **External Auth (tùy chọn):**
   - Google login

### 🗄️ Database Schema
```
Users ──1:N──▶ Orders ──1:N──▶ OrderItems ◀──N:1── Products
Users ──1:N──▶ Reviews ◀──N:1── Products
Users ──1:N──▶ CartItems ◀──N:1── Products
Products ◀──M:N──▶ Categories (qua ProductCategories)
Products ──1:N──▶ ProductVariants
```

### 💡 Gợi ý khi thực hành
- Áp dụng Clean Architecture (Presentation → Application → Domain → Infrastructure)
- Repository + Unit of Work pattern
- Custom middleware cho logging, security headers
- EF Core Interceptors cho auditing
- Value Converters cho encryption

---

## 🚀 Thứ tự thực hiện khuyến nghị

```
Project 1 (2-3 ngày)   →   Project 2 (5-7 ngày)   →   Project 3 (7-14 ngày)
    ⭐ Beginner              ⭐⭐ Intermediate           ⭐⭐⭐ Advanced
```

> 💡 **Mẹo:** Hoàn thành Project 1 trước, tự review code, nhận ra điểm yếu → Áp dụng cải thiện vào Project 2 → Project 3 là "bản hoàn chỉnh" nhất.
