using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.Questions;
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
        void DeleteQuestionById(Guid questionId);
    }
}
