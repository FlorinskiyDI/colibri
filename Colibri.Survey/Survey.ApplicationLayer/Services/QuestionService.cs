using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dataaccesscore.Abstractions.Uow;
using Newtonsoft.Json;
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
        ControlStates state;

        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        protected readonly OptionGroupDefinitions optionGroupDefinitions;


        private Guid pageId;
        private BaseQuestionModel baseQuestionModel;
        private BaseQuestionModel baseQuestionUpdateModel;

        private Dictionary<Type, Action> switchQuestionType;
        private Dictionary<Type, Action> switchUpdateQuestionType;

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


            switchUpdateQuestionType = new Dictionary<Type, Action> {
                { typeof(TextQuestionModel), () =>
                    {
                        UpdateTextQuestion( (TextQuestionModel)baseQuestionUpdateModel );
                    }
                },
                { typeof(TextAreaQuestionModel), () =>
                    {
                        UpdateTextAreaQuestion( (TextAreaQuestionModel)baseQuestionUpdateModel );
                    }
                },
                { typeof(RadioQuestionModel), () =>
                    {
                        UpdateRadioQuestion( (RadioQuestionModel)baseQuestionUpdateModel );
                    }
                },
                { typeof(CheckBoxQuesstionModel), () =>
                    {
                        UpdateCheckboxQuestion( (CheckBoxQuesstionModel)baseQuestionUpdateModel );
                    }
                },
                { typeof(DropdownQuestionModel), () =>
                    {
                        UpdateDropdownQuestion( (DropdownQuestionModel)baseQuestionUpdateModel );
                    }
                },
                {  typeof(GridRadioQuestionModel), () =>
                    {
                        UpdateGridRadioQuestion( (GridRadioQuestionModel)baseQuestionUpdateModel );
                    }
                },
            };
        }

        public IEnumerable<Questions> GetListByPageId(Guid? pageId)
        {
            try
            {


                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    var questions = repositoryQuestion.Query(item => item.PageId == pageId);
                    uow.SaveChanges();
                    return questions;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private IEnumerable<Questions> GetListByParentId(Guid parentId)
        {
            try
            {


                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    var questions = repositoryQuestion.Query(item => item.ParentId == parentId);
                    uow.SaveChanges();
                    return questions;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Questions GetQuestionById(Guid id)
        {
            try
            {
                Questions item;
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    item = repositoryQuestion.Get(id);
                    //await uow.SaveChangesAsync();
                    return item;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteQuestionById(Guid questionId)
        {
            try
            {
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    repositoryQuestion.Remove(questionId);
                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void UpdateQuestion(Questions question)
        {
            try
            {
                using (var uow = UowProvider.CreateUnitOfWork())
                {
                    var repositoryQuestion = uow.GetRepository<Questions, Guid>();
                    repositoryQuestion.Update(question);
                    uow.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
        // save section
        private void SaveTextQuestion(TextQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.textBox).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            Guid id =  _optionChoiceService.AddAsync(optionGroupId, null).Result;
        }

        private void SaveTextAreaQuestion(TextAreaQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.textArea).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            Guid id = _optionChoiceService.AddAsync(optionGroupId, null).Result;
        }

        private void SaveRadioQuestion(RadioQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.radio).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            if (data.Options.Count() > 0)
            {
                _optionChoiceService.AddRange(optionGroupId, data.Options);
            }

            if (data.IsAdditionalAnswer)
            {
                ItemModel item = new ItemModel()
                {
                    Value = "Additional answer radio"
                };
                _optionChoiceService.AddAsync(optionGroupId, item, true);

            }
        }

        private void SaveCheckBoxQuestion(CheckBoxQuesstionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.checkbox).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, true, optionGroupId, type.Id, null).Result;
            if (data.Options.Count() > 0)
            {
                _optionChoiceService.AddRange(optionGroupId, data.Options);
            }

            if (data.IsAdditionalAnswer)
            {
                ItemModel item = new ItemModel()
                {
                    Value = "Additional answer checkbox"
                };
                _optionChoiceService.AddAsync(optionGroupId, item, true);

            }
        }

        private void SaveDropdownQuestion(DropdownQuestionModel data)
        {
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.dropdown).Result;
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            var questionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;
            if (data.Options.Count() > 0)
            {
                _optionChoiceService.AddRange(optionGroupId, data.Options);
            }

            if (data.IsAdditionalAnswer)
            {
                ItemModel item = new ItemModel()
                {
                    Value = "Additional answer dropdown"
                };
                _optionChoiceService.AddAsync(optionGroupId, item, true);

            }
        }

        private void SaveGridRadioQuestion(GridRadioQuestionModel data)
        {

            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            Guid optionGroupId = _optionGroupService.AddAsync(optionGroupDefinitions.gridRadio).Result;
            var parentQuestionId = SaveQuestion(data, false, optionGroupId, type.Id, null).Result;

            if (data.Grid.Rows.Count() > 0)
            {
                var rowTypeName = QuestionTypes.Radio.ToString();
                InputTypesDto rowsType = inputTypeList.Where(item => item.Name == rowTypeName).FirstOrDefault();
                foreach (var item in data.Grid.Rows)
                {

                    BaseQuestionModel rowQuestion = new BaseQuestionModel()
                    {
                        Text = item.Value,
                        Description = "",
                        ControlType = rowTypeName,
                        IsAdditionalAnswer = false,
                        Required = data.Required,
                        Order = 0, // stub
                    };
                    var rowQuestionId = SaveQuestion(rowQuestion, false, optionGroupId, rowsType.Id, parentQuestionId).Result;
                }
            }
            if (data.Grid.Cols.Count() > 0)
            {
                _optionChoiceService.AddRange(optionGroupId, data.Grid.Cols);
            }

            if (data.IsAdditionalAnswer)
            {
                ItemModel item = new ItemModel()
                {
                    Value = "Additional answer gridRadio"
                };
                _optionChoiceService.AddAsync(optionGroupId, item, true);

            }
        }






        // update section
        private void UpdateTextQuestion(TextQuestionModel data)
        {
            var question = GetQuestionById(Guid.Parse(data.Id));

            //IEnumerable<QuestionsDto> questionDto = Mapper.Map<IEnumerable<Questions>, IEnumerable<QuestionsDto>>(items);


            question.Name = data.Text;
            question.Description = data.Description;
            question.Required = data.Required;
            question.OrderNo = data.Order;
            question.AdditionalAnswer = data.IsAdditionalAnswer;

            UpdateQuestion(question);
        }



        private void UpdateTextAreaQuestion(TextAreaQuestionModel data)
        {
            var question = GetQuestionById(Guid.Parse(data.Id));

            question.Name = data.Text;
            question.Description = data.Description;
            question.Required = data.Required;
            question.OrderNo = data.Order;
            question.AdditionalAnswer = data.IsAdditionalAnswer;

            UpdateQuestion(question);
        }


        private void UpdateRadioQuestion(RadioQuestionModel data)
        {
            var question = GetQuestionById(Guid.Parse(data.Id));

            question.Name = data.Text;
            question.Description = data.Description;
            question.Required = data.Required;
            question.OrderNo = data.Order;
            question.AdditionalAnswer = data.IsAdditionalAnswer;
            UpdateQuestion(question);

            var optionChoiseList = _optionChoiceService.GetListByOptionGroupId(question.OptionGroupId).Result;

            foreach (var item in optionChoiseList)
            {
                var result = data.Options.Find(x => x.Id == item.Id.ToString());
                if (result != null)
                {
                    item.Name = result.Value;
                    _optionChoiceService.UpdateOptionChoise(item);
                    data.Options.Remove(result); // Delete an item from updated option list to getting the only new options
                }
                else
                {
                    // delete this item form db
                    _optionChoiceService.DeleteOptionChoise(item);
                }
            }
            _optionChoiceService.AddRange(question.OptionGroupId.Value, data.Options);
        }


        private void UpdateDropdownQuestion(DropdownQuestionModel data)
        {
            var question = GetQuestionById(Guid.Parse(data.Id));

            question.Name = data.Text;
            question.Description = data.Description;
            question.Required = data.Required;
            question.OrderNo = data.Order;
            question.AdditionalAnswer = data.IsAdditionalAnswer;
            UpdateQuestion(question);

            var optionChoiseList = _optionChoiceService.GetListByOptionGroupId(question.OptionGroupId).Result;

            foreach (var item in optionChoiseList)
            {
                var result = data.Options.Find(x => x.Id == item.Id.ToString());
                if (result != null)
                {
                    item.Name = result.Value;
                    _optionChoiceService.UpdateOptionChoise(item);
                    data.Options.Remove(result); // Delete an item from updated option list to getting the only new options
                }
                else
                {
                    // delete this item form db
                    _optionChoiceService.DeleteOptionChoise(item);
                }
            }
            _optionChoiceService.AddRange(question.OptionGroupId.Value, data.Options);
        }





        private void UpdateGridRadioQuestion(GridRadioQuestionModel data)
        {
            var question = GetQuestionById(Guid.Parse(data.Id));

            question.Name = data.Text;
            question.Description = data.Description;
            question.Required = data.Required;
            question.OrderNo = data.Order;
            question.AdditionalAnswer = data.IsAdditionalAnswer;
            UpdateQuestion(question);

            // update grid cols (variants of answers)
            var optionChoiseList = _optionChoiceService.GetListByOptionGroupId(question.OptionGroupId).Result;
            foreach (var item in optionChoiseList)
            {
                var result = data.Grid.Cols.Find(x => x.Id == item.Id.ToString());
                if (result != null)
                {
                    item.Name = result.Value;
                    _optionChoiceService.UpdateOptionChoise(item);
                    data.Grid.Cols.Remove(result); // Delete an item from updated option list to getting the only new options
                }
                else
                {
                    // delete this item from db
                    _optionChoiceService.DeleteOptionChoise(item);
                }
            }
            _optionChoiceService.AddRange(question.OptionGroupId.Value, data.Grid.Cols);

            // update grid rows (nested questions)
            var rowQuestionList = GetListByParentId(question.Id);
            foreach (var item in rowQuestionList)
            {
                var result = data.Grid.Rows.Find(x => x.Id == item.Id.ToString());
                if (result != null)
                {
                    item.Name = result.Value;
                    item.OrderNo = 0; // stub

                    UpdateQuestion(item);
                    data.Grid.Rows.Remove(result); // Delete an item from updated option list to getting the only new options
                }
                else
                {
                    // delete this item from db
                    DeleteQuestionById(item.Id);
                    //_optionChoiceService.DeleteOptionChoise(item);
                }
            }
            if (data.Grid.Rows.Count() > 0)
            {
                foreach (var row in data.Grid.Rows)
                {
                    var rowTypeName = QuestionTypes.Radio.ToString();
                    InputTypesDto rowsType = inputTypeList.Where(item => item.Name == rowTypeName).FirstOrDefault();
                    BaseQuestionModel rowQuestion = new BaseQuestionModel()
                    {
                        Text = row.Value,
                        Description = "",
                        ControlType = rowTypeName,
                        IsAdditionalAnswer = false,
                        Required = data.Required,
                        Order = 0, // stub
                    };
                    var rowQuestionId = SaveQuestion(rowQuestion, false, question.OptionGroupId, rowsType.Id, question.Id).Result;
                }
            }

        }

        private void UpdateCheckboxQuestion(CheckBoxQuesstionModel data)
        {
            var question = GetQuestionById(Guid.Parse(data.Id));

            question.Name = data.Text;
            question.Description = data.Description;
            question.Required = data.Required;
            question.OrderNo = data.Order;
            question.AdditionalAnswer = data.IsAdditionalAnswer;
            UpdateQuestion(question);

            var optionChoiseList = _optionChoiceService.GetListByOptionGroupId(question.OptionGroupId).Result;

            foreach (var item in optionChoiseList)
            {
                var result = data.Options.Find(x => x.Id == item.Id.ToString());
                if (result != null)
                {
                    item.Name = result.Value;
                    _optionChoiceService.UpdateOptionChoise(item);
                    data.Options.Remove(result); // Delete an item from updated option list to getting the only new options
                }
                else
                {
                    // delete this item form db
                    _optionChoiceService.DeleteOptionChoise(item);
                }
            }
            _optionChoiceService.AddRange(question.OptionGroupId.Value, data.Options);
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
                    Label = "",
                    Value = row.Name,
                    Order = 0 // stub
                };
                gridQuestion.Grid.Rows.Add(item);
            }


            return gridQuestion as BaseQuestionModel;
        }

        public void UpdateQuestionByType(BaseQuestionModel baseQuestion, Guid id)
        {
            baseQuestionUpdateModel = baseQuestion;
            pageId = id;
            switchUpdateQuestionType[baseQuestion.GetType()]();
        }





        public void Update(List<BaseQuestionModel> questionList, Guid pageId)
        {


            foreach (var question in questionList)
            {

                if (Enum.TryParse(question.State.ToString(), out state))
                {
                    switch (state)
                    {
                        case ControlStates.Created:
                            {
                                SaveQuestionByType(question, pageId);
                                //var typedQuestion = GetTextQuestion(item, QuestionTypes.Textbox.ToString());
                                //baseQuestions.Add(typedQuestion);
                                break;
                            }
                        case ControlStates.Updated:
                            {
                                UpdateQuestionByType(question, pageId);
                                //var typedQuestion = GetTextareaQuestion(item, QuestionTypes.Textarea.ToString());
                                //baseQuestions.Add(typedQuestion);
                                break;
                            }
                    }
                }
            }
        }
    }
}