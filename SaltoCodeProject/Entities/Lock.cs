using System.Collections.Generic;

namespace SaltoCodeProject.Entities
{
    public class Lock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsLocked { get; set; }
        public List<User> AuthorisedUserIds { get; set; }
    }
}
