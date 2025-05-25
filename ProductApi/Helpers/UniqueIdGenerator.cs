namespace ProductApi.Helpers
{
    public class UniqueIdGenerator
    {
        private static readonly Random random = new Random();
        public static int GenerateUniqueId(IEnumerable<int> existingIds)
        {
            int id;
            do
            {
                id = random.Next(100000, 999999);
            }while(existingIds.Contains(id));
            return id;
        }
    }
}
