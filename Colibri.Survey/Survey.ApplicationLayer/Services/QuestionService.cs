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

        private readonly IOptionGroupService _optionGroupService;
        private readonly IInputTypeService _inputTypeService;
        QuestionTypes type;

        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;


        private Guid pageId;
        private BaseQuestionModel baseQuestionModel;

        private Dictionary<Type, Action> switchQuestionType;
        private IEnumerable<InputTypesDto> inputTypeList;

        public QuestionService(
            IUowProvider uowProvider,
            IMapper mapper,
            IInputTypeService inputTypeService,
            IOptionGroupService optionGroupService
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
            this._inputTypeService = inputTypeService;
            this._optionGroupService = optionGroupService;

            inputTypeList = _inputTypeService.GetAll();



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
                { typeof(GridRadioQuestionModel), () =>
                    {
                        SaveGridRadioQuestion( (GridRadioQuestionModel)baseQuestionModel );
                    }
                },
            };
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


        private async void SaveTextQuestion(TextQuestionModel data)
        {
            Guid optionGroup = await  _optionGroupService.AddAsync();
            InputTypesDto type = inputTypeList.Where(item => item.Name == data.ControlType).FirstOrDefault();
            QuestionsDto questionDto = new QuestionsDto()
            {
                Name = data.Text,
                ParentId = null,
                Description = data.Description,
                Required = data.Required,
                OrderNo = data.Order,
                AdditionalAnswer = data.IsAdditionalAnswer,
                AllowMultipleOptionAnswers = false,
                InputTypesId = type.Id,
                PageId = pageId,
                OptionGroupId = optionGroup
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    Questions pageEntity = Mapper.Map<QuestionsDto, Questions>(questionDto);
                    var repositoryPage = uow.GetRepository<Questions, Guid>();
                    await repositoryPage.AddAsync(pageEntity);
                    await uow.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }

            Console.Write("1111");
        }

        private void SaveTextAreaQuestion(TextAreaQuestionModel data)
        {
            Console.Write("1111");
        }

        private void SaveRadioQuestion(RadioQuestionModel data)
        {
            Console.Write("1111");
        }

        private void SaveCheckBoxQuestion(CheckBoxQuesstionModel data)
        {
            Console.Write("1111");
        }

        private void SaveDropdownQuestion(DropdownQuestionModel data)
        {
            Console.Write("1111");
        }

        private void SaveGridRadioQuestion(GridRadioQuestionModel data)
        {
            Console.Write("1111");
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
            //var type = baseQuestion.GetType();
            switchQuestionType[baseQuestion.GetType()]();

        }
    }
}
