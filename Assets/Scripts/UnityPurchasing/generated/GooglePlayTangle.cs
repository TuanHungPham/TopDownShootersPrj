// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("3+g/V6oYjUVt2VLQnSSCp5X1jiUG6ACjn44M8hLoQRITspVRiG1J8lNHbbIqhdEdvo+hppRvhDJBr43BKqmnqJgqqaKqKqmpqAvFdL3kASvnTYmgTKf0ZdZlOsvSCcD3/OzQWJqV2JSQ22zlxWxUK9ttuFd+VEkEjADjp3ugqWX2P89/360qq4YxsrJsJvPgahuFLCFDesz7lc8FOMVJyzedjdpmuza7MAk3EfRSivDdrlFxmCqpipilrqGCLuAuX6WpqamtqKv+HvR78pgGbl7FnDCnBBL70LN1zrBCDtqQ6aMesDHlkdPDhTHYQ1H5nvSOtrj8AAzGYcVFIAy822sHCSN+rwpQXJnzD1Pu5LUlUmdTklnGtEM8E9WQSlDVFaqrqaip");
        private static int[] order = new int[] { 9,8,12,8,8,5,7,12,10,9,12,12,12,13,14 };
        private static int key = 168;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
