using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Habbit.Resources.Models;
using System.Net.Http;
using Auth0.ManagementApi.Models;
using MongoDB.Driver;

public class TaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;

    }

    private readonly IMongoCollection<Habbit.Resources.Models.Task> _tasks;

    public TaskService(IMongoDatabase database)
    {
        _tasks = database.GetCollection<Habbit.Resources.Models.Task>("tasks");
    }

    // Метод для створення нового завдання
    public async Task<bool> CreateAsync(Habbit.Resources.Models.Task task)
    {
        try
        {
            task.Id = string.Empty;
            // Перетворюємо завдання в JSON
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Викликаємо ваш API
            var response = await _httpClient.PostAsync("https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks", content);

            // Перевіряємо, чи вдалося створити завдання через API
            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error creating task: {errorDetails}");
                return false; // Якщо помилка, повертаємо false
            }

            return true; // Якщо успішно, повертаємо true
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception creating task: {ex.Message}");
            return false; // Якщо виникла помилка, повертаємо false
        }
    }

    // Метод для отримання завдання за ID
    public async Task<Habbit.Resources.Models.Task> GetByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Habbit.Resources.Models.Task>();
        }
        else
        {
            throw new Exception($"Error fetching task: {response.StatusCode}");
        }
    }

    // Метод для отримання всіх завдань користувача
    public async Task<List<Habbit.Resources.Models.Task>> GetByUserIdAsync(string userId)
    {
        var response = await _httpClient.GetAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks/user/{userId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Habbit.Resources.Models.Task>>();
        }
        else
        {
            throw new Exception($"Error fetching user tasks: {response.StatusCode}");
        }
    }

    // Метод для оновлення завдання
    public async Task<bool> UpdateAsync(string id, Habbit.Resources.Models.Task updatedTask)
    {
        var json = JsonSerializer.Serialize(updatedTask);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks/{id}", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error updating task: {errorDetails}");
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> MarkTaskAsCompletedAsync(string taskId)
    {
        var response = await _httpClient.PutAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks/complete/{taskId}", null);
        return response.IsSuccessStatusCode;
    }

    // Метод для видалення завдання
    public async Task<bool> DeleteAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error deleting task: {errorDetails}");
        }

        return response.IsSuccessStatusCode;
    }

    // Метод для позначення завдання як виконаного
    public async Task<bool> MarkAsCompletedAsync(string id)
    {
        var task = await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
        if (task == null) return false;

        task.CompletionDate = DateTime.UtcNow; // Встановлюємо поточну дату як дату завершення
        var result = await _tasks.ReplaceOneAsync(t => t.Id == id, task);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }


    public async Task<bool> UpdateTaskAsync(Habbit.Resources.Models.Task task)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://habbit-api-dbesdvgkhefgdwdh.polandcentral-01.azurewebsites.net/api/tasks/{task.Id}", task);
        return response.IsSuccessStatusCode;
    }

}