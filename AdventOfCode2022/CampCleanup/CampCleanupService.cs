﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CampCleanup
{
    public class CampCleanupService : SimplePuzzleService<CampCleanupModel>
    {
        public CampCleanupService(IEnumerable<IPuzzleStrategy<CampCleanupModel>> strategies) : base(strategies)
        {
        }
    }
}
