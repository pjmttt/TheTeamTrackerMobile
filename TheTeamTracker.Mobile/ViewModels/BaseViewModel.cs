using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using TheTeamTracker.Mobile.Classes;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace TheTeamTracker.Mobile.ViewModels
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		private IProgressDialog _loadingDialog;
		bool isBusy = false;
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				SetProperty(ref isBusy, value);
				if (value)
				{
					_loadingDialog = Acr.UserDialogs.UserDialogs.Instance.Loading("Loading");
				}
				else if (_loadingDialog != null)
				{
					_loadingDialog.Hide();
				}
			}
		}

		bool isRefreshing = false;
		public bool IsRefreshing
		{
			get { return isRefreshing; }
			set { SetProperty(ref isRefreshing, value); }
		}

		string title = string.Empty;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName]string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		protected async Task<bool> runTask(Func<Task> task, string prompt = null)
		{
			if (IsBusy)
				return false;

			if (!string.IsNullOrEmpty(prompt))
			{
				var confirm = await UserDialogs.Instance.ConfirmAsync(prompt);
				if (!confirm) return false;
			}

			if (!IsRefreshing)
				IsBusy = true;

			try
			{
				await task();
			}
			catch (Exception ex)
			{
				IsBusy = false;
				IsRefreshing = false;
				ExceptionHelper.ShowException(ex);
				return false;
			}
			finally
			{
				IsBusy = false;
				IsRefreshing = false;
			}
			return true;
		}


		protected async Task<T> runTask<T>(Func<Task<T>> task, string prompt = null) where T: class
		{
			if (IsBusy)
				return null;

			if (!string.IsNullOrEmpty(prompt))
			{
				var confirm = await UserDialogs.Instance.ConfirmAsync(prompt);
				if (!confirm) return null;
			}

			T result = null;

			if (!IsRefreshing)
				IsBusy = true;

			try
			{
				result = await task();
			}
			catch (Exception ex)
			{
				IsBusy = false;
				IsRefreshing = false;
				ExceptionHelper.ShowException(ex);
			}
			finally
			{
				IsBusy = false;
				IsRefreshing = false;
			}
			return result;
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
