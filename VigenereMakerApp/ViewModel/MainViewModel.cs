using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;

namespace VigenereMakerApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                Title = "Vigenere Maker (DESIGN MODE)";
                RowPattern = "KRYPTOSABCDEFGHIJLMNQUVWXZ";

                VigenereMatrix = SampleMatrix.AllText.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);  // for testing only.

                MatrixSelectedColumn = 4;
                MatrixSelectedRow = 17;

                Paraphrase = "CROSSWORD";

                TopBoxText = "This is only a test";

                BottomBoxText = "This is only a test";

                ProofText = "This is ONLY A PROOF";

            }
            else
            {
                Title = "Vigenere Maker";
                RowPattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                Paraphrase = "SAMPLE";

            

                PropertyChanged += MainViewModel_PropertyChanged;
            }

            SavePattern_Action();
            SaveParaphrase_Action();

            SavePattern = new RelayCommand(SavePattern_Action);
            SaveParaphrase = new RelayCommand(SaveParaphrase_Action);

            ChangeSelectedCol = new RelayCommand<object>(ChangeSelectedCol_Action);
            ChangeSelectedRow = new RelayCommand<object>(ChangeSelectedRow_Action);
        }

        private void ChangeSelectedRow_Action(object index)
        {
            App.Current.Dispatcher.Invoke(method: new Action<object>(item => {
                var new_row = Convert.ToInt32((string)item);
                MatrixSelectedRow = new_row;
            }), args: new[] { index });
        }

        private void ChangeSelectedCol_Action(object index)
        {
            App.Current.Dispatcher.Invoke(method: new Action<object>(item => {
                var new_col = Convert.ToInt32((string)item);
                MatrixSelectedColumn = new_col;
            }), args: new[] { index });
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (IsInDesignMode) return;
            if (e.PropertyName == "TopBoxText")
            {
                TriggerTranslate();

                // hijinx.
            }
        }

        private void TriggerTranslate()
        {
            _BottomBoxText = VigenereTranslate(_TopBoxText);
            _ProofText = VigenereTranslate(_BottomBoxText);
            RaisePropertyChanged("BottomBoxText");
            RaisePropertyChanged("ProofText");
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title == value) return;
                _Title = value;
                RaisePropertyChanged("Title");
            }
        }

        private string _Paraphrase;

        public string Paraphrase
        {
            get { return _Paraphrase; }
            set
            {
                if (_Paraphrase == value) return;
                _Paraphrase = value;
                RaisePropertyChanged("Paraphrase");
            }
        }


        private string _ProofText;
        public string ProofText
        {
            get { return _ProofText; }
            set
            {
                if (_ProofText == value) return;
                _ProofText = value;
                RaisePropertyChanged("ProofText");
            }
        }

        private string _ParaphraseInfinityBreadcrumb;
        public string ParaphraseInfinityBreadcrumb
        {
            get { return _ParaphraseInfinityBreadcrumb; }
            set
            {
                if (_ParaphraseInfinityBreadcrumb == value) return;
                _ParaphraseInfinityBreadcrumb = value;
                RaisePropertyChanged("ParaphraseInfinityBreadcrumb");
            }
        }


        private string _TopBoxText;
        public string TopBoxText
        {
            get { return _TopBoxText; }
            set
            {
                if (_TopBoxText == value) return;
                _TopBoxText = value;
                RaisePropertyChanged("TopBoxText");
            }
        }


        private string _BottomBoxText;
        public string BottomBoxText
        {
            get { return _BottomBoxText; }
            set
            {
                if (_BottomBoxText == value) return;
                _BottomBoxText = value;
                RaisePropertyChanged("BottomBoxText");
            }
        }

        private string _RowPattern;
        public string RowPattern
        {
            get { return _RowPattern; }
            set
            {
                if (_RowPattern == value) return;
                _RowPattern = value;
                RaisePropertyChanged("RowPattern");
            }
        }

        public RelayCommand SavePattern { get; set; }
        public RelayCommand SaveParaphrase { get; set; }
        public RelayCommand<object> ChangeSelectedCol { get; set; }
        public RelayCommand<object> ChangeSelectedRow { get; set; }

        private string[] _VigenereMatrix;
        public string[] VigenereMatrix
        {
            get { return _VigenereMatrix; }
            set
            {
                if (_VigenereMatrix == value) return;
                _VigenereMatrix = value;
                RaisePropertyChanged("VigenereMatrix");
            }
        }

        private int _MatrixSelectedColumn;
        public int MatrixSelectedColumn
        {
            get { return _MatrixSelectedColumn; }
            set
            {
                if (_MatrixSelectedColumn == value) return;
                _MatrixSelectedColumn = value;
                RaisePropertyChanged("MatrixSelectedColumn");
            }
        }

        private int _MatrixSelectedRow;
        public int MatrixSelectedRow
        {
            get { return _MatrixSelectedRow; }
            set
            {
                if (_MatrixSelectedRow == value) return;
                _MatrixSelectedRow = value;
                RaisePropertyChanged("MatrixSelectedRow");
            }
        }

        private bool ValidatePattern()
        {
            // validate that the RowPattern input passes this requirements for the Vigenere Square
            // 1. not blank.
            if (string.IsNullOrWhiteSpace(RowPattern))
            {
                MessageBox.Show("Please enter a sequence in the input box.");

                RowPattern = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                return false;
            }
            // 2. must be 26 letters, no duplicates, no whacky characters.
            if (RowPattern.Length != 26 || !RowPattern.All(ltr => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(ltr.ToString().ToUpper())))
            {
                MessageBox.Show("The sequence must be 26 letters, 1 instance or each letter, no duplicates, and no special characters or numbers.");
                return false;
            }
            return true;
        }

        private void SavePattern_Action()
        {
            if (!ValidatePattern()) return;
            var lines = new List<string>(new[] { RowPattern }); // initialize with the first line.

            for (var offset = 1; offset < RowPattern.Length; offset++)
            {
                var chunk = RowPattern.Substring(0, RowPattern.Length - offset);
                var otherpart = RowPattern.Remove(0, RowPattern.Length - offset);
                var newpattern = otherpart + chunk;
                lines.Add(newpattern);
            }
            VigenereMatrix = lines.ToArray(); // SampleMatrix.AllText.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);  // for testing
            TriggerTranslate();
        }

        private void SaveParaphrase_Action()
        {
            if (string.IsNullOrWhiteSpace(Paraphrase) || Paraphrase.Length < 6)
            {
                MessageBox.Show("Your Paraphrase should be at least 6 characters, letters only.");
                return;
            }
            if (!Paraphrase.All(ltr => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(ltr.ToString().ToUpper())))
            {
                MessageBox.Show("Your Paraphrase should be letters only.");
                return;
            }
            Paraphrase = Paraphrase.ToUpper();
            ParaphraseInfinityBreadcrumb = Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase + Paraphrase;
            TriggerTranslate();
        }

        /// Translational Logic for Vigenere is here. 
        public string VigenereTranslate(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty; // no harm, no foul.

            var map = RowPattern;

            var cleaned = input.ToUpper().Replace(" ", "").Replace("'", "").Replace(",", "").Replace(".", "").Replace("!", "").Replace("-", "").Replace("?", "");
            var phr = Paraphrase.ToUpper();
            int phrase_offset = 0;
            var matrix = VigenereMatrix;

            var buffer = new StringBuilder();

            for(var pos = 0; pos < cleaned.Length; pos++)
            {
                var row_char = cleaned[pos];
                var col_char = phr[phrase_offset++];

                var posX = map.IndexOf(row_char);
                var posY = map.IndexOf(col_char);

                var letter = matrix[posX][posY].ToString();
                buffer.Append(letter);

                if (phrase_offset >= phr.Length) phrase_offset = 0;
            }

            return buffer.ToString();
        }

        
        /// Translational Logic for Vigenere is here. 
    }

}