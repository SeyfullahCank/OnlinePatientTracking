using Department.API.Models.Interfaces;

namespace Department.API.Models
{
    public class Department : IHasId
    {
        private Department()
        {

        }
        public Guid Id { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentTypeId { get; set; }
        public List<Doctor> Doctors { get; set; }
    }
}
