# Backend System Overview
My project's backend system, developed in C#, serves as the backbone for my task management application.

## Core Functionality:
- CRUD Operations: Efficiently handles Create, Read, Update, and Delete operations for tasks.
- Sorting: Supports sorting tasks by name for enhanced usability.
- Test Data Generation: Leverages Faker.NET to generate realistic fake tasks, enriching our test suites and facilitating rapid prototyping.

## Deployment to AWS:
The deployment process involves setting up the backend system on AWS to ensure scalability, reliability, and high availability. We utilize Amazon Web Services (AWS) for hosting our application, which includes:

Elastic Beanstalk: For deploying and managing the backend services.

## Security and Access Control:
To enhance security and provide a structured access control system, we implement role-based access control (RBAC).

### Role-Based Access Control (RBAC):
The system supports three primary roles:

1. User:
Can read and view tasks sorted by name.
2. Manager:
Inherits User permissions.
Can add, remove, and update tasks.
3. Admin:
Possesses all permissions.
Can manage users and managers.

## Authentication and Authorization:
We implement a secure authentication and authorization system to manage user access based on their roles. Key features include:

- Registration:
New users can register through a secure endpoint.
Users are assigned the 'User' role by default or can choose between 'Manager' or 'Admin'.
- Login:
Users authenticate via a secure login endpoint using JWT (JSON Web Token).
The token includes user role information upon successful authentication.
- Authorization Middleware:
Middleware checks user roles on each request to ensure they have the necessary permissions to access the endpoint.

This comprehensive backend system ensures that the task management application is secure, scalable, and reliable while providing a seamless and user-friendly experience.






