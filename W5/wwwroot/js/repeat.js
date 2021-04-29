var vocabulary = [];
var contentHandler;
var word;
var getVocFlag = true;

$(document).ready(function () {
    GetVocabulary();
});

function GetVocabulary() {
    
    $.ajax(
        {
            type: "GET",
            url: "/User/Repeat/GetAll?SetId=" + setId,
            success: function (data) {
                $.each(data, function (i, v) {
                    vocabulary.push({
                        nativeWord: v.nativeWord,
                        learnWord: v.learnWord,
                        nativeSentence: v.nativeSentence,
                        learnSentence: v.learnSentence
                    });
                   
                })
                nextWord();
            },
            error: function (error) {
                alert('failed');
            }
        }
    )
}

function nextWord() {
    if (vocabulary.length == 0) {
        finish();
        return;
    }
    word = vocabulary.pop();

    document.getElementById("content").innerHTML = 
    `<div class="border">
         <br /> <br /> <br />
        <h1 style="text-align:center;" class="nativeW">`+ word.nativeWord +`</h1>
        <div class="form-group row mt-5 mb-5">
            <div class="col-1"></div>
            <div class="col-5 ml-5">
                <input id="answer" class="form-control" type="text" placeholder="Podaj odpowiedź" />
            </div>
            <div class="col-4">
                <button class="btn-info form-control" onclick="check()">Sprawdź!</button>
            </div>
        </div>
        <h5 style="text-align:center;">Pozostało `+vocabulary.length+` słowek do powtórzenia</h5>
        <br /> <br /> <br /> <br />
    </div >`;
    
}

function check() {

    var answer = document.getElementById("answer").value;
    console.log(answer);

    document.getElementById("content").innerHTML = `<div class="border">
        <br /> <br />
        <h1 style="text-align:center;" class="nativeW">`+ word.nativeWord +`</h1>
        <div class="form-group row mt-5 mb-5">
            <div class="col-4">
                <h3>Prawidłowa odpowiedź: </h3>
            </div>
            <div class="col-6">
                <h3>`+ word.learnWord +`</h3>
            </div>
        </div>
        <div class="form-group row mt-5 mb-5">
            <div class="col-4">
                <h3>Twoja odpowiedź: </h3>
            </div>
            <div class="col-6">
                <h3>`+ answer +`</h3>
            </div>
        </div>
        
        <div class="form-group row mt-5 mb-5">
            <div class="col-4"><h4>Przykładowe zdanie:</h4></div>
            <div class="col-4">
                <h4>`+ word.nativeSentence +`</h4>
            </div>
            <div class="col-4">
                <h4>`+ word.learnSentence +` </h4>
            </div>
        </div>
        <div class="col-10 ml-5">
            <button class="btn-success form-control" onclick="nextWord()">Dalej</button>
        </div>
        <br /> <br /> <br /> <br />
    </div>`;
    
}

function finish() {

    BackToIndex("/User/Repeat/DeleteAndBack?Id=" + rEventId);
}

function BackToIndex(url) {
    swal({
        title: "Gratulacje, powtórzyłeś całe słownictwo",
        text: "Wróć do poprzedniej strony by wybrać kolejny zestaw",
        icon: "success",
        buttons: true,
        dangerMode: true
    }).then((click) => {
        if (click) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    window.location.href = data;
                }
            });
        }
    });
}