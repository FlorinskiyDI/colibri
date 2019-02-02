using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Survey.Common.Context
{
    public static class NTContext
    {
        private static AsyncLocal<NTContextModel> _megaMineContext = new AsyncLocal<NTContextModel>();
        private static AsyncLocal<NTContextModel> MegaMineContext
        {
            get { return _megaMineContext; }
            set { _megaMineContext = value; }
        }

        public static NTContextModel Context
        {
            get
            {
                //return  (NTContextModel)CallContext.LogicalGetData("MegaMineContext");
                return MegaMineContext.Value;
            }
            set
            {
                NTContextModel model = value;
                if (model == null)
                {
                    model = new NTContextModel();
                }

                NTContextModel contextModel = Context;
                if (contextModel == null)
                {
                    MegaMineContext.Value = model;
                }
                else
                {
                    contextModel = model;
                    MegaMineContext.Value = contextModel;
                    //contextModel = Mapper.Map<NTContextModel, NTContextModel>(model, contextModel);
                }
            }
        }


        private static AsyncLocal<HttpContext> megaMineHttpContext = new AsyncLocal<HttpContext>();
        private static AsyncLocal<HttpContext> MegaMineHttpContext
        {
            get { return megaMineHttpContext; }
            set { megaMineHttpContext = value; }
        }
        public static HttpContext HttpContext
        {
            get
            {
                //return (HttpContext)CallContext.LogicalGetData("HttpContext");
                return MegaMineHttpContext.Value;
            }
            set
            {
                //CallContext.LogicalSetData("HttpContext", value);
                MegaMineHttpContext.Value = value;
            }
        }
    }
}
