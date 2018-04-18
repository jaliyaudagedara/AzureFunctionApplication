using System;

namespace AzureFunctionApplication.Common.Domain
{
    public class LeaveRequest
    {
        public LeaveRequest(Employee employee, int noOfDays)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            if (noOfDays == 0)
            {
                throw new ArgumentException(nameof(employee));
            }

            Id = Guid.NewGuid().ToString();
            Employee = employee;
            NoOfDays = noOfDays;
            ReceivedDateTime = DateTime.Now;
        }

        public string Id { get; }
        public Employee Employee { get; }
        public int NoOfDays { get; }
        public DateTime ReceivedDateTime { get; }
    }
}
