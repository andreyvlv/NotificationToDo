using Plugin.LocalNotification;
using NotificationToDo.Data;
using NotificationToDo.Models;

namespace NotificationToDo.Views;

[QueryProperty("Item", "Item")]
public partial class TodoItemPage : ContentPage
{
	TodoItem item;
	public TodoItem Item
	{
		get => BindingContext as TodoItem;
		set => BindingContext = value;
	}
    TodoItemDatabase database;
    public TodoItemPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Note))
        {
            await DisplayAlert("Требуется ввести текст заметки", "Введите пожалуйста текст", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;

        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    async void OnCreateNotificationClicked(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;

        var notifyId = Item.ID;

        var request = new NotificationRequest
        {
            NotificationId = notifyId,
            Title = Item.Note,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(1),
            }
        };
        
        await LocalNotificationCenter.Current.Show(request);
        await Shell.Current.GoToAsync("..");
    }
}