using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Develop.Scripts.Utility
{
    public class Validator
    {
        private readonly string _fieldsNotFilled = "Не все поля заполнены.";

        public MethodResult FiledsFilled(params object[] values)
        {
            foreach (var value in values)
            {
                if (value is int && (int)value == -1)
                    return MethodResult.Fail(_fieldsNotFilled);

                if (value is string && (string)value == "")
                    return MethodResult.Fail(_fieldsNotFilled);
            }

            return MethodResult.Ok();
        }
    }
}
