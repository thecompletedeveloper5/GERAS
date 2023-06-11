using EmergencyRoadsideAssistanceService.DTOs;

namespace EmergencyRoadsideAssistanceService.Helpers
{
    public class AssistantComparer : IComparer<Assistant>
    {
        public int Compare(Assistant? x, Assistant? y)
        {

            if (x == null && y == null) return 0;
            if (x == null && y != null) return 1;
            if (x != null && y == null) return -1;

            var res = x.ClosenessIndex.CompareTo(y.ClosenessIndex);
            return res;
        }
    }
}
