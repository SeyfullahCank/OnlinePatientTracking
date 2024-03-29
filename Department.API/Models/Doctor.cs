using Department.API.Models.Interfaces;

namespace Department.API.Models
{
    public class Doctor : IHasId
    {
        private Doctor()
        {
            
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
