using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Common.Enums;

namespace Survey.ApplicationLayer.Services
{
    public class QuestionService : IQuestionService
    {
        QuestionTypes type;



        public List<BaseQuestionModel> GetTypedQuestionList(SurveyModel survey)
        {
            List<BaseQuestionModel> baseQuestionList = new List<BaseQuestionModel>();
            if (survey.Pages.Count() > 0)
            {
                foreach (var item in survey.Pages)
                {
                    if (item.Questions != null)
                    {
                        foreach (var baseQuestion in item.Questions)
                        {
                            BaseQuestionModel question = GetQuestionByType(baseQuestion.ToString());
                            baseQuestionList.Add(question);
                        }
                    }
                }
            }
            return baseQuestionList;
        }

        public BaseQuestionModel GetQuestionByType(string baseQuestion)
        {
            BaseQuestionModel baseQuestionM = JsonConvert.DeserializeObject<BaseQuestionModel>(baseQuestion);
            if (Enum.TryParse(baseQuestionM.ControlType.ToString(), out type))
            {
                switch (type)
                {
                    case QuestionTypes.Textbox:
                    {
                        TextQuestionModel question = JsonConvert.DeserializeObject<TextQuestionModel>(baseQuestion);
                        baseQuestionM = question as BaseQuestionModel;
                            break;
                    }
                    case QuestionTypes.Textarea:
                    {
                        TextAreaQuestionModel question = JsonConvert.DeserializeObject<TextAreaQuestionModel>(baseQuestion);
                        baseQuestionM = question as BaseQuestionModel;
                            break;
                    }
                    case QuestionTypes.Radio:
                    {
                        RadioQuestionModel question = JsonConvert.DeserializeObject<RadioQuestionModel>(baseQuestion);
                        baseQuestionM = question as BaseQuestionModel;
                            break;
                    }
                    case QuestionTypes.Checkbox:
                    {
                        CheckBoxQuesstionModel question = JsonConvert.DeserializeObject<CheckBoxQuesstionModel>(baseQuestion);
                        baseQuestionM = question as BaseQuestionModel;
                            break;
                    }
                    case QuestionTypes.Dropdown:
                    {
                        DropdownQuestionModel question = JsonConvert.DeserializeObject<DropdownQuestionModel>(baseQuestion);
                        baseQuestionM = question as BaseQuestionModel;
                            break;
                    }
                    case QuestionTypes.GridRadio:
                    {
                        GridRadioQuestionModel question = JsonConvert.DeserializeObject<GridRadioQuestionModel>(baseQuestion);
                        baseQuestionM = question as BaseQuestionModel;

                            break;
                    }
                }
            }
            return baseQuestionM;
        }

       
    }
}
