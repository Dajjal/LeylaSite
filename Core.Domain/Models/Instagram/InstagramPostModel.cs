using Core.Domain.Abstracts;

namespace Core.Domain.Models.Instagram;

/// <summary>
/// Посты в интаграме Entity
/// </summary>
/// <param name="title">Заголовок</param>
/// <param name="description">Описание</param>
/// <param name="likes">Количество лайков</param>
public class InstagramPostModel(string title, string description, int likes) : AbstractGuidModel
{
    public string Title { get; init; } = title;
    public string Description { get; init; } = description;
    public int Likes { get; init; } = likes;
}