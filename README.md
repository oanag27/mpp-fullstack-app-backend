My project's backend system, developed in C#, serves as the backbone for my task management application. 

It efficiently handles CRUD operations for tasks, including sorting by name for enhanced usability. Additionally,  we leverage Faker.NET to generate realistic fake tasks, enriching our test suites and facilitating rapid prototyping.

# Deployment to AWS
The deployment process involves setting up the backend system on AWS to ensure scalability, reliability, and high availability. We utilize Amazon Web Services (AWS) for hosting our application, which includes Elastic Beanstalk for deploying and managing the backend services.

# To enhance security and provide a structured access control system, we implement role-based access control (RBAC). The system supports three primary roles: User, Admin, and Manager, each with distinct permissions and capabilities.

User: Can read tasksa and view tasks sorted by name.
Manager: Has all the permissions of a User, plus the ability to add, remove, update tasks.
Admin: Possesses all permissions, including managing users and managers.

# Login and Register Based on Roles
We implement a secure authentication and authorization system to manage user access based on their roles. Key features include:
## Registration: New users can register through a secure endpoint. Upon registration, users are assigned the 'User' role by default, or they can choose between 'Manager' or 'Admin'.
## Login: Users authenticate via a secure login endpoint using JWT (JSON Web Token). Upon successful authentication, the token includes user role information.
## Authorization Middleware: Middleware checks user roles on each request to ensure they have the necessary permissions to access the endpoint.
