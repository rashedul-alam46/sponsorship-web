# Sponsorship Web (Frontend)

## 📌 Overview

Sponsorship Web is a **Blazor WebAssembly frontend application** for the Sponsorship Management System.  
It provides a clean, responsive UI for managing sponsorship requests, approvals, and workflows through role-based access control.

The application communicates with the **Sponsorship Backend API** to perform all operations.

---

## 🚀 Features

- Role-based login (Admin, Manager, Finance, Requestor)
- Create, edit, and submit sponsorship requests
- Workflow tracking (Draft → Approval → Final Status)
- Sponsorship type selection and management
- Dashboard-based navigation
- Form validation with EditForm
- API-driven data handling

---

## 🏗️ Frontend Structure

```
Sponsorship.Web/
│
├── Pages/
│ ├── Login/
│ │ └── Login.razor
│ ├── Sponsorship/
│ │ ├── SponsorshipCreate.razor
│ │ ├── SponsorshipHistoryg.razor
│ │ ├── SponsorshipList.razor
│ │ └── SponsorshipView.razor
│ ├── SponsorshipTypes/
│ │ ├── SponsorshiptypeCreate.razor
│ │ └── SponsorshiptypeList.razor
│
├── Services/
│ ├── AccountAuthService.cs
│ ├── SponsorshipService.cs
│ ├── SponsorshipTypeService.cs
│ └── DropdownService.cs
│
├── Models/
│
├── Layout/
│ ├── MainLayout.razor
│ └── NavMenu.razor
│
├── wwwroot/
│ ├── css/
│ ├── js/
│ └── images/
│
└── Program.cs
```

---

## 👤 Sample Login Users

You can use the following sample credentials for testing:

| Role      | Email         | Password |
| --------- | ------------- | -------- |
| System    | sys@test.com  | 123456   |
| Manager   | man@test.com  | 123456   |
| Finance   | fin@test.com  | 123456   |
| Requestor | user@test.com | 123456   |

---

## ⚙️ Configuration

Update Backend API base URL in wwwroot/appsettings.json:
```
{
  "ApiBaseUrl": "https://localhost:5001"
}
```

## ▶️ How to Run the Frontend

```bash
cd Sponsorship.Web
dotnet restore
dotnet build
dotnet run
```
