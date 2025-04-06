📓 DiaryApp

> **This project was originally developed in 2020 as part of a university assignment and is shared here to help other students with similar coursework.**

**DiaryApp** is a simple and user-friendly desktop application that allows users to register, log in, and securely create, edit, and delete their personal diary entries.

---

## 🔍 Overview

![image](https://github.com/user-attachments/assets/9a4257f0-d7dd-4bb7-9a3e-cc94cd7fa317)

The application is developed in **C#** and uses **CSV files** for data storage. It follows a **layered architecture** to ensure clean code separation and maintainability.

---

## 🗂️ Project Structure

### 🛠️ Layered Architecture

- **Data Layer**  
  Handles all read/write operations on CSV files.

- **Business Layer**  
  Implements business logic and validates data by communicating with the data layer.

- **Presentation Layer**  
  Manages the user interface and user interactions.

---

## 💾 Data Storage

Access database has been replaced with **CSV files**.

### Files Used:
- `users.csv` – Stores user credentials  
- `diaries.csv` – Stores diary entries

---

## ✨ Features

- User registration and login
- Diary creation, editing, and deletion
- Clean, modular code structure using layered architecture
- Simple and intuitive Windows Forms interface
- Lightweight CSV file storage

---

## ⚙️ Setup Instructions

1. Clone or download the repository  
2. Open `DiaryApp.sln` in Visual Studio  
3. Build and run the project  
4. Ensure `users.csv` and `diaries.csv` are present in the main directory

---

## 📝 Notes

Code is commented for clarity and ease of understanding. This project can serve as a good starting point or reference for students working on similar C# desktop applications.
