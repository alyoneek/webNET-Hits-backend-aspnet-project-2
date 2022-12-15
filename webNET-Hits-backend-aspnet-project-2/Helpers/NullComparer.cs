namespace webNET_Hits_backend_aspnet_project_2.Helpers
{
    public class NullComparer : IComparer<double?>
    {
        public int Compare(double? x, double? y)
        {
            double xValue = x.HasValue ? x.Value : double.MinValue;
            double yValue = y.HasValue ? y.Value : double.MinValue;
            return xValue.CompareTo(yValue);
        }
    }
}
