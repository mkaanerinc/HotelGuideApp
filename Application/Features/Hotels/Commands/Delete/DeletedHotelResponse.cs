﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Hotels.Commands.Delete;

public class DeletedHotelResponse
{
    public Guid Id { get; set; }
    public DateTime DeletedDate { get; set; }
}
