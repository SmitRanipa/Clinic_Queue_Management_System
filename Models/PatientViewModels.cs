namespace Clinic_Queue_Management.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }
        public int PatientId { get; set; }
        public int ClinicId { get; set; }

        public QueueEntryViewModel QueueEntry { get; set; }
    }

    public class QueueEntryViewModel
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string Status { get; set; }
        public string QueueDate { get; set; }
        public int AppointmentId { get; set; }

        public PatientInfo Patient { get; set; }
    }

    public class PatientInfo
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class BookAppointmentViewModel
    {
        public string AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
    }

    public class AppointmentDetailsViewModel
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; }
        public string TimeSlot { get; set; }
        public string Status { get; set; }

        public PrescriptionViewModel Prescription { get; set; }
        public ReportViewModel Report { get; set; }
    }

    public class PrescriptionViewModel
    {
        public List<PrescriptionMedicine> Medicines { get; set; }
        public string Notes { get; set; }
        public object Doctor { get; set; }
    }

    public class PrescriptionMedicine
    {
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
    }

    public class ReportViewModel
    {
        public string Diagnosis { get; set; }
        public string TestRecommended { get; set; }
        public string Remarks { get; set; }
        public object Doctor { get; set; }
    }
}