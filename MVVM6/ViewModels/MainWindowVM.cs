using MVVM6.Commands;

using MVVM6.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace MVVM6.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public User SelectUser { get; set; }

        //ObservableCollection这个类实现了INotifyCollectionChanged这个接口
        public static ObservableCollection<User> UserList { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand RemoveUserCommand { get; set; }

        public MainWindowVM()
        {
            using mvvmlogindbModel context = new mvvmlogindbModel();
            var users = context.Users.ToList();
            UserList = new ObservableCollection<User>(users);            

            AddUserCommand = new RelayCommand(AddUser, CanAddUser);
            RemoveUserCommand = new RelayCommand(RemoveUser, CanRemoveUser);
        }

        private bool CanRemoveUser(object obj)
        {
            return true;
        }

        private void RemoveUser(object obj)
        {
            //数据库删除选定的User
            using mvvmlogindbModel db = new mvvmlogindbModel();
            db.Users.Remove(SelectUser);
            db.SaveChanges();

            //UserList删除选定的User，其类型实现了INotifyCollectionChanged接口
            UserList.Remove(SelectUser);
        }

        private bool CanAddUser(object obj)
        {
            return true;
        }

        private void AddUser(object obj)
        {
            AddUserView addUserView = new AddUserView();
            addUserView.ShowDialog();
        }
    }
}
