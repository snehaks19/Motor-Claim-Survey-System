# **Motor Survey System - ASP.NET Web Application**

This repository contains the source code for the **Motor Survey System**, a web-based application designed to manage motor insurance policies, claims, and surveys. The application is developed using **ASP.NET** and utilizes various components to handle the policy, claim, and survey management workflow.

## **Key Features:**

### **Policy Management:**
- Create and manage motor insurance policies.
- Support for policy details like policy number, premium amounts, and policyholder information.
  
### **Claim Management:**
- Allows users to file claims including vehicle information, claim status, loss descriptions, and more.
- Dynamic claim forms with validation to ensure accurate data entry.
- Integration with **Oracle Database** for storing and retrieving claim data.
- Surveys can be attached to claims.

### **Survey Management:**
- Surveys can be created for specific claims, tracking important survey details like item codes, types, unit prices, quantities, and amounts in both **FC (Foreign Currency)** and **LC (Local Currency)**.
- Calculations for **LC** and **FC** amounts based on user input.
- Historical survey data is displayed in a grid with pagination and proper formatting.
- Validation and dynamic form updates based on user actions (like dropdown selection and text input).

### **User Interface:**
- Clean and responsive design using **Bootstrap** for better user experience on various devices.
- Dynamic fields using **AJAX** and **UpdatePanels** for smooth updates without full page reloads.
- Integrated **SweetAlert** for displaying notifications and alerts in a user-friendly manner.

### **Database Integration:**
- Utilizes **Oracle Database** for storing and managing policy, claim, and survey data.
- Stored procedures are also used for inserting and updating records in the database.
- SQL queries for retrieving and displaying historical survey data.

### **ASP.NET Features:**
- **CodeBehind** structure for server-side logic.
- Data binding with **GridView** and dynamic updates using **AJAX** triggers.

### **Security:**
- The system features user authentication with role-based access control, where different types of users (e.g., **User**, **Surveyor**) have different permissions.
- Conditional rendering of navigation links based on the logged-in user type.

### **Extensibility:**
- The code is modular and can be extended for other insurance-related workflows.
- Easy integration with other modules or APIs as needed.

## **Pages Overview:**

### **Policy.aspx:**
- Interface for creating and managing motor insurance policies.
- Dropdown lists and input fields for policyholder and vehicle information.

### **Claim.aspx:**
- A form for filing claims and managing claim records.
- Includes various fields related to the claim, like claim number, policy number, loss details, and claim status.

### **Survey.aspx:**
- A page for managing surveys related to claims.
- Includes fields for selecting item codes, type, unit price, quantity, and calculating LC and FC amounts.
- Historical survey data is displayed in a **GridView** with pagination.

### **SurveyDetails.aspx:**
- Provides a detailed view for managing survey items.
- Includes functionality for adding new items, calculating amounts, and viewing history.

## **Technologies Used:**
- **ASP.NET WebForms** (C#)
- **Bootstrap** for responsive design
- **JavaScript/jQuery** for client-side functionality
- **Oracle Database** for backend data storage
- **SweetAlert** for UI notifications
- **AJAX** for dynamic updates
