using MVVM6.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVM6.ViewModels
{
    public class AddUserVm : ViewModelBase
    {
        public ICommand AdduserCom { get; set; }

        public AddUserVm()
        {
            AdduserCom = new RelayCommand(adduser, canadduser);
        }


        private void adduser(object obj)
        {
            if (ValidUser())
            {
                using mvvmlogindbModel db = new mvvmlogindbModel();
                db.Add(new User(Id, Age, Email, Name));
                db.SaveChanges();

                MainWindowVM.UserList.Add(new User(Id, Age, Email, Name));   

                Id = 0;
                Name = "";
                Age = 0;
                Email = "";
            }            
        }

        private bool ValidUser()
        {
            if (string.IsNullOrEmpty(Name) || (Age == 0) || string.IsNullOrEmpty(Email))
            {
                // 显示错误消息
                MessageBox.Show("请确保姓名、年龄和邮箱都已填写", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool canadduser(object obj)
        {
            return true;
        }


        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

    }
}
