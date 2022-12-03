namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class PageInfoModel
    {
        public int Size { get; set; } = 5;
        public int Count { get; set; }
        public int Current { get; set; }

        public PageInfoModel(int totalSize, int current)
        {
            Count = (int)Math.Ceiling((double)totalSize / Size);
            Current = current;
        }
    }
}
