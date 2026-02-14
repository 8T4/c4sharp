using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;
using C4Sharp.Elements.Relationships;
using static C4Sharp.Elements.Relationships.Position;

namespace C4Sharp.Diagrams
{
    /// <summary>
    /// Healthcare Component Diagram - Shows the components within the Patient Management Service
    /// </summary>
    public class HealthcareComponentDiagram : ComponentDiagram
    {
        protected override string Title => "Healthcare Solution - Patient Management Service Components";
        protected override string Description => "The component diagram showing the internal structure of the Patient Management Service";

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            // External Users
            new Person("patient", "Patient", "Healthcare service recipient"),
            new Person("doctor", "Doctor", "Healthcare provider"),

            // External Containers
            new Container("patient_portal", "Patient Portal", ContainerType.Spa, "React", "Patient web application"),
            new Container("provider_portal", "Provider Portal", ContainerType.Spa, "React", "Provider web application"),
            new Container("patient_db", "Patient Database", ContainerType.Database, "PostgreSQL", "Patient data storage"),
            new Container("message_queue", "Message Queue", ContainerType.Queue, "RabbitMQ", "Event messaging"),

            // Patient Management Service Components
            Bound("patient_service", "Patient Management Service",
                new Component("patient_controller", "Patient Controller", ".NET 8 Web API", 
                    "REST API controller handling HTTP requests for patient operations"),
                new Component("patient_service_impl", "Patient Service", ".NET 8", 
                    "Business logic service for patient management operations"),
                new Component("patient_repository", "Patient Repository", ".NET 8, Entity Framework", 
                    "Data access layer for patient information"),
                new Component("patient_validator", "Patient Validator", ".NET 8", 
                    "Validates patient data and business rules"),
                new Component("patient_mapper", "Patient Mapper", ".NET 8, AutoMapper", 
                    "Maps between domain models and DTOs"),
                new Component("event_publisher", "Event Publisher", ".NET 8", 
                    "Publishes patient-related events to message queue"),
                new Component("security_service", "Security Service", ".NET 8", 
                    "Handles authentication and authorization for patient data"),
                new Component("audit_service", "Audit Service", ".NET 8", 
                    "Logs all patient data access and modifications for compliance")
            )
        };

        protected override IEnumerable<Relationship> Relationships => new[]
        {
            // External access to service
            this["patient_portal"] > this["patient_controller"] | "Makes API calls for patient operations", "HTTPS/JSON",
            this["provider_portal"] > this["patient_controller"] | "Makes API calls for patient management", "HTTPS/JSON",

            // Internal component relationships
            this["patient_controller"] > this["security_service"] | "Validates user permissions",
            this["patient_controller"] > this["patient_validator"] | "Validates request data",
            this["patient_controller"] > this["patient_service_impl"] | "Delegates business operations",
            this["patient_controller"] > this["patient_mapper"] | "Maps DTOs to domain models",

            this["patient_service_impl"] > this["patient_repository"] | "Performs data operations",
            this["patient_service_impl"] > this["event_publisher"] | "Publishes patient events",
            this["patient_service_impl"] > this["audit_service"] | "Logs patient data access",

            this["patient_repository"] > this["patient_db"] | "Reads from and writes to", "SQL/TCP",
            this["event_publisher"] > this["message_queue"] | "Publishes events", "AMQP",

            // Cross-cutting concerns
            this["security_service"] > this["audit_service"] | "Logs security events",
            this["patient_validator"] > this["audit_service"] | "Logs validation failures"
        };
    }
}
