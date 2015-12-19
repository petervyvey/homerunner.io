
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeRunner.Foundation.Dapper.Filter
{
    public class Criterion<TEntity> 
        : Criterion, ICriterion<TEntity>
        where TEntity : class
    {
        public ICriteria<TEntity> Criteria { get; set; }

        public ICriteria<TEntity> EqualTo(object value)
        {
            this.Operator = Operator.Equals;
            this.Value = value;

            return this.Criteria;
        }

        public ICriteria<TEntity> LessThan(object value)
        {
            this.Operator = Operator.LessThan;
            this.Value = value;

            return this.Criteria;
        }

        public ICriteria<TEntity> LessThanOrEqual(object value)
        {
            this.Operator = Operator.LessThanOrEqual;
            this.Value = value;

            return this.Criteria;
        }

        public ICriteria<TEntity> GreaterThan(object value)
        {
            this.Operator = Operator.GreaterThan;
            this.Value = value;

            return this.Criteria;
        }

        public ICriteria<TEntity> GreaterThanOrEqual(object value)
        {
            this.Operator = Operator.GreaterThanOrEqual;
            this.Value = value;

            return this.Criteria;
        }

        public ICriteria<TEntity> In(IEnumerable<object> value)
        {
            this.Operator = Operator.In;
            this.Value = value;

            return this.Criteria;
        }
    }

    public class Criterion : ICriterion
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Criterion()
            : base() { }

        public string Field { get; set; }

        public Type FieldType { get; set; }

        public Operator Operator { get; set; }

        public object Value { get; set; }

        public override string ToString()
        {
            string value = string.Empty;
            if (this.Operator == Operator.In)
            {
                value = "(" + string.Join(", ", ((IEnumerable<object>)this.Value).Select(x => "'" + x.ToString() + "'")) + ")";
            }
            else
            {
                value = "'" + this.Value.ToString() + "'";
            }

            string criterion = this.Field + this.ConvertOperator() + value;

            return criterion;
        }

        #region - Private & protected methods. -

        /// <summary>
        /// Converts the operator to a SQL operator.
        /// </summary>
        /// <returns>A string containing the SQL operator.</returns>
        private string ConvertOperator()
        {
            string @operator = string.Empty;

            switch (this.Operator)
            {
                case Operator.Equals:
                    @operator = " = ";
                    break;

                case Operator.LessThan:
                    @operator = " < ";
                    break;

                case Operator.LessThanOrEqual:
                    @operator = " <= ";
                    break;

                case Operator.GreaterThan:
                    @operator = " > ";
                    break;

                case Operator.GreaterThanOrEqual:
                    @operator = " >= ";
                    break;

                case Operator.In:
                    @operator = " IN ";
                    break;
            }

            return @operator;
        }

        #endregion
    
    }
}
