using KTLT_23880008.Entity;

namespace KTLT_23880008.Utils {
    public static class ArrayHandler {
        public static T[] ArrayAdd<T>(this T[] source, T data) {
            T[] dest = new T[source.Length + 1];
            for (int i = 0; i < dest.Length; i++) {
                dest[i] = source[i];
            }
            dest[dest.Length - 1] = data;
            return dest;
        }

        public static T[] RemoveAt<T>(this T[] source, int index) {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        public static int FindIndex<T>(this T[] source, T data) {
            int index = -1;
            for (int i = 0; i < source.Length; i++) {
                if(source[i] != null && source[i].Equals(data)) {
                    index = i; break;
                };
            }
            return index;
        }
    }
}
    