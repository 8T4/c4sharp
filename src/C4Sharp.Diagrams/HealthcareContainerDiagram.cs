using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Containers;
using C4Sharp.Elements.Relationships;
using static C4Sharp.Elements.Relationships.Position;

namespace C4Sharp.Diagrams
{
    /// <summary>
    /// Healthcare Container Diagram - Shows the containers within the healthcare system
    /// </summary>
    public class HealthcareContainerDiagram : ContainerDiagram
    {
        protected override string Title => "Healthcare Solution - Container Diagram";
        protected override string Description => "The container diagram for the healthcare solution showing web applications, APIs, databases, and their interactions";

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            // People (external to the system)
            new Person("patient", "Patient", "Healthcare service recipient"),
            new Person("doctor", "Doctor", "Healthcare provider"),
            new Person("nurse", "Nurse", "Healthcare professional"),
            new Person("admin", "Healthcare Administrator", "Administrative staff"),

            // Healthcare System Boundary with Containers
            Bound("healthcare_system", "Healthcare Management System", 
                // Web Applications
                new ClientSideWebApp("patient_portal", "Patient Portal", "React, TypeScript", 
                    "Single-page application providing patients with access to their health information, appointments, and medical records"),
                new ClientSideWebApp("provider_portal", "Provider Portal", "React, TypeScript", 
                    "Single-page application for healthcare providers to manage patients, clinical workflows, and medical documentation"),
                new ClientSideWebApp("admin_portal", "Admin Portal", "React, TypeScript", 
                    "Administrative interface for system configuration, user management, and operational oversight"),

                // Mobile Applications
                new Mobile("mobile_app", "Healthcare Mobile App", "React Native", 
                    "Mobile application for patients to access health services, book appointments, and receive notifications"),

                // Backend Services
                new Microservice("api_gateway", "API Gateway", "Kong, Nginx", 
                    "Central entry point for all API requests, handling authentication, rate limiting, and routing"),
                new Microservice("patient_service", "Patient Management Service", ".NET 8, C#", 
                    "Microservice handling patient registration, profiles, and personal health information"),
                new Microservice("appointment_service", "Appointment Service", ".NET 8, C#", 
                    "Microservice managing appointment scheduling, calendar management, and availability"),
                new Microservice("medical_records_service", "Medical Records Service", ".NET 8, C#", 
                    "Microservice for managing electronic health records, clinical notes, and medical history"),
                new Microservice("billing_service", "Billing Service", ".NET 8, C#", 
                    "Microservice handling medical billing, insurance claims, and payment processing"),
                new Microservice("notification_service", "Notification Service", ".NET 8, C#", 
                    "Microservice for sending notifications via email, SMS, and push notifications"),

                // Databases
                new Container("patient_db", "Patient Database", ContainerType.Database, "PostgreSQL", 
                    "Stores patient information, demographics, and personal health data"),
                new Container("medical_records_db", "Medical Records Database", ContainerType.Database, "PostgreSQL", 
                    "Stores electronic health records, clinical notes, and medical history"),
                new Container("appointment_db", "Appointment Database", ContainerType.Database, "PostgreSQL", 
                    "Stores appointment schedules, availability, and booking information"),
                new Container("billing_db", "Billing Database", ContainerType.Database, "PostgreSQL", 
                    "Stores billing information, insurance claims, and payment records"),

                // Message Queues
                new Container("message_queue", "Message Queue", ContainerType.Queue, "RabbitMQ", 
                    "Handles asynchronous communication between microservices")
            ),

            // External Systems
            new SoftwareSystem("ehr_system", "External EHR System", "Third-party electronic health records system"),
            new SoftwareSystem("lab_system", "Laboratory System", "External laboratory information system"),
            new SoftwareSystem("pharmacy_system", "Pharmacy System", "External pharmacy management system")
        };

        protected override IEnumerable<Relationship> Relationships => new[]
        {
            // User to Web Applications
            this["patient"] > this["patient_portal"] | "Uses web browser to access health information",
            this["doctor"] > this["provider_portal"] | "Uses web browser to manage patients and clinical workflows",
            this["nurse"] > this["provider_portal"] | "Uses web browser for patient care and documentation",
            this["admin"] > this["admin_portal"] | "Uses web browser for system administration",

            // Mobile App Usage
            this["patient"] > this["mobile_app"] | "Uses mobile app for health services",

            // Web Applications to API Gateway
            this["patient_portal"] > this["api_gateway"] | "Makes API calls", "HTTPS",
            this["provider_portal"] > this["api_gateway"] | "Makes API calls", "HTTPS",
            this["admin_portal"] > this["api_gateway"] | "Makes API calls", "HTTPS",
            this["mobile_app"] > this["api_gateway"] | "Makes API calls", "HTTPS",

            // API Gateway to Microservices
            this["api_gateway"] > this["patient_service"] | "Routes patient-related requests", "HTTP/REST",
            this["api_gateway"] > this["appointment_service"] | "Routes appointment requests", "HTTP/REST",
            this["api_gateway"] > this["medical_records_service"] | "Routes medical records requests", "HTTP/REST",
            this["api_gateway"] > this["billing_service"] | "Routes billing requests", "HTTP/REST",
            this["api_gateway"] > this["notification_service"] | "Routes notification requests", "HTTP/REST",

            // Microservices to Databases
            this["patient_service"] > this["patient_db"] | "Reads from and writes to", "SQL/TCP",
            this["appointment_service"] > this["appointment_db"] | "Reads from and writes to", "SQL/TCP",
            this["medical_records_service"] > this["medical_records_db"] | "Reads from and writes to", "SQL/TCP",
            this["billing_service"] > this["billing_db"] | "Reads from and writes to", "SQL/TCP",

            // Microservices to Message Queue
            this["patient_service"] > this["message_queue"] | "Publishes patient events", "AMQP",
            this["appointment_service"] > this["message_queue"] | "Publishes appointment events", "AMQP",
            this["billing_service"] > this["message_queue"] | "Publishes billing events", "AMQP",
            this["notification_service"] < this["message_queue"] | "Consumes notification events", "AMQP",

            // External System Integrations
            this["medical_records_service"] > this["ehr_system"] | "Synchronizes medical records", "HL7 FHIR",
            this["medical_records_service"] > this["lab_system"] | "Retrieves lab results", "HL7 FHIR",
            this["billing_service"] > this["pharmacy_system"] | "Sends prescriptions", "HL7 FHIR"
        };
    }
}
