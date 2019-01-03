using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface  IQuestionService
    {
        BaseQuestionModel GetQuestionByType(string baseQuestion);
        List<BaseQuestionModel> GetTypedQuestionList(PageModel survey);
        void SaveQuestionByType(BaseQuestionModel baseQuestion, Guid pageId);
        Task<List<BaseQuestionModel>> GetTypedQuestionListByPage(Guid pageId);
        void Update(List<BaseQuestionModel> questionList, Guid pageId);
        Task DeleteQuestionById(Guid questionId);
        IEnumerable<Questions> GetListByPageId(Guid? pageId);
        Questions GetQuestionById(Guid id);
        Task<IEnumerable<QuestionsDto>> GetListByBaseQuestion(Guid baseQuestionid);
    }
}
