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
│ ├── Auth/
│ │ └── Login.razor
│ ├── Sponsorship/
│ │ ├── SponsorshipList.razor
│ │ ├── SponsorshipForm.razor
│ │ └── SponsorshipDetails.razor
│ ├── SponsorshipTypes/
│ │ └── SponsorshipTypes.razor
│
├── Services/
│ ├── AuthService.cs
│ ├── SponsorshipService.cs
│ └── DropdownService.cs
│
├── Models/
│ ├── DTOs/
│ └── ViewModels/
│
├── Shared/
│ ├── MainLayout.razor
│ ├── NavMenu.razor
│ └── LoadingSpinner.razor
│
├── wwwroot/
│ ├── css/
│ ├── js/
│ └── images/
│
└── Program.cs
```

