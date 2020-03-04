namespace JBoyerLibaray
{

    public class MutableKeyValuePair<TKey, TValue>
    {

        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public MutableKeyValuePair() { }

        public MutableKeyValuePair(TKey key, TValue val)
        {
            this.Key = key;
            this.Value = val;
        }

    }

}
