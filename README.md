# 📚 Library System

A comprehensive **Library Management System** built with **ASP.NET Core Web API** and **Entity Framework Core**, offering a secure and scalable platform for managing books, users, and borrowing operations. The system uses **JWT authentication** for secure access and follows clean architecture and RESTful API best practices.

---

## 📌 Overview

This project enables:
- Robust **book inventory management**
- Secure **user registration and authentication**
- Efficient **borrowing and return workflows**
- **Role-based authorization** for Admins, Staff, and Members

---

## 🛠 Technologies Used

- ✅ ASP.NET Core Web API
- ✅ Entity Framework Core
- ✅ Microsoft SQL Server
- ✅ JWT (JSON Web Token) Authentication
- ✅ C# (.NET 6+)
- ✅ RESTful API design

---

## 🚀 Core Features

### 📘 Book Management
- Add, update, delete books
- Track book status: *Available*, *Borrowed*, *Reserved*
- Manage details: Title, Author, ISBN, etc.

### 👥 User Management
- User registration (Admin, Staff, Member)
- Secure login with JWT authentication
- Role-based access control
- Manage user profiles

### 🔁 Borrowing System
- Book checkout and return operations
- Track borrowing history
- Manage due dates and return status

---

## 🔐 Authentication & Security

The system uses JWT for secure, stateless authentication with:
- Encrypted token generation and validation
- Role-based authorization (Admin, Staff, Member)
- Secure password hashing
- Token expiration handling

Include the JWT token in the request header:
```http
Authorization: Bearer <your-jwt-token>

