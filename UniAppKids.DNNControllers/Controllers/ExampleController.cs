namespace UniAppKids.DNNControllers.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security.Membership;
    using DotNetNuke.Security.Roles;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Web.Api;

    using UniAppKids.DNNControllers.Helpers;

    using UniAppSpel.Helpers;

    using Uni_AppKids.Application.Dto;
    using Uni_AppKids.Application.Services;

    public class ExampleController : ControllerBase
    {

        private readonly DictionaryService aDictionaryService = new DictionaryService();
        private readonly WordService aWordService = new WordService();
        private readonly PhraseService aPhraseService = new PhraseService();

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public async Task<List<string>> PostAsync()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");

                MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                List<string> messages = new List<string>();
                foreach (var file in streamProvider.FileData)
                {
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                }

                return messages;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
        }
     

        [DnnAuthorize]
        [AcceptVerbs("POST")]
        public HttpResponseMessage AddPhrase(string listOfWords, int dictionaryId)
        {
            if (listOfWords.Length == 0)
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Invalid parameters, Please check there is elements in array");
            }

            var delimiter = " ";
            var wordList = Json.Deserialize<List<WordDto>>(listOfWords);
            wordList.Select(c => { c.CreationTime = DateTime.Now; return c; }).ToList();


            try
            {
                var listNoRepeatedElements = wordList.Distinct(new DistinctItemComparer()).ToList();
                this.aWordService.BulkInsertOfWords(listNoRepeatedElements);

                if (wordList.Count == 1)
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                }

                var sentence = wordList.Select(i => i.WordName).Aggregate((i, j) => i + delimiter + j);
                var listOfWordsId = this.aWordService.GetIdOfWords(wordList);
                var aPhrase = new PhraseDto
                                  {
                                      PhraseText = sentence,
                                      CreationTime = DateTime.Now,
                                      WordsIds = string.Join(",", listOfWordsId),
                                      AssignedDictionaryId = dictionaryId,
                                      UserName = UserController.GetCurrentUserInfo().Username
                                  };

                this.aPhraseService.InsertPhrase(aPhrase);
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                var listRepeatedWords = this.aWordService.GetRepeatedWords(wordList);
                var errorMessage = string.Format("Cannot insert duplicate words: {0}", string.Join(",", listRepeatedWords));
                return this.ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorMessage);
            }
        }


        public void createDnnUser(string UserName)
        {
            UserInfo newUser = new UserInfo();
            newUser.Username = UserName;
            newUser.PortalID = PortalSettings.PortalId;
            newUser.DisplayName = "John Doe";
            newUser.Email = "jdoe@email.com";
            newUser.FirstName = "John";
            newUser.LastName = "Doe";
            newUser.Profile.SetProfileProperty("SSN", "123-456-7890");

            UserCreateStatus rc = UserController.CreateUser(ref newUser);
            if (rc == UserCreateStatus.Success)
            {
                // Manual add role to user
                addRoleToUser(newUser, "Registered Users", DateTime.MaxValue);
            }
        }

        public bool addRoleToUser(UserInfo user, string roleName, DateTime expiry)
{
	var rc = false;
	var roleCtl = new RoleController();
	RoleInfo newRole = roleCtl.GetRoleByName(user.PortalID, roleName);
	if (newRole != null && user != null)
	{
		rc = user.IsInRole(roleName);
		roleCtl.AddUserRole(user.PortalID, user.UserID, newRole.RoleID, DateTime.MinValue, expiry);
		// Refresh user and check if role was added
		user = UserController.GetUserById(user.PortalID, user.UserID);
		rc = user.IsInRole(roleName);
	}
	return rc;
}
        [AcceptVerbs("GET")]
        public HttpResponseMessage checkUserAuthenticated()
        {
            var authenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            if (authenticated)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, UserController.GetCurrentUserInfo().Username);
            }

            return ControllerContext.Request.CreateResponse(
          HttpStatusCode.Unauthorized, "Not authorized");

        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetAllWordsInDictionary()
        {
            var wordList = this.aWordService.GetAllWords();
           
            if (!wordList.Any())
            {
                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "There is no words in dictionary");
            }

            return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, wordList);
        }

        [DnnAuthorize]
        [AcceptVerbs("GET")]
        public HttpResponseMessage GetWordsList(int dictionaryId, int indexOfPhraseList)
        {
            var errorMessage = string.Empty;
            try
            {
                var listOfPhrase = this.aPhraseService.GetListOfPhrase(dictionaryId);
                var wordsId = listOfPhrase[indexOfPhraseList].WordsIds;
                var listOfWords = this.aWordService.GetListOfWordsForAPhrase(wordsId);
                if (listOfWords.Any())
                {
                    return this.ControllerContext.Request.CreateResponse(HttpStatusCode.OK, listOfWords);
                }

                return this.ControllerContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    "Couldn't find any dictionary.");
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            return this.ControllerContext.Request.CreateResponse(
                      HttpStatusCode.BadRequest,
                      errorMessage);

        }

        public HttpResponseMessage GetDictionary(int dictionaryId)
        {
            var actualDictionary = aDictionaryService.GetADictionary(dictionaryId);

            if (actualDictionary != null)
            {
                return this.ControllerContext.Request.CreateResponse(
              HttpStatusCode.OK, actualDictionary);
            }

            return this.ControllerContext.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                "Couldn't find any dictionary.");
        }
    }
}