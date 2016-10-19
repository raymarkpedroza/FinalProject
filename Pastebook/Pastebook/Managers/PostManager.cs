using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pastebook.PastebookServiceReference;
using Pastebook.Models;
using Pastebook.Mappers;

namespace Pastebook.Managers
{
    public class PostManager
    {
        PastebookServiceClient pastebookServiceClient = new PastebookServiceClient();
        public int CreatePost(PostModel post)
        {
            CreatePostRequest request = new CreatePostRequest();
            request.Post = Mapper.MapMVCPostModelToWCFPostEntity(post);

            CreatePostResponse response = new CreatePostResponse();
            response = pastebookServiceClient.CreatePost(request);

            return response.Result;
        }

        public List<UserPostModel> RetrieveUserPosts(int id)
        {
            List<UserPostModel> listOfPostsWithPoster = new List<UserPostModel>();
            List<UserCommentModel> listOfCommentsWithCommenters;
            List<LikeModel> listOfLikes;

            RetrievePostsRequest retrievePostsRequest = new RetrievePostsRequest();
            retrievePostsRequest.UserId = id;
            retrievePostsRequest.ListOfFriendsId = null;

            RetrievePostsResponse retrievePostsResponse = new RetrievePostsResponse();
            retrievePostsResponse = pastebookServiceClient.RetrievePosts(retrievePostsRequest);


            foreach (var post in retrievePostsResponse.ListOfPosts)
            {
                RetrieveUserByIdRequest retrievePosterRequest = new RetrieveUserByIdRequest();
                retrievePosterRequest.Id = post.PosterId;
                RetrieveUserByIdResponse retrievePosterResponse = new RetrieveUserByIdResponse();
                retrievePosterResponse = pastebookServiceClient.RetrieveUserById(retrievePosterRequest);

                RetrieveUserByIdRequest retrieveProfileOwnerRequest = new RetrieveUserByIdRequest();
                retrieveProfileOwnerRequest.Id = post.ProfileOwnerId;
                RetrieveUserByIdResponse retrieveProfileOwnerResponse = new RetrieveUserByIdResponse();
                retrieveProfileOwnerResponse = pastebookServiceClient.RetrieveUserById(retrieveProfileOwnerRequest);

                RetrieveCommentRequest retrieveCommentRequest = new RetrieveCommentRequest();
                retrieveCommentRequest.PostId = post.Id;

                RetrieveCommentResponse retrieveCommentResponse = new RetrieveCommentResponse();
                retrieveCommentResponse = pastebookServiceClient.RetrieveComment(retrieveCommentRequest);

                listOfCommentsWithCommenters = new List<UserCommentModel>();
                foreach (var comment in retrieveCommentResponse.ListOfComments)
                {
                    RetrieveUserByIdRequest retrieveCommenterRequest = new RetrieveUserByIdRequest();
                    retrieveCommenterRequest.Id = post.PosterId;
                    RetrieveUserByIdResponse retrieveCommenterResponse = new RetrieveUserByIdResponse();
                    retrieveCommenterResponse = pastebookServiceClient.RetrieveUserById(retrieveCommenterRequest);

                    UserCommentModel commenterComment = new UserCommentModel();
                    commenterComment.Comment = Mapper.MapWCFCommentEntityToMVCCommentModel(comment);
                    commenterComment.User = Mapper.MapWCFUserEntityToMVCUserModel(retrieveCommenterResponse.User);
                    listOfCommentsWithCommenters.Add(commenterComment);
                }

                RetrieveLikeRequest retrieveLikeRequest = new RetrieveLikeRequest();
                retrieveLikeRequest.PostId = post.Id;

                RetrieveLikeResponse retrieveLikeResponse = new RetrieveLikeResponse();
                retrieveLikeResponse = pastebookServiceClient.RetrieveLike(retrieveLikeRequest);

                listOfLikes = new List<LikeModel>();
                foreach (var like in retrieveLikeResponse.ListOfLikes)
                {
                    listOfLikes.Add(Mapper.MapWCFLikeEntityToMVCLikeModel(like));
                }

                listOfPostsWithPoster.Add(new UserPostModel() { Poster = Mapper.MapWCFUserEntityToMVCUserModel(retrievePosterResponse.User), ProfileOwner = Mapper.MapWCFUserEntityToMVCUserModel(retrieveProfileOwnerResponse.User), Post = Mapper.MapWCFPostEntityToMVCPostModel(post), ListOfCommentsWithCommenters = listOfCommentsWithCommenters, ListOfLikes = listOfLikes });
            }

            return listOfPostsWithPoster.OrderByDescending(x=>x.Post.CreatedDate).ToList();
        }

        public List<UserPostModel> RetrieveNewsfeedPosts(int id)
        {
            List<UserPostModel> listOfPostsWithPoster = new List<UserPostModel>();
            List<UserCommentModel> listOfCommentsWithCommenters;
            List<LikeModel> listOfLikes;

            RetrieveFriendsRequest retrieveFriendsRequest = new RetrieveFriendsRequest();
            retrieveFriendsRequest.UserId = id;

            RetrieveFriendsResponse retrieveFriendsResponse = new RetrieveFriendsResponse();
            retrieveFriendsResponse = pastebookServiceClient.RetrieveFriends(retrieveFriendsRequest);

            RetrievePostsRequest retrievePostsRequest = new RetrievePostsRequest();
            retrievePostsRequest.UserId = id;

            retrievePostsRequest.ListOfFriendsId = new List<int>();

            foreach (var friend in retrieveFriendsResponse.listOfFriends)
            {
                if ((retrievePostsRequest.ListOfFriendsId.Any(x => x == friend.UserId) || retrievePostsRequest.ListOfFriendsId.Any(x => x == friend.FriendId)) == true)
                {
                    if (friend.UserId == id && friend.Request == 'Y')
                    {
                        retrievePostsRequest.ListOfFriendsId.Add(friend.FriendId);
                    }

                    if (friend.FriendId == id && friend.Request == 'Y')
                    {
                        retrievePostsRequest.ListOfFriendsId.Add(friend.UserId);
                    }
                }
            }

            RetrievePostsResponse retrievePostsResponse = new RetrievePostsResponse();
            retrievePostsResponse = pastebookServiceClient.RetrievePosts(retrievePostsRequest);


            foreach (var post in retrievePostsResponse.ListOfPosts)
            {
                RetrieveUserByIdRequest retrievePosterRequest = new RetrieveUserByIdRequest();
                retrievePosterRequest.Id = post.PosterId;
                RetrieveUserByIdResponse retrievePosterResponse = new RetrieveUserByIdResponse();
                retrievePosterResponse = pastebookServiceClient.RetrieveUserById(retrievePosterRequest);

                RetrieveUserByIdRequest retrieveProfileOwnerRequest = new RetrieveUserByIdRequest();
                retrieveProfileOwnerRequest.Id = post.ProfileOwnerId;
                RetrieveUserByIdResponse retrieveProfileOwnerResponse = new RetrieveUserByIdResponse();
                retrieveProfileOwnerResponse = pastebookServiceClient.RetrieveUserById(retrieveProfileOwnerRequest);

                RetrieveCommentRequest retrieveCommentRequest = new RetrieveCommentRequest();
                retrieveCommentRequest.PostId = post.Id;

                RetrieveCommentResponse retrieveCommentResponse = new RetrieveCommentResponse();
                retrieveCommentResponse = pastebookServiceClient.RetrieveComment(retrieveCommentRequest);

                listOfCommentsWithCommenters = new List<UserCommentModel>();
                foreach (var comment in retrieveCommentResponse.ListOfComments)
                {
                    RetrieveUserByIdRequest retrieveCommenterRequest = new RetrieveUserByIdRequest();
                    retrieveCommenterRequest.Id = comment.PosterId;
                    RetrieveUserByIdResponse retrieveCommenterResponse = new RetrieveUserByIdResponse();
                    retrieveCommenterResponse = pastebookServiceClient.RetrieveUserById(retrieveCommenterRequest);

                    UserCommentModel commenterComment = new UserCommentModel();
                    commenterComment.Comment = Mapper.MapWCFCommentEntityToMVCCommentModel(comment);
                    commenterComment.User = Mapper.MapWCFUserEntityToMVCUserModel(retrieveCommenterResponse.User);
                    listOfCommentsWithCommenters.Add(commenterComment);
                }

                RetrieveLikeRequest retrieveLikeRequest = new RetrieveLikeRequest();
                retrieveLikeRequest.PostId = post.Id;

                RetrieveLikeResponse retrieveLikeResponse = new RetrieveLikeResponse();
                retrieveLikeResponse = pastebookServiceClient.RetrieveLike(retrieveLikeRequest);

                listOfLikes = new List<LikeModel>();
                foreach (var like in retrieveLikeResponse.ListOfLikes)
                {
                    listOfLikes.Add(Mapper.MapWCFLikeEntityToMVCLikeModel(like));
                }

                listOfPostsWithPoster.Add(new UserPostModel() { Poster = Mapper.MapWCFUserEntityToMVCUserModel(retrievePosterResponse.User), ProfileOwner = Mapper.MapWCFUserEntityToMVCUserModel(retrieveProfileOwnerResponse.User), Post = Mapper.MapWCFPostEntityToMVCPostModel(post), ListOfCommentsWithCommenters = listOfCommentsWithCommenters, ListOfLikes = listOfLikes });
            }

            return listOfPostsWithPoster.OrderByDescending(x => x.Post.CreatedDate).ToList();
        }
    }
}