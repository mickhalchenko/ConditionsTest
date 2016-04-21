using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExspressionTest
{
    public class Condition
    {
        public int Id { get; set; }
        public String Field { get; set; }

        public String Expression { get; set; }

        public String Value { get; set; }

        public bool Result { get; set; }

        public int RelatedConditionId { get; set; }

        public ConditionRelationType RelationType { get; set; }
        public Func<object, bool> Execute()
        {
            return (userAgent) =>
            {
                if (userAgent == null)
                {
                    return false;
                }

                var props = typeof(UserAgentViewModel).GetProperties();
                var entryArg = string.Empty;

                foreach (var prop in props)
                {
                    if (prop.Name.ToLower().Equals(Field.ToLower()))
                    {
                        entryArg = (string)prop.GetValue(userAgent);
                        break;
                    }
                }


                var result = false;
                var entryArgAsInt = -1;
                int.TryParse(entryArg, out entryArgAsInt);

                int expectedValueAsInt = -1;
                int.TryParse(Value, out expectedValueAsInt);

                switch (Expression)
                {

                    case ">":
                        if (entryArgAsInt != null)
                        {

                            if (expectedValueAsInt > 0 && entryArgAsInt > 0)
                            {
                                result = entryArgAsInt > expectedValueAsInt;
                            }
                        }
                        break;

                    case "<":

                        if (entryArgAsInt != null)
                        {
                            if (expectedValueAsInt > 0 && entryArgAsInt > 0)
                            {
                                result = entryArgAsInt < expectedValueAsInt;
                            }
                        }
                        break;

                    case "==":
                        if (entryArgAsInt != null)
                        {
                            if (expectedValueAsInt > 0 && entryArgAsInt > 0)
                            {
                                result = entryArgAsInt == expectedValueAsInt;
                            }
                        }
                        break;
                    case "=":
                        result = entryArg.Equals(Value);
                        break;

                    case "!=":
                        result = !entryArg.Equals(Value);
                        break;

                    case "contains":
                        result = entryArg.ToString().ToLower().Contains(Value.ToLower());
                        break;
                }

                Result = result;

                return Result;
            };
        }
    }

    public enum ConditionRelationType
    {
        None,
        Or,
        And
    }
}
