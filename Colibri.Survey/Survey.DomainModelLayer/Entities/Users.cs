using dataaccesscore.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.DomainModelLayer.Entities
{

    public partial class Users : BaseEntity<Guid>
    {
        public Users()
        {
            Respondents = new HashSet<Respondents>();
            SurveySections = new HashSet<SurveySections>();
        }

        [InverseProperty("User")]
        public ICollection<Respondents> Respondents { get; set; }
        [InverseProperty("User")]
        public ICollection<SurveySections> SurveySections { get; set; }
    }
}
