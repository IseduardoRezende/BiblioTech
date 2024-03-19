using BiblioTechDomain.Interfaces;

namespace BiblioTechDomain.Bases
{
    public class BaseList<ReadModel> where ReadModel : IReadModel
    {
        private IList<ReadModel> _items;

        public BaseList(int currentPage, short itemsPerPage, long totalItems)
        {
            _items = new List<ReadModel>();

            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;

            Pages = Convert.ToInt32(TotalItems / ItemsPerPage);

            if (TotalItems % ItemsPerPage != 0)
                Pages++;
        }

        public int Pages { get; }

        public int CurrentPage { get; }
       
        public short ItemsPerPage { get; }

        public long TotalItems { get; }
        
        public IEnumerable<ReadModel> Items { get { return _items; } }
    
        public void AddItem(ReadModel item)
        {
            _items.Add(item);  
        }

        public void FillItems(IEnumerable<ReadModel> items)
        {
            _items = items.ToList();
        }
    }
}
