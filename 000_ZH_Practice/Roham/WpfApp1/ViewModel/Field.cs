using System;
using RoboChase.ViewModel;
using Roham.Persistance;

namespace Roham.ViewModel
{

    public class Field : ViewModelBase
    {
        private String _text;
        private String _color;
        public Int32 X { get; set; }
        public Int32 Y { get; set; }

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

        public String BackgroundColor
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }


    

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
