var EditPageController = function ($scope, $http, $window,$filter, userService) {

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/AddPhrase?dictionaryId=1&listOfWords=";
    var urlWordList = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetAllWordsInDictionary";
    var urlDictionary = "http://dnndev.me/DesktopModules/DataExchange/API/WordHandler/GetDictionary?dictionaryId=1";
    


    $scope.imagelist = [];
    $scope.words = [];
    $scope.sentence = "";

    GetWordList(urlWordList);
    GetDictionary();
   

    function GetDictionary() {
        userService.GetRequest(urlDictionary).success(function (request) {
            applyRemoteDataToDictionary(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });

    }

    function GetWordList(url) {
        userService.GetRequest(url).success(function (request) {
            applyRemoteDataToWordList(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
      
    }

   $scope.GetImageList=function (keyWord) {
        var url = "http://dnndev.me/DesktopModules/DataExchange/API/RemoteService/GetListOfImageUrl?wordToSearch=";
        userService.GetRequest(url+keyWord).success(function (request) {
            applyRemoteDataToImageList(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
    }

    function applyRemoteDataToImageList(request) {
        if (request !== null) {
            for (var index = 0; index < request.length; index++) {
                $scope.imagelist.push(request[index]);
            }
        }
    }

    function applyRemoteDataToDictionary(request) {

        $scope.dictionaries = request.DictionaryName;
      

    }
    function applyRemoteDataToWordList(request) {

        $scope.words_in_dictionary = request;
        
    }
    $scope.chooseImage= function(imageUrl) {
        $scope.imageurl = imageUrl;
    }

    $scope.processForm = function () {
        var wordsJsonFormat = JSON.stringify($scope.words, null, '');
  
        $scope.words = [];
        urlPhrase = urlPhrase + wordsJsonFormat;
     
        userService.PostRequest(urlPhrase).success(function (request) {
            wordsJsonFormat = "";
            return(request);
        }).error(function (request) {
            $scope.error = "An internal error has ocurred. " + request;
        });
  
    };

    $scope.parseSentence = function () {

        var words = $scope.sentence.split(/\s+/g);
        var wordObjects = [];

        for (var i = 0; i < words.length; i++) {
            wordObjects.push({ WordName: words[i] });
            
        }

        if ((words.length == 1) && (words[0] === '')) {
            $scope.words = [];
        } else {
            $scope.words = wordObjects;
    
        }

    };



    $scope.addWordFromDictionary = function (aWord) {
        
        $scope.words.push({ WordName: aWord });
        $scope.buildSentance();
        $scope.GetImageList(aWord);
    }
  

    $scope.buildSentance = function () {

        var words = [];

        for (var i = 0; i < $scope.words.length; i++) {
            var word = $scope.words[i].WordName;
            if (word.replace(/\s+/g, '') !== '') {
                words.push(word);
                
            }
        }

        $scope.sentence = words.join(' ');
        //if (w.WordName.indexOf(' ') > -1) {
        //    $scope.parseSentenceDebounced();
        //}

    }

    $scope.parseSentence();

}

EditPageController.$inject = ['$scope', '$http', '$window','$filter', 'userService'];