using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExspressionTest
{
    public class ConditionContainer
    {
        public ConditionContainer()
        {
            IncludedConditions = new List<Condition>();
        }
        public List<Condition> IncludedConditions { get; set; }
        public bool GroupResult { get; set; }
        public bool UncheckedRelations { get; set; }
        public bool CheckGroupResult()
        {
            GroupResult = CheckConditions();
            if (GroupResult)
            {
                while (UncheckedRelations)
                {
                    for (var i = 0; i < IncludedConditions.Count; i++)
                    {
                        var currentCondition = IncludedConditions[i];
                        var nextConditionId = i + 1;
                        if (nextConditionId != IncludedConditions.Count)
                        {
                            var nextCondition = IncludedConditions[nextConditionId];
                            if (currentCondition.RelatedConditionId == nextCondition.Id)
                            {
                                GroupResult = CheckPair(nextCondition.Id, currentCondition.Id);
                                if (!GroupResult)
                                {
                                    break;
                                }
                            }

                        }
                        else
                        {
                            UncheckedRelations = false;
                        }

                    }
                }
            }
            return GroupResult;
        }
        private bool CheckPair(int parentId, int childId)
        {
            var parentCondition = IncludedConditions.First(item => item.Id == parentId);
            var childCondition = IncludedConditions.First(item => item.Id == childId);
            bool result = false;

            switch (parentCondition.RelationType)
            {
                case ConditionRelationType.And:
                    result = parentCondition.Result && childCondition.Result;
                    break;
                case ConditionRelationType.Or:
                    result = parentCondition.Result || childCondition.Result;
                    break;
                case ConditionRelationType.None:
                    result = false;
                    break;

            }
            return result;
        }
        private bool CheckConditions()
        {
            var userAgent = UserAgentViewModel.DefaultAgentViewModel();
            UncheckedRelations = false;
            var conditonsResult = false;
            if (IncludedConditions.Any())
            {
                foreach (var condition in IncludedConditions)
                {
                    conditonsResult = condition.Execute().Invoke(userAgent);
                    if (!UncheckedRelations)
                    {
                        UncheckedRelations = condition.RelatedConditionId > 0;
                    }
                }

            }
            return conditonsResult;
        }
        
    }
}
