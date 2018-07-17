using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Webapi.Data3
{
    public partial class Users
    {
        public Users()
        {
            Respondents = new HashSet<Respondents>();
            SurveySections = new HashSet<SurveySections>();
        }

        public Guid Id { get; set; }

        [InverseProperty("User")]
        public ICollection<Respondents> Respondents { get; set; }
        [InverseProperty("User")]
        public ICollection<SurveySections> SurveySections { get; set; }
    }
}
