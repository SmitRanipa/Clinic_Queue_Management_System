namespace Clinic_Queue_Management.Models
{
    public class DoctorQueueViewModel
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string Status { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
    }

    public class AddPrescriptionViewModel
    {
        public int AppointmentId { get; set; }
        public List<PrescriptionMedicine> Medicines { get; set; } = new List<PrescriptionMedicine>();
        public string Notes { get; set; }
    }

    public class AddReportViewModel
    {
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string TestRecommended { get; set; }
        public string Remarks { get; set; }
    }
}
