namespace Clinic_Queue_Management.Models
{
    public class ReceptionistQueueViewModel
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string Status { get; set; }
        public string QueueDate { get; set; }
        public int AppointmentId { get; set; }
        public QueueAppointment Appointment { get; set; }
    }

    public class QueueAppointment
    {
        public QueuePatient Patient { get; set; }
    }

    public class QueuePatient
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
