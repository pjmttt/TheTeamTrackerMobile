using TheTeamTracker.Mobile.DataLayer.Models.Base;

namespace TheTeamTracker.Mobile.DataLayer.Models
{
	public class User : UserBase
	{
		public string DisplayName
		{
			get { return FirstName + (!string.IsNullOrEmpty(MiddleName) ? " " + MiddleName : "") + " " + LastName; }
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
