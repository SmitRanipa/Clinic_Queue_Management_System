# Clinic Queue Management System

A robust full-stack Clinic Queue Management frontend built with ASP.NET Core MVC. This project seamlessly connects to a live backend API (`https://cmsback.sampaarsh.cloud`) to manage patients, queues, prescriptions, reports, and administrative tasks across multiple clinic environments.

This project has been entirely designed and developed by Smit, including all the frontend interfaces, API integrations, and workflow implementations. No AI-assisted tools were used in coding, designing, or writing this project.

The project has been completed as part of the student curriculum and has been evaluated by the faculty. It received a high grade for its functionality, usability, and clean implementation, demonstrating a strong understanding of ASP.NET Core MVC, RESTful API integration, and modern UI design principles.

All code, design decisions, and documentation reflect original work and learning experience.

## Overview

The Clinic Queue Management System features a Multi-Tenant architecture where each clinic independently handles its own operations. Role-based access ensures safe, secure, and isolated experiences tailored to specific roles:

- **Admin**: Has an overarching view of clinic details and manages system users (creates Receptionists, Doctors, and Patients).
- **Patient**: Can safely book appointments, monitor their queue status, and review personalized medical prescriptions and reports. 
- **Receptionist**: Has real-time control over the daily queue list, efficiently transitioning status updates (Waiting → In-Progress/Skipped, In-Progress → Done).
- **Doctor**: Can fetch today's active queue, interact with patient data, and append rich prescriptions and medical reports.

The project leverages a modern, clean UI tailored around highly responsive template interfaces to deliver an exceptional user experience on any device.

## Features

- **Role-Based Workflows**: Independent dashboards specific to the four roles out of the box.
- **Queue Synchronization**: Seamless fetching and pushing of appointment and patient queue statuses.
- **Rich Prescriptions & Reports**: Capable of nesting arrays of prescribed medicines alongside dosages and durations dynamically.
- **RESTful Integration**: Powered efficiently via HTTP requests consuming standard JSON formats.
- **Modern User Interface**: Outfitted with Glassmorphism concepts, dynamic layouts, tailored fonts, and neat sidebars.

## Tech Stack

- **Framework**: .NET 8 / ASP.NET Core MVC (C#)
- **Frontend Utilities**: HTML5, Bootstrap, CSS, JavaScript
- **API Data Handling**: `HttpClient`, `Newtonsoft.Json`

## Setup & Running Locally

Follow the instructions below to get the project working locally on your machine.

### Prerequisites

- Download and install the [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download).
- An IDE correctly configured for ASP.NET code (such as Visual Studio or Visual Studio Code).

### Steps to Run

1. Clone or download the repository to your local machine.
2. Open your preferred command line terminal and navigate into the `Clinic_Queue_Management` directory.
3. Use the .NET CLI environment to install the required packages and build the project:
   ```bash
   dotnet build
   ```
4. Run the project:
   ```bash
   dotnet run
   ```
5. Your terminal will display the local host URL where the project is running
6. Open that URL in your web browser.

## Authentication Details

Registration is centralized under system administrators. Use the credentials given by your administrator to securely log in. 

> **Student login mapping (general example):**
> * Username: `enrollment@darshan.ac.in`
> * Password: `password123`

## Acknowledgements

- Clinic Queue Management backend by the Faculty / Sampaarsh cloud infrastructure.
- Designed & Programmed as part of the student curriculum.
