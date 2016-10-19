using Pastebook.Mappers;
using Pastebook.Models;
using Pastebook.PastebookServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.Managers
{
    public class ReactionManager
    {
        PastebookServiceClient pastebookServiceClient = new PastebookServiceClient();
        public int AddLike(LikeModel like)
        {
            AddLikeRequest request = new AddLikeRequest();
            request.Like = Mapper.MapMVCLikeModelToWCFLikeEntity(like);
            AddLikeResponse response = new AddLikeResponse();
            response = pastebookServiceClient.AddLike(request);

            return response.Result;
        }

        public int AddComment(CommentModel comment)
        {
            AddCommentRequest request = new AddCommentRequest();
            request.Comment = Mapper.MapMVCCommentModelToWCFCommentEntity(comment);

            AddCommentResponse response = new AddCommentResponse();
            response = pastebookServiceClient.AddComment(request);

            return response.Result;
        }

    }
}