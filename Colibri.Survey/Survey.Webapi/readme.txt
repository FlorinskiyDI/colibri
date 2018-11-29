// scafold 
Scaffold-DbContext "Data Source=SB-204\SQLEXPRESS ;Initial Catalog=Colibri.Survey; Integrated Security=True; MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data3 -d


// clear survey data
delete from SurveySectoin_Respondents
delete from Answers
delete from Respondents
delete from Question_Options
delete from OptionChoises
delete from Questions
delete from OptionGroups
delete from Pages
delete from SurveySections