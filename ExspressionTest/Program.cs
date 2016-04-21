using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExspressionTest
{
    class Program
    {

        


        static void Main(string[] args)
        {
          
            //for angular we create conditionsFieldsList (can be translated)
            var conditionFieldsJson = JsonConvert.SerializeObject(typeof (UserAgentViewModel).GetProperties().Select(prop => prop.Name));

            Console.WriteLine();
            Console.WriteLine("conditionFieldsJson is {0}", conditionFieldsJson);
            Console.WriteLine();
            
            
            #region one condition example
            var condition = new Condition()
            {
                Id = 1,
                Field = "OperatingSystem",
                Expression = "=",
                Value = "Windows",
            };

            var userAgent = UserAgentViewModel.DefaultAgentViewModel();

            condition.Execute().Invoke(userAgent);

            Console.WriteLine(condition.Result);

            #endregion


            var conditionsContainer = new ConditionContainer();

            for (int i = 0; i < 5; i++)
            {
                int childId = i + 1;
                var conditionItem = new Condition()
                {
                    Id = i,
                    Field = "OperatingSystem",
                    Expression = "=",
                    Value = "Windows",
                    RelatedConditionId = childId,
                    RelationType = ConditionRelationType.And
                };

                conditionsContainer.IncludedConditions.Add(conditionItem);
            }

            //add bad condition
            //return value must be false
            //conditionsContainer.IncludedConditions.Add(new Condition()
            //{
            //  Id = conditionsContainer.IncludedConditions.Count + 1,
            //  Field = "OperatingSystemVersion",
            //  Expression = "=",
            //  Value = "8.1"
            //
            //});


            //add different condition
            //return value must be true
            conditionsContainer.IncludedConditions.Add(new Condition()
            {
                Id = conditionsContainer.IncludedConditions.Count + 1,
                Field = "OperatingSystemVersion",
                Expression = "==",
                Value = "10"
            });
            

            var groupResult = conditionsContainer.CheckGroupResult();
            
            Console.WriteLine(groupResult);
            Console.WriteLine();
            var conditionsJson = JsonConvert.SerializeObject(conditionsContainer);
            Console.WriteLine(conditionsJson);
            Console.WriteLine();
            Console.WriteLine("Condition count {0}", conditionsContainer.IncludedConditions.Count);
            Console.WriteLine("Size is {0} if x4 byte", conditionsJson.Length * 4);
            Console.WriteLine("Size is {0} if x8 byte", conditionsJson.Length * 8);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
