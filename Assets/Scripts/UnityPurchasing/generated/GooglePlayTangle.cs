// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ZuXr5NRm5e7mZuXl5EeJOPGoTWcgar+sJlfJYG0PNoC32YNJdIkFh9bZlNjclyCpiSAYZ5ch9BsyGAVI0rjC+vSwTECKLYkJbEDwlydLRW+TpHMb5lTBCSGVHpzRaM7r2bnCaXvRwZYq93r3fEV7XbgexryR4h09SqRM79PCQL5epA1eX/7ZHcQhBb4fCyH+ZsmdUfLD7erYI8h+DePBjbJSuDe+1EoiEonQfOtIXrec/zmC1GblxtTp4u3OYqxiE+nl5eXh5Of8DkKW3KXvUvx9qd2fj8l9lA8dtasBxewA67gpmil2h55FjLuwoJwUMuNGHBDVv0Mfoqj5aR4rH94VivjATK/rN+zlKbpzgzOT4Wbnyn3+/g9wX5ncBhyZWebn5eTl");
        private static int[] order = new int[] { 1,2,5,11,9,10,8,12,8,10,10,11,13,13,14 };
        private static int key = 228;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
