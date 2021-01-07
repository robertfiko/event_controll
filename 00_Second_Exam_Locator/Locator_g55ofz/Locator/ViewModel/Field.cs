using System;
using Locator.Model;

namespace Locator.ViewModel
{

    public class Field : ViewModelBase
    {
        private String _text;
        private String _imgsrc;

        public String Text 
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value; 
                    OnPropertyChanged();
                }
            } 
        }

        


        public Int32 X { get; set; }
        public Int32 Y { get; set; }

        public Position Position
        {
            get { return new Position(X,Y); }
            set
            {
                if (!value.Equals(new Position(X,Y)))
                {
                    X = value.X;
                    Y = value.Y;
                    OnPropertyChanged();
                }
            }
        }

        public Int32 Number { get; set; }

        public DelegateCommand StepCommand { get; set; }
    }
}
