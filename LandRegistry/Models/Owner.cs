using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace LandRegistry.Models
{
    public partial class Owner : IDataErrorInfo
    {
        public Owner()
        {
            Registry = new HashSet<Registry>();
        }

        public int OwnId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Inn { get; set; }
        public string ConNum { get; set; }
        public string Email { get; set; }        

        public string this[string columnName]
        {
            get
            {
                Error = string.Empty;
                switch (columnName)
                {
                    case "Surname":
                        if (Surname != null)
                        {
                            if (Surname.Length < 2 || Surname.Length > 30)
                            {
                                Error = "Недопустимая длина!";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }

                        if (Surname != null && Surname.Length < 30 && Surname.Length > 2)
                        {
                            if (!char.IsUpper(Surname[0]))
                            {
                                Error = "Первая буква - заглавная.";
                            }
                        }

                        break;

                    case "Name":
                        if (Name != null)
                        {
                            if (Name.Length < 2 || Name.Length > 30)
                            {
                                Error = "Недопустимая длина!";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }

                        if (Name != null && Name.Length < 30 && Name.Length > 2)
                        {
                            if (!char.IsUpper(Name[0]))
                            {
                                Error = "Первая буква - заглавная.";
                            }
                        }

                        break;

                    case "Patronymic":
                        if (Patronymic != null)
                        {
                            if (Patronymic.Length > 30)
                            {
                                Error = "Недопустимая длина!";
                            }
                        }

                        if (Patronymic != null && Patronymic.Length < 30 && Patronymic.Length > 2)
                        {
                            if (!char.IsUpper(Patronymic[0]))
                            {
                                Error = "Первая буква - заглавная";
                            }
                        }
                        break;

                    case "Inn":
                        string innPattern = @"^[\d]{12}$";
                        if (Inn != null)
                        {
                            if (!Regex.IsMatch(Inn, innPattern))
                            {
                                Error = "Недопустимое значение";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }

                        break;

                    case "ConNum":
                        string conNumPattern = @"^(8)[\d]{10}$";
                        if (ConNum != null)
                        {
                            if (!Regex.IsMatch(ConNum, conNumPattern))
                            {
                                Error = "Некорректный номер телефона!";
                            }
                        }
                        else
                        {
                            Error = " ";
                        }

                        break;

                    case "Email":
                        string emailPpattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
                        if (Email != null)
                        {
                            if (!Regex.IsMatch(Email, emailPpattern, RegexOptions.IgnoreCase))
                            {
                                Error = "Некорректный email!";
                            }
                        }

                        break;
                }
                return Error;
            }
        }
        
        [NotMapped]
        public string Error
        {
            get; set;
        }

        public virtual ICollection<Registry> Registry { get; set; }
    }
}
