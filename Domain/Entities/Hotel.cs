using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Hotel : Entity<Guid>
{
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public string CompanyName { get; set; }

    public virtual ICollection<ContactInformation> ContactInformations { get; set; }

    public Hotel()
    {
        ContactInformations = new HashSet<ContactInformation>();
    }

    public Hotel(Guid id, string managerFirstName, string managerLastName, string companyName)
    {
        Id = id;
        ManagerFirstName = managerFirstName;
        ManagerLastName = managerLastName;
        CompanyName = companyName;
    }
}
