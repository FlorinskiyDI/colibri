using System.Collections.Generic;

namespace Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager
{
    public class PageSearchEntryDto: SearchEntryDto
    {
        public int PageNumber { get; set; }
        public int PageLength { get; set; }
    }
}
