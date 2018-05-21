using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.Common.Enums;
using Survey.DomainModelLayer.Entities;

namespace Survey.ApplicationLayer.Services
{



    public class QuestionService : IQuestionService
    {
        private readonly IOptionChoiceService _optionChoiceService;
        private readonly IOptionGroupService _optionGroupService;
        private readonly IInputTypeService _inputTypeService;
        QuestionTypes type;

        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        protected readonly OptionGroupDefinitions optionGroupDefinitions;


        private Guid pageId;
        private BaseQuestionModel baseQuestionModel;

        private Dictionary<Type, Action> switchQuestionType;
        private IEnumerable<InputTypesDto> inputTypeList;

        public QuestionService(
            IUowProvider uowProvider,
            IMapper mapper,
            IInputTypeService inputTypeService,
            IOptionGroupService optionGroupService,
            IOptionChoiceService optionChoiceService
        )
        {

            this.UowProvider = uowProvider;
            this.Mapper = mapper;
            this._inputTypeService = inputTypeService;
            this._optionGroupService = optionGroupService;
            this._optionChoiceService = optionChoiceService;

            inputTypeList = _inputTypeService.GetAll();
            optionGroupDefinitions = new OptionGroupDefinitions();


            switchQuestionType = new Dictionary<Type, Action> {
                { typeof(TextQuestionModel), () =>
                    {
                        SaveTextQuestion( (TextQuestionModel)baseQuestionModel );
                    }
                },
                { typeof(TextAreaQuestionModel), () =>
                    {
                        SaveTextAreaQuestion( (TextAreaQuestionModel)baseQuestionModel );
                    }
                },
                { typeof(RadioQuestionModel), () =>
                    {
                        SaveRadioQuestion( (RadioQuestionModel)baseQuestionModel );
                    }
                },
                { typeof(CheckBoxQuesstionModel), () =>
                    {
                        SaveCheckBoxQuestion( (CheckBoxQuesstionModel)baseQuestionModel );
                    }
                },
                { typeof(DropdownQuestionModel), () =>
                    {
                        SaveDropdownQuestion( (DropdownQuestionModel)baseQuestionModel );
                    }
                },
                {  typeof(GridRadioQuestionModel), () =>
                    {
                        SaveGridRadioQuestion( (GridRadioQuestionModel)baseQuestionModel );
                    }
                },
            };
        }




        public async Task<IEnumerable<QuestionsDto>> GetListByBaseQuestion(Guid baseQuestionid)
        {
            try
            {
                IEnumerable<Questions> items;
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    items = await repositoryQuestion.QueryAsync(item => item.ParentId == baseQuestionid);
                    await uow.SaveChangesAsync();
                    IEnumerable<QuestionsDto> questionDto = Mapper.Map<IEnumerable<Questions>, IEnumerable<QuestionsDto>>(items);
                    return questionDto;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<BaseQuestionModel> GetTypedQuestionList(PageModel page)
        {
            List<BaseQuestionModel> baseQuestionList = new List<BaseQuestionModel>();

            if (page.Questions != null)
            {
                foreach (var baseQuestion in page.Questions)
                {
                    BaseQuestionModel question = GetQuestionByType(baseQuestion.ToString());
                    baseQuestionList.Add(question);
                }
            }
            return baseQuestionList;
        }

        private async Task<Guid> SaveQuestion(BaseQuestionModel data, bool IsAllowMultipleOptionAnswers, Guid? optionGroupId, Guid typeid, Guid? ParentId)
        {
            QuestionsDto questionDto = new QuestionsDto()
            {
                Name = data.Text,
                ParentId = ParentId != null ? ParentId : null,
                Description = data.Description,
                Required = data.Required,
                OrderNo = data.Order,
                AdditionalAnswer = data.IsAdditionalAnswer,
                AllowMultipleOptionAnswers = false,
                InputTypesId = typeid,
                PageId = pageId,
                OptionGroupId = optionGroupId
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    Questions questionEntity = Mapper.Map<QuestionsDto, Questions>(questionDto);
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    await repositoryQuestion.AddAsync(questionEntity);
                    await uow.SaveChangesAsync();

                    return questionEntity.Id;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }
        }

        private void SaveTextQuestion(TextQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.textBox).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            _optionChoiceService.AddAsync(optionGroupId, null);
        }

        private void SaveTextAreaQuestion(TextAreaQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.textArea).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            _optionChoiceService.AddAsync(optionGroupId, null);
        }

        private void SaveRadioQuestion(RadioQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.radio).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            if (data.Options.Count() > 0)
            {
                _optionChoiceService.AddRangeAsync(optionGroupId, data.Options);
            }
        }

        private void SaveCheckBoxQuestion(CheckBoxQuesstionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.checkbox).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, true, optionGroupId, type.Id, null).Result;
            if (data.Options.Count() > 0)
            {
                _optionChoiceService.AddRangeAsync(optionGroupId, data.Options);
            }
        }

        private void SaveDropdownQuestion(DropdownQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.dropdown).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            if (data.Options.Count() > 0)
            {
                _optionChoiceService.AddRangeAsync(optionGroupId, data.Options);
            }
        }

        private void SaveGridRadioQuestion(GridRadioQuestionModel data)
        {

            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.gridRadio).Result;
            var parentQuestionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            
            if (data.Grid.Rows.Count() > 0)
            {

                foreach (var item in data.Grid.Rows)
                {
                    
                    BaseQuestionModel rowQuestion = new BaseQuestionModel()
                    {
                        Text = item.Value,
                        Description = "",
                        ControlType = data.ControlType, // took from base question
                        IsAdditionalAnswer = false,
                        Required = data.Required,
                        Order = 0, // stub
                    };
                    var rowQuestionId = SaveQuestion(rowQuestion, false, optionGroupId, type.Id, parentQuestionId).Result;
                }
            }
            if (data.Grid.Cols.Count() > 0)
            {
                _optionChoiceService.AddRangeAsync(optionGroupId, data.Grid.Cols);
            }
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

        public void SaveQuestionByType(BaseQuestionModel baseQuestion, Guid id)
        {
            baseQuestionModel = baseQuestion;
            pageId = id;
            switchQuestionType[baseQuestion.GetType()]();

        }

        public async Task<List<BaseQuestionModel>> GetTypedQuestionListByPage(Guid pageId)
        {
            try
            {
                List<BaseQuestionModel> baseQuestions = new List<BaseQuestionModel>();


                IEnumerable<Questions> items;
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    items = await repositoryQuestion.QueryAsync(item => item.PageId == pageId && item.ParentId == null);
                    await uow.SaveChangesAsync();
                    IEnumerable<QuestionsDto> questionDtoList = Mapper.Map<IEnumerable<Questions>, IEnumerable<QuestionsDto>>(items);

                    foreach (var item in questionDtoList)
                    {
                        var inputType = inputTypeList.Where(i => i.Id == item.InputTypesId).FirstOrDefault();
                        if (Enum.TryParse(inputType.Name, out type))
                        {
                            switch (type)
                            {
                                case QuestionTypes.Textbox:
                                    {
                                        var typedQuestion = GetTextQuestion(item, QuestionTypes.Textbox.ToString());
                                        baseQuestions.Add(typedQuestion);
                                        break;
                                    }
                                case QuestionTypes.Textarea:
                                    {
                                        var typedQuestion = GetTextareaQuestion(item, QuestionTypes.Textarea.ToString());
                                        baseQuestions.Add(typedQuestion);
                                        break;
                                    }
                                case QuestionTypes.Radio:
                                    {
                                        var typedQuestion = GetRadioQuestion(item, QuestionTypes.Radio.ToString());
                                        baseQuestions.Add(typedQuestion);
                                        break;
                                    }
                                case QuestionTypes.Checkbox:
                                    {
                                        var typedQuestion = GetCheckboxQuestion(item, QuestionTypes.Checkbox.ToString());
                                        baseQuestions.Add(typedQuestion);
                                        break;
                                    }
                                case QuestionTypes.Dropdown:
                                    {
                                        var typedQuestion = GetDropdownQuestion(item, QuestionTypes.Dropdown.ToString());
                                        baseQuestions.Add(typedQuestion);
                                        break;
                                    }
                                case QuestionTypes.GridRadio:
                                    {
                                        var typedQuestion = GetGridRadioQuestion(item, QuestionTypes.GridRadio.ToString());
                                        baseQuestions.Add(typedQuestion);
                                        break;
                                    }
                            }
                        }
                    }
                    return baseQuestions;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private BaseQuestionModel GetTextQuestion(QuestionsDto questionDto, string type)
        {
            TextQuestionModel textQuestion = new TextQuestionModel()
            {
                Id = questionDto.Id.ToString(),
                Text = questionDto.Name,
                Order = questionDto.OrderNo,
                ControlType = type,
                Description = questionDto.Description,
                IsAdditionalAnswer = questionDto.AdditionalAnswer,
                Required = questionDto.Required
            };
            return textQuestion as BaseQuestionModel;
        }

        private BaseQuestionModel GetTextareaQuestion(QuestionsDto questionDto, string type)
        {
            TextAreaQuestionModel textareaQuestion = new TextAreaQuestionModel()
            {
                Id = questionDto.Id.ToString(),
                Text = questionDto.Name,
                Order = questionDto.OrderNo,
                ControlType = type,
                Description = questionDto.Description,
                IsAdditionalAnswer = questionDto.AdditionalAnswer,
                Required = questionDto.Required,
            };
            return textareaQuestion as BaseQuestionModel;
        }

        private BaseQuestionModel GetRadioQuestion(QuestionsDto questionDto, string type)
        {
            var options = _optionChoiceService.GetListByOptionGroup(questionDto.OptionGroupId).Result;
            RadioQuestionModel radioQuestion = new RadioQuestionModel()
            {
                Id = questionDto.Id.ToString(),
                Text = questionDto.Name,
                Order = questionDto.OrderNo,
                ControlType = type,
                Description = questionDto.Description,
                IsAdditionalAnswer = questionDto.AdditionalAnswer,
                Required = questionDto.Required,
                Options = options
            };
            return radioQuestion as BaseQuestionModel;
        }

        private BaseQuestionModel GetCheckboxQuestion(QuestionsDto questionDto, string type)
        {
            var options = _optionChoiceService.GetListByOptionGroup(questionDto.OptionGroupId).Result;
            CheckBoxQuesstionModel checkboxQuestion = new CheckBoxQuesstionModel()
            {
                Id = questionDto.Id.ToString(),
                Text = questionDto.Name,
                Order = questionDto.OrderNo,
                ControlType = type,
                Description = questionDto.Description,
                IsAdditionalAnswer = questionDto.AdditionalAnswer,
                Required = questionDto.Required,
                Options = options
            };
            return checkboxQuestion as BaseQuestionModel;
        }

        private BaseQuestionModel GetDropdownQuestion(QuestionsDto questionDto, string type)
        {
            var options = _optionChoiceService.GetListByOptionGroup(questionDto.OptionGroupId).Result;
            DropdownQuestionModel dropdownQuestion = new DropdownQuestionModel()
            {
                Id = questionDto.Id.ToString(),
                Text = questionDto.Name,
                Order = questionDto.OrderNo,
                ControlType = type,
                Description = questionDto.Description,
                IsAdditionalAnswer = questionDto.AdditionalAnswer,
                Required = questionDto.Required,
                Options = options
            };
            return dropdownQuestion as BaseQuestionModel;
        }

        private BaseQuestionModel GetGridRadioQuestion(QuestionsDto questionDto, string type)
        {
            var rowQuestions = GetListByBaseQuestion(questionDto.Id).Result;

            var colOptionChoises = _optionChoiceService.GetListByOptionGroup(questionDto.OptionGroupId).Result;
            GridRadioQuestionModel gridQuestion = new GridRadioQuestionModel()
            {
                Id = questionDto.Id.ToString(),
                Text = questionDto.Name,
                Order = questionDto.OrderNo,
                ControlType = type,
                Description = questionDto.Description,
                IsAdditionalAnswer = questionDto.AdditionalAnswer,
                Required = questionDto.Required,
                Grid = new GridOptionsModel()
                {
                    Rows = new List<ItemModel>(),
                    Cols = colOptionChoises
                }
            };

            foreach (var row in rowQuestions)
            {
                ItemModel item = new ItemModel()
                {
                    Id = row.Id.ToString(),
                    Label = null,
                    Value = row.Name,
                    Order = 0 // stub
                };
                gridQuestion.Grid.Rows.Add(item);
            }
           

            return gridQuestion as BaseQuestionModel;
        }
    }
}
