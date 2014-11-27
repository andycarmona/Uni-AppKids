var EditPageController = function ($scope, $http, $window, userService) {

    var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/AddPhrase?listOfWords=";
    var urlWordList = "http://dnndev.me/DesktopModules/DataExchange/API/Example/GetAllWordsInDictionary";

    $scope.dictionaryName = window.localStorage['dictionary_name'];

    $scope.words = [
        { "WordName": "Tes", "SoundFile": "Doe.wav" },
        { "WordName": "Anna", "SoundFile": "Smith.wav" }
    ];

    $scope.sentence = 'Hello there how are you today?';

    GetWordList(urlWordList);

    function GetWordList(url) {
        userService.GetRequest(url).then(
                         function (request) {
                             if (request == true) {
                                   $scope.errors = request;
                             }
                          
                             applyRemoteDataToWordList(request);

                         }
                     );
    }
    function applyRemoteDataToWordList(request) {

        $scope.words_in_dictionary = request;
        
    }

    $scope.processForm = function () {


        var wordsJsonFormat = JSON.stringify($scope.words, null, 2);
        urlPhrase = urlPhrase + wordsJsonFormat;
       

        userService.PostRequest(urlPhrase).then(
                     function (request) {
                         $scope.errors = request;
                         return request;
                     }
                 );
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

EditPageController.$inject = ['$scope', '$http', '$window', 'userService'];