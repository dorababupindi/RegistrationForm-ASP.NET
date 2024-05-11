//alert("Dorababu")

$(".female").on("click", function () {
    $(".male").prop("checked", false)
})

$(".male").on("click", function () {
    $(".female").prop("checked", false)
})

let India = ["Andhra Pradesh", "Gujarat", "Tamil Nadu"];
let USA = ["Alabama", "Alsaka", "AriZona"]

var options = {
    India: ["AP", "TS", "GJ"],
    USA: ["AR","ZA"]

};
let statesHTML = document.querySelector(".State")
function UpdateStates() {
    var selectedcountry = document.querySelector(".Country").value;
    var states = options[selectedcountry];
    statesHTML.innerHTML = "";

    for (var i = 0; i < states.length; i++) {
        var option = document.createElement('option');
        option.text = states[i];
        option.value = states[i];
        statesHTML.appendChild(option);
    }
}

UpdateStates();
document.querySelector(".Country").addEventListener('change', UpdateStates);

$("#submit").on("click", function () {
    let name = $("#name").val();
    let age = $("#age").val();
    let email = $("#email").val();
    let number = $("#number").val();
    let country = $("#country").val();
    let state = $("#state").val();
    let gender
    if ($(".male")[0].checked) {
        gender = "M"
    }
    else {
        gender = "F"
    }
    let isvalid = true;

    if (Number(age) < 18 || Number(age)> 100) {
        alert("Please enter age between 18 to 100");
        isvalid = false;
    }
    if (email.indexOf("@") == -1) {
        alert("Please enter valid email");
        isvalid = false;
    }
    if (number.length !== 10) {
        alert("Please enter valid mobile number");
        isvalid = false;
    }


    if (isvalid) {
        let URL = '/Home/InsertUser?name=' + name + '&age=' + age + '&email=' + email + '&phnum=' + number + '&country=' + country + '&state=' + state + '&gender=' + gender;
        $.ajax({
            url: URL,
            type: 'GET',
            success: function (response) {
                if (response == "1") {
                    alert("Success");
                }
                else {
                    alert("Failed");
                }
                console.log(response);
            },
            error: function () {
                
                console.error('Error calling the controller method.');
            }
        });
    }
    
})