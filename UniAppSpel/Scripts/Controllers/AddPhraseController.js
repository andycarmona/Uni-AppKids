var AddPhraseController = function ($scope, $http) {
    $scope.models = {
        helloAngular: 'New one!'
    };
    $scope.words = [
        { "WordName": "Tes", "SoundFile": "Doe.wav" },
        { "WordName": "Anna", "SoundFile": "Smith.wav" }
    ];
    $scope.sentence = 'Hello there how are you today?';

    $scope.formData = {};
    $scope.processForm = function () {

        $scope.formData.words = $scope.words;
        var json_text = JSON.stringify($scope.words, null, 2);
        var urlPhrase = "http://dnndev.me/DesktopModules/DataExchange/API/Example/AddPhrase?listOfWords=" + json_text;
        $http({
            url: urlPhrase,
            method: "POST",
            dataType: "json",
            headers: {
                'Content-Type': 'x-www-form-urlencoded'
            }
        }).success(function (data) {
            console.log(data);

            if (!data.success) {
                $scope.errorName = data.errors.name;
                $scope.errorSuperhero = data.errors.superheroAlias;
            } else {
                $scope.message = data.message;
            }
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

    $scope.buildSentance = function (w) {

        var words = [];

        for (var i = 0; i < $scope.words.length; i++) {
            var word = $scope.words[i].WordName;
            if (word.replace(/\s+/g, '') !== '') {
                words.push(word);

            }
        }

        $scope.sentence = words.join(' ');
        if (w.WordName.indexOf(' ') > -1) {
            $scope.parseSentenceDebounced();
        }

    }

    $scope.parseSentence();
}
AddPhraseController.$inject = ['$scope', '$http'];