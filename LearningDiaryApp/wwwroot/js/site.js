function showInfo() {
    document.getElementById("maini").style.display = "none";
    document.getElementById("infoText").style.display = "block";
}

function hideInfo() {
    document.getElementById("maini").style.display = "block";
    document.getElementById("infoText").style.display = "none";
}

function loginPrompt() {
    var password = sessionStorage.getItem("passKey");
    if (password != "asd") {  //password is showing in debugging of browser and also content can be switched on without password from there
        document.getElementById("allCont").style.display = "none";
        document.getElementById("login").style.display = "block";
    } else {
        document.getElementById("allCont").style.display = "block";
        document.getElementById("login").style.display = "none";
    }
}

function setPassword() {
    var password = document.forms[0].elements["logg"].value;
    sessionStorage.setItem("passKey", password);
    document.forms[0].reset();
    loginPrompt();
}

function logout() {
    sessionStorage.setItem("passKey", "password");
    loginPrompt();
}

function sortTopics(option, topics)
{
    switch (option) {
        case "Title":
            break;

        case "TSpent":
            break;

        case "TMaster":
            break;

        case "Prog":
            break;

        case "SDate":
            break;

        case "CDate":
            break;

        default:
            break;
    }
}