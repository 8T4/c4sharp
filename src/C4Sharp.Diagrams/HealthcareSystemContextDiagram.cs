using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using static C4Sharp.Elements.Relationships.Position;

namespace C4Sharp.Diagrams
{
    /// <summary>
    /// Healthcare System Context Diagram - Shows the healthcare system in scope and its relationships with users and other systems
    /// </summary>
    public class HealthcareSystemContextDiagram : ContextDiagram
    {
        protected override string Title => "Healthcare Solution - System Context";
        protected override string Description => "The system context diagram for the healthcare solution showing external users, systems, and their interactions";

        protected override IEnumerable<Structure> Structures => new Structure[]
        {
            // People
            new Person("patient", "Patient", "Healthcare service recipient who accesses medical services and manages their health information"),
            new Person("doctor", "Doctor", "Healthcare provider who diagnoses, treats patients, and manages medical records"),
            new Person("nurse", "Nurse", "Healthcare professional who provides patient care and assists in medical procedures"),
            new Person("admin", "Healthcare Administrator", "Administrative staff who manages healthcare operations and patient data"),

            // Main Healthcare System
            new SoftwareSystem("healthcare_system", "Healthcare Management System", 
                "Comprehensive healthcare platform providing patient management, medical records, appointments, and clinical workflows"),

            // External Systems
            new SoftwareSystem("ehr_system", "Electronic Health Records (EHR)", 
                "External EHR system for storing and managing patient medical records and clinical data"),
            new SoftwareSystem("lab_system", "Laboratory Information System", 
                "External laboratory system for managing lab tests, results, and diagnostic information"),
            new SoftwareSystem("pharmacy_system", "Pharmacy Management System", 
                "External pharmacy system for prescription management and medication dispensing"),
            new SoftwareSystem("insurance_system", "Insurance Claims System", 
                "External insurance system for processing claims, coverage verification, and billing"),
            new SoftwareSystem("billing_system", "Medical Billing System", 
                "External billing system for processing medical charges, payments, and financial transactions"),
            new SoftwareSystem("notification_system", "Notification Service", 
                "External notification system for sending SMS, email, and push notifications")
        };

        protected override IEnumerable<Relationship> Relationships => new[]
        {
            // Patient interactions
            this["patient"] > this["healthcare_system"] | "Books appointments, views medical records, manages health information",
            this["healthcare_system"] > this["patient"] | "Provides health information, appointment confirmations, test results",

            // Doctor interactions
            this["doctor"] > this["healthcare_system"] | "Manages patient care, updates medical records, schedules procedures",
            this["healthcare_system"] > this["doctor"] | "Provides patient information, clinical data, scheduling tools",

            // Nurse interactions
            this["nurse"] > this["healthcare_system"] | "Records patient vitals, administers medications, updates care plans",
            this["healthcare_system"] > this["nurse"] | "Provides patient care instructions, medication schedules, alerts",

            // Administrator interactions
            this["admin"] > this["healthcare_system"] | "Manages system configuration, user accounts, operational reports",
            this["healthcare_system"] > this["admin"] | "Provides system analytics, user management tools, audit logs",

            // External system integrations
            this["healthcare_system"] > this["ehr_system"] | "Synchronizes patient medical records and clinical data",
            this["healthcare_system"] > this["lab_system"] | "Orders lab tests and retrieves results",
            this["healthcare_system"] > this["pharmacy_system"] | "Sends prescriptions and medication orders",
            this["healthcare_system"] > this["insurance_system"] | "Submits claims and verifies coverage",
            this["healthcare_system"] > this["billing_system"] | "Processes medical charges and payments",
            this["healthcare_system"] > this["notification_system"] | "Sends appointment reminders and health alerts"
        };
    }
}
