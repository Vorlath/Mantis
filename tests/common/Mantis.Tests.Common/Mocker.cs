using Moq;

namespace Mantis.Tests.Common
{
    public class Mocker<T>
        where T : class
    {
        private Mock<T>? _mock;
        private T? _object;
        private Mocker<T>? _inherit;

        public Mock<T> Mock
        {
            get
            {
                if (this.Inherit is not null)
                {
                    return this.Inherit.Mock;
                }

                return this._mock ??= new Mock<T>();
            }
        }
        public T Object
        {
            set
            {
                this._object = value;
            }
            get
            {
                if (this.Inherit is not null)
                {
                    return this.Inherit.Object;
                }

                if (this._object is not null)
                {
                    return this._object;
                }

                return this.Mock.Object;
            }
        }

        public Mocker<T>? Inherit
        {
            get => this._inherit;
            set
            {
                // Recusion check
                Mocker<T>? instance = value;
                while (instance is not null)
                {
                    if (instance == this)
                    {
                        throw new ArgumentException("Recursion detected");
                    }

                    instance = instance.Inherit;
                }

                this._inherit = value;
            }
        }
    }
}