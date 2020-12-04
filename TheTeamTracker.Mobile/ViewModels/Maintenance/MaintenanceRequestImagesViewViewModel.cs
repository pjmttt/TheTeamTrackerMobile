using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TheTeamTracker.Mobile.Classes;
using TheTeamTracker.Mobile.DataLayer.Models;
using TheTeamTracker.Mobile.DataLayer.Services;
using TheTeamTracker.Mobile.Views;
using Xamarin.Forms;

namespace TheTeamTracker.Mobile.ViewModels.Maintenance
{
	public class MaintenanceRequestImagesViewViewModel : BaseViewModel
	{
		public MaintenanceRequest Request { get; private set; }
		public ICommand TakePictureCommand { get; set; }
		public ICommand DeleteCommand { get; set; }
		public ObservableCollection<MaintenanceImage> Images { get; }
		public List<MaintenanceImage> NewImages { get; }
		public List<MaintenanceRequestImage> DeletedImages { get; }
		private bool _suspendSave;


		public MaintenanceRequestImagesViewViewModel(bool suspendSave)
		{
			_suspendSave = suspendSave;
			this.TakePictureCommand = new Command(takePicture);
			this.DeleteCommand = new Command(async (img) => await deleteImage(img as MaintenanceImage));
			Images = new ObservableCollection<MaintenanceImage>();
			NewImages = new List<MaintenanceImage>();
			DeletedImages = new List<MaintenanceRequestImage>();
		}

		public async System.Threading.Tasks.Task SetRequest(MaintenanceRequest request)
		{
			this.Request = request;
			OnPropertyChanged("Request");
			foreach (var img in this.Request.MaintenanceRequestImages)
			{
				var stream = await DataService.GetAsync($"maintenanceRequestImages/{img.MaintenanceRequestImageIdValue}",
					img.ImageType);
				byte[] bytes = null;
				using (var ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					bytes = ms.ToArray();
				}
				Images.Add(new MaintenanceImage()
				{
					MaintenanceRequestImage = img,
					ImageBytes = bytes
				});
			}
		}

		private async void takePicture()
		{
			MediaFile photo;
			try
			{
				photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
				{
					PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
				});
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				ExceptionHelper.ShowException(ex);
				return;
			}

			if (photo != null)
			{
				var stream = photo.GetStreamWithImageRotatedForExternalStorage();
				var maintImage = new MaintenanceRequestImage();
				var ms = new MemoryStream();
				stream.CopyTo(ms);
				var bytes = ms.ToArray();
				maintImage.ImageType = "image/jpeg";
				if (!_suspendSave)
				{
					var fileToSend = new FileToSend();
					fileToSend.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
					fileToSend.Bytes = bytes;
					fileToSend.ContentType = maintImage.ImageType;
					var loadingDialog = Acr.UserDialogs.UserDialogs.Instance.Loading("Loading");
					try
					{
						var results = await DataService.SendFiles<List<MaintenanceRequestImage>>($"maintenanceRequestImage/{this.Request.MaintenanceRequestIdValue}",
							new List<FileToSend>() { fileToSend }, true);
						maintImage.MaintenanceRequestImageIdValue = results[0].MaintenanceRequestImageIdValue;
						loadingDialog.Hide();
					}
					catch (Exception ex)
					{
						loadingDialog.Hide();
						if (ex.InnerException != null)
						{
							ex = ex.InnerException;
						}
						ExceptionHelper.ShowException(ex);
					}
				}
				this.Request.MaintenanceRequestImages.Add(maintImage);
				var newImage = new MaintenanceImage()
				{
					MaintenanceRequestImage = maintImage,
					ImageBytes = bytes
				};
				this.NewImages.Add(newImage);
				this.Images.Add(newImage);
			}

		}

		private async System.Threading.Tasks.Task deleteImage(MaintenanceImage image)
		{
			bool success = true;
			if (!_suspendSave)
			{
				success = await runTask(async () =>
				{
					await DataService.DeleteItemAsync("maintenanceRequestImages", image.MaintenanceRequestImage.MaintenanceRequestImageIdValue);
				}, "Are you sure you want to delete this image?");
			}
			if (success)
			{
				DeletedImages.Add(image.MaintenanceRequestImage);
				this.Request.MaintenanceRequestImages.Remove(image.MaintenanceRequestImage);
				this.Images.Remove(image);
			}
		}
	}

	public class MaintenanceImage
	{
		public byte[] ImageBytes { get; set; }
		public ImageSource ImageSource { get { return ImageSource.FromStream(() => new MemoryStream(ImageBytes)); } }
		public MaintenanceRequestImage MaintenanceRequestImage { get; set; }
	}
}
