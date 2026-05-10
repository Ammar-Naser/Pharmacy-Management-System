# Pharmacy Management System

A desktop-based Pharmacy Management System built using C# and Windows Forms, designed to manage pharmacy operations efficiently, including drugs, customers, employees, and invoices with automatic stock and sales handling.

# Features
 - Manage Employees (Add / Update / Delete / View)
 - Manage Customers
 - Drug Inventory Management
 - Invoice & Sales System
 - Automatic stock updates after sales
 - Customer loyalty points system
 - Restore stock when deleting invoices
 - View reports and data records
    
# Database Design
 - The system uses a relational database, including:
 - EMPLOYEES
 - CUSTOMERS
 - DRUGS
 - DRUG_CATEGORY
 - INVOICE
 - INVOICE_ITEM

# Each invoice automatically updates:
 - stock quantity
 - total price
 - customer loyalty points

# APP Forms:
each implements the CRDU operations
 - Login Screen
 - Dashboard 
 - EMPLOYEES Form
 - CUSTOMERS Form
 - DRUGS Form
 - INVOICE Form

# Project Structure
 - Forms → UI Layer
 - Data Access Layer → Database operations
 - Models → Entities (Customer, Drug, Invoice)
 - Services → Business logic
 - SQL -> table creation and testing tables 

# Technologies Used
 - C#
 - Windows Forms (.NET Framework)
 - SQL Server
 - ADO.NET
