using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Queries.GetManagerList;

public class GetListManagerListItemDto
{
    public string CompanyName { get; set; }
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
}
