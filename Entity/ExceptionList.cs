namespace KTLT_23880008.Entity {
    public struct ExceptionList {
        public string[] Exceptions;
        public int Length;

        public ExceptionList() {
            Length = 0;
            Exceptions = new string[0];
        }

        public void AddException(string exception) {
            Length++;
            string[] result = new string[Length];
            for(int i  = 0; i < Exceptions.Length; i++) {
                result[i] = Exceptions[i];
            }
            result[result.Length - 1] = exception;
            Exceptions = result;

        }

        public string[] GetExceptions() { 
            if(Length == 0) return Array.Empty<string>();
            return Exceptions;
        }
    }
}
