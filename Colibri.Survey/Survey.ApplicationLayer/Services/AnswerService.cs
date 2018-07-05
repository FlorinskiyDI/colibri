using Newtonsoft.Json;
using Survey.ApplicationLayer.Dtos.Models.Answers;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Survey.ApplicationLayer.Services
{
    class AnswerService : IAnswerService
    {
        AnswerTypes type;
        private readonly IQuestionOptionService _questionOptionService;
        private readonly IQuestionService _questionService;
        private readonly IOptionChoiceService _optionChoiceService;


        private BaseAnswerModel baseAnswerModel;
        private Dictionary<Type, Action> switchAnswerType;



        public AnswerService(
            IQuestionOptionService questionOptionService,
            IQuestionService questionService,
            IOptionChoiceService optionChoiceService
            )
        {
            _questionOptionService = questionOptionService;
            _questionService = questionService;
            _optionChoiceService = optionChoiceService;

            switchAnswerType = new Dictionary<Type, Action> {
                { typeof(TextAnswerModel), () =>
                    {
                        SaveTextAnswer((TextAnswerModel) baseAnswerModel);
                    }
                },
                //{ typeof(TextAreaQuestionModel), () =>
                //    {
                //        SaveTextAreaQuestion((TextAreaQuestionModel) baseQuestionModel);
                //    }
                //},
                //{ typeof(RadioQuestionModel), () =>
                //    {
                //        SaveRadioQuestion((RadioQuestionModel) baseQuestionModel);
                //    }
                //},
                //{ typeof(CheckBoxQuesstionModel), () =>
                //    {
                //        SaveCheckBoxQuestion((CheckBoxQuesstionModel) baseQuestionModel);
                //    }
                //},
                //{ typeof(DropdownQuestionModel), () =>
                //    {
                //        SaveDropdownQuestion((DropdownQuestionModel) baseQuestionModel);
                //    }
                //},
                {  typeof(GridAnswerModel), () =>
                    {
                        SaveGridRadioAnswer((GridAnswerModel) baseAnswerModel);
                    }
                },
            };
        }





        public List<BaseAnswerModel> GetTypedAnswerList(List<object> survey)
        {
            List<BaseAnswerModel> baseAnswerList = new List<BaseAnswerModel>();

            if (survey != null)
            {
                foreach (var item in survey)
                {
                    BaseAnswerModel question = GetQuestionByType(item.ToString());
                    baseAnswerList.Add(question);
                }
            }

            return baseAnswerList;
        }




        public void SaveAnswerByType(BaseAnswerModel baseAnswer)
        {
            baseAnswerModel = baseAnswer;

            switchAnswerType[baseAnswer.GetType()]();
        }


        public BaseAnswerModel GetQuestionByType(string baseQuestion)
        {
            var baseAnswerM = JsonConvert.DeserializeObject<BaseAnswerModel>(baseQuestion);
            if (Enum.TryParse(baseAnswerM.Type.ToString(), out type))
            {
                switch (type)
                {
                    case AnswerTypes.Textbox:
                        {
                            TextAnswerModel question = JsonConvert.DeserializeObject<TextAnswerModel>(baseQuestion);
                            baseAnswerM = question as BaseAnswerModel;
                            break;
                        }
                    case AnswerTypes.Textarea:
                        {
                            TextAreaAnswerModel question = JsonConvert.DeserializeObject<TextAreaAnswerModel>(baseQuestion);
                            baseAnswerM = question as BaseAnswerModel;
                            break;
                        }
                    case AnswerTypes.Radio:
                        {
                            RadioAnswerModel question = JsonConvert.DeserializeObject<RadioAnswerModel>(baseQuestion);
                            baseAnswerM = question as BaseAnswerModel;
                            break;
                        }
                    case AnswerTypes.Checkbox:
                        {
                            CheckBoxAnswerModel question = JsonConvert.DeserializeObject<CheckBoxAnswerModel>(baseQuestion);
                            baseAnswerM = question as BaseAnswerModel;
                            break;
                        }
                    case AnswerTypes.Dropdown:
                        {
                            DropdownAnswerModel question = JsonConvert.DeserializeObject<DropdownAnswerModel>(baseQuestion);
                            baseAnswerM = question as BaseAnswerModel;
                            break;
                        }
                    case AnswerTypes.GridRadio:
                        {
                            GridAnswerModel question = JsonConvert.DeserializeObject<GridAnswerModel>(baseQuestion);
                            baseAnswerM = question as BaseAnswerModel;

                            break;
                        }
                }
            }
            return baseAnswerM;
        }



        private void SaveTextAnswer(TextAnswerModel data)
        {
            var question = _questionService.GetQuestionById(data.Id);
            var optionChoice = _optionChoiceService.GetListByOptionGroupId(question.OptionGroupId).Result.FirstOrDefault();
            var questionOptionId = _questionOptionService.Add(data, optionChoice.Id);

            //Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.textBox).Result;
            //InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            //var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            //_optionChoiceService.AddAsync(optionGroupId, null);
        }


        private void SaveGridRadioAnswer(GridAnswerModel data)
        {
            
        }




    }
}
