namespace DataCollection.Editor.PostgreSQL.BCR_PsqlItems
{
    public abstract class ABcrPsqlInsertItemBuilder<TPsqlItem,TObject>:I_BCR_Psql_InsertItem where TPsqlItem : I_BCR_Psql_InsertItem 
    {
        private readonly TObject _targetObject;
        private TPsqlItem _resultItem;

        // public TObject TargetObject => targetObject;

        public TPsqlItem ResultItem => _resultItem;
 
        protected ABcrPsqlInsertItemBuilder(TObject targetObject)
        {
            this._targetObject = targetObject;
            Create();
        }

        public void Create()
        {
            _resultItem = CreateFrom(_targetObject);
        }

        protected abstract TPsqlItem CreateFrom(TObject robotInfo);
        public string GetTable()
        {
            return _resultItem.GetTable();
        }

        public string[] GetColumNames()
        {
           return _resultItem.GetColumNames();
        }

        public object[] GetValues()
        {
            return _resultItem.GetValues();
        }
    }
}