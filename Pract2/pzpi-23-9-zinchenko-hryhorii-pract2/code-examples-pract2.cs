// code-examples-pract2.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//////////////////////////////////////////////////////////////
// ПРИКЛАД 1 – Завантаження відео (Upload)
//////////////////////////////////////////////////////////////

public class VideoUploadRequest
{
    public string UserId { get; set; }
    public string FileName { get; set; }
    public long FileSize { get; set; }
}

public class VideoMetadata
{
    public string VideoId { get; set; }
    public string UserId { get; set; }
    public string Status { get; set; }
    public DateTime UploadedAt { get; set; }
}

public class VideoUploadService
{
    public async Task<VideoMetadata> UploadVideoAsync(VideoUploadRequest request)
    {
        // Створення метаданих відео
        var metadata = new VideoMetadata
        {
            VideoId = Guid.NewGuid().ToString(),
            UserId = request.UserId,
            Status = "Uploaded",
            UploadedAt = DateTime.UtcNow
        };

        Console.WriteLine($"Video {metadata.VideoId} uploaded.");

        // Публікація події
        await PublishVideoUploadedEventAsync(metadata);

        return metadata;
    }

    private Task PublishVideoUploadedEventAsync(VideoMetadata metadata)
    {
        Console.WriteLine(
            $"Event published: VideoUploaded, id = {metadata.VideoId}");

        return Task.CompletedTask;
    }
}

//////////////////////////////////////////////////////////////
// ПРИКЛАД 2 – Обробка відео (Processing)
//////////////////////////////////////////////////////////////

public class VideoUploadedEvent
{
    public string VideoId { get; set; }
    public string UserId { get; set; }
}

public class VideoProcessingService
{
    public async Task ProcessVideoAsync(VideoUploadedEvent videoEvent)
    {
        Console.WriteLine(
            $"Start transcoding video {videoEvent.VideoId}");

        await TranscodeVideoAsync(videoEvent.VideoId);

        Console.WriteLine(
            $"Video {videoEvent.VideoId} processed successfully.");

        await UpdateVideoStatusAsync(videoEvent.VideoId, "Ready");
    }

    private Task TranscodeVideoAsync(string videoId)
    {
        Console.WriteLine(
            $"Creating several video qualities for {videoId}");

        return Task.CompletedTask;
    }

    private Task UpdateVideoStatusAsync(string videoId, string status)
    {
        Console.WriteLine(
            $"Video {videoId} status changed to {status}");

        return Task.CompletedTask;
    }
}

//////////////////////////////////////////////////////////////
// ПРИКЛАД 3 – Рекомендації (Recommendation)
//////////////////////////////////////////////////////////////

public class UserAction
{
    public string UserId { get; set; }
    public string VideoId { get; set; }
    public string ActionType { get; set; }
}

public class RecommendationService
{
    public List<string> GetRecommendedVideos(
        string userId,
        List<UserAction> actions)
    {
        var watchedVideos = actions
            .Where(a => a.UserId == userId)
            .Select(a => a.VideoId)
            .Distinct()
            .ToList();

        Console.WriteLine(
            $"User {userId} has watched {watchedVideos.Count} videos.");

        // Повернення рекомендацій
        return new List<string>
        {
            "video_101",
            "video_205",
            "video_309"
        };
    }
}
