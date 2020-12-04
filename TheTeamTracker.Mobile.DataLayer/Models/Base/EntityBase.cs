using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TheTeamTracker.Mobile.DataLayer.Classes;

namespace TheTeamTracker.Mobile.DataLayer.Models.Base
{
	public abstract class EntityBase : INotifyPropertyChanged
	{
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;

		public T GetForSave<T>() where T : EntityBase
		{
			var forSave = Common.Clone<T>((T)this);
			forSave.resetParentsChildren();
			return forSave;
		}

		internal abstract void resetParentsChildren();
	}
}
