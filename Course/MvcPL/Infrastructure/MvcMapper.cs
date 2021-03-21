using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure
{
    public static class MvcMapper
    {
        public static PostViewModel ToPostViewModel(this BllPost photo)
        {
            return new PostViewModel
            {
                Id = photo.PostId,
                Image = photo.Image
            };
        }

        public static PostDetailsViewModel ToPostDetailsViewModel(this BllPost photo)
        {
            return new PostDetailsViewModel
            {
                Id = photo.PostId,
                Name = photo.Name,
                Description = photo.Description,
                Image = photo.Image,
                NumberOfLikes = photo.NumberOfLikes,
                Tags = photo.Tags.Select(t => t.Text),
                UploadDate = photo.UploadDate.ToLocalTime(),
                UserId = photo.User.UserId,
                UserLikesIds = photo.UserLikesEntity.Select(ul => ul.UserLikesId)
            };
        }

        public static PostOwnerViewModel ToPostOwnerViewModel(this BllUser user)
        {
            return new PostOwnerViewModel
            {
                NameOwner = user.Login,
                ProfilePostOwner = user.ProfilePhoto
            };
        }

        public static PostRatingViewModel ToPostRatingViewModel(this BllPost photo)
        {
            return new PostRatingViewModel
            {
                Id = photo.PostId,
                NumberOfLikes = photo.NumberOfLikes
            };
        }

        public static ProfileViewModel ToProfileViewModel(this BllUser user)
        {
            return new ProfileViewModel
            {
                Email = user.Email,
                Login = user.Login,
                Name = user.Name,
                Phone = user.Phone,
                ProfilePost = user.ProfilePhoto
            };
        }

        public static ProfileInfoViewModel ToProfileInfoViewModel(this BllUser user)
        {
            return new ProfileInfoViewModel
            {
                Name = user.Name,
                ProfilePost = user.ProfilePhoto,
                Language = user.LanguageId.ToString(),
                Country = user.CountryId.ToString(),
                Sex = user.SexId.ToString(),
                Age = user.AgeId.ToString()
            };
        }

        public static BllPost ToBllPost(this UploadPostViewModel photo, int userId)
        {
            return new BllPost
            {
                Name = photo.Name,
                Image = ToByteArray(photo.ImageFile),
                Description = photo.Description,
                Tags = ToTags(photo.Tags),
                UploadDate = DateTime.Now,
                UserLikesEntity = new List<BllUserLikesEntity>(),
                User = new BllUser { UserId = userId }
            };
        }

        public static BllPost ToBllPost(this UploadAdViewModel photo, int userId)
        {
            return new BllPost
            {
                Name = photo.Name,
                Image = photo.Photo,
                Description = photo.Description,
                Tags = ToTags(photo.Tags),
                UploadDate = DateTime.Now,
                UserLikesEntity = new List<BllUserLikesEntity>(),
                User = new BllUser { UserId = userId },
                LanguageId = int.TryParse(photo.Language, out var tempL) ? tempL : (int?)null,
                SexId = int.TryParse(photo.Sex, out var tempS) ? tempS : (int?)null,
                CountryId = int.TryParse(photo.Countries, out var tempC) ? tempC : (int?)null,
                AgeId = int.TryParse(photo.Age, out var tempA) ? tempA : (int?)null,
                IsAd = true,
                Price = photo.Price
            };
        }

        public static BllComment ToBllComment(this AddCommentViewModel model, int userId, string userName)
        {
            return new BllComment
            {
                PostId = model.PostId,
                Posted = DateTime.Now,
                Text = model.Text,
                User = new BllUser
                {
                    UserId = userId,
                    Name = userName
                }
            };
        }

        public static Author ToAuthor(this BllUser author)
        {
            return new Author
            {
                Id = author.UserId,
                Name = author.Name
            };
        }

        public static CommentViewModel ToCommentViewModel(this BllComment comment)
        {
            return new CommentViewModel
            {
                Id = comment.CommentId,
                Text = comment.Text,
                Author = comment.User.ToAuthor()
            };
        }

        public static byte[] ToByteArray(this HttpPostedFileBase photo)
        {
            if (photo == null)
            {
                return new byte[0];
            }
            photo.InputStream.Flush();
            byte[] thePictureAsBytes = new byte[photo.ContentLength];
            using (BinaryReader theReader = new BinaryReader(photo.InputStream))
            {
                thePictureAsBytes = theReader.ReadBytes(photo.ContentLength);
            }
            return thePictureAsBytes;
        }

        private static IEnumerable<BllTag> ToTags(string tags)
        {
            if (string.IsNullOrEmpty(tags))
                return new List<BllTag>();
            string[] splitedTags = tags.Split(' ');
            return splitedTags.Where(s => s.StartsWith("#")).Select(t => new BllTag { Text = t });
        }
    }
}