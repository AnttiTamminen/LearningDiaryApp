function showInfo() {
    if (document.getElementById("infoText").style.display == "block") {
        document.getElementById("infoText").style.display = "none";
        document.getElementById("infoButt").style.background = '';
    } else {
        document.getElementById("infoText").style.display = "block";
        document.getElementById("infoButt").style.background = 'red';
    }
}

function loginPrompt() {
    var password = sessionStorage.getItem("passKey");
    if (password != "asd") {  //password is showing in browser debugging window -> to database?
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

function sortTopics(option) {
    var list = document.getElementById('allTop');
    var items = list.childNodes;
    var itemsArr = [];
    for (var i in items) {
        if (items[i].nodeType == 1) {
            itemsArr.push(items[i]);
        }
    }
    switch (option) {
        case "Title":
            itemsArr.sort(function(a, b) {
                return a.children[1].firstElementChild.firstElementChild.firstElementChild.innerHTML ==
                    b.children[1].children[0].children[0].children[0].innerHTML
                    ? 0
                    : (a.children[1].children[0].children[0].children[0].innerHTML >
                        b.children[1].children[0].children[0].children[0].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "Desc":
            itemsArr.sort(function(a, b) {
                return a.children[1].firstElementChild.children[1].children[1].innerHTML ==
                    b.children[1].firstElementChild.children[1].children[1].innerHTML
                    ? 0
                    : (a.children[1].firstElementChild.children[1].children[1].innerHTML >
                        b.children[1].firstElementChild.children[1].children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "Source":
            itemsArr.sort(function (a, b) {
                return a.children[1].firstElementChild.children[2].children[1].innerHTML ==
                    b.children[1].firstElementChild.children[2].children[1].innerHTML
                    ? 0
                    : (a.children[1].firstElementChild.children[2].children[1].innerHTML >
                        b.children[1].firstElementChild.children[2].children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "TSpent":
            itemsArr.sort(function (a, b) {
                return a.children[1].children[1].children[1].children[1].innerHTML ==
                    b.children[1].children[1].children[1].children[1].innerHTML
                    ? 0
                    : (a.children[1].children[1].children[1].children[1].innerHTML >
                        b.children[1].children[1].children[1].children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "TMaster":
            itemsArr.sort(function (a, b) {
                return a.children[1].children[1].firstElementChild.children[1].innerHTML ==
                    b.children[1].children[1].firstElementChild.children[1].innerHTML
                    ? 0
                    : (a.children[1].children[1].firstElementChild.children[1].innerHTML >
                        b.children[1].children[1].firstElementChild.children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "Prog":
            itemsArr.sort(function (a, b) {
                return a.children[1].firstElementChild.children[3].children[1].innerHTML ==
                    b.children[1].firstElementChild.children[3].children[1].innerHTML
                    ? 0
                    : (a.children[1].firstElementChild.children[3].children[1].innerHTML >
                        b.children[1].firstElementChild.children[3].children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "SDate":
            itemsArr.sort(function (a, b) {
                return a.children[1].children[1].children[2].children[1].innerHTML ==
                    b.children[1].children[1].children[2].children[1].innerHTML
                    ? 0
                    : (a.children[1].children[1].children[2].children[1].innerHTML >
                        b.children[1].children[1].children[2].children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        case "CDate":
            itemsArr.sort(function (a, b) {
                return a.children[1].children[1].children[3].children[1].innerHTML ==
                    b.children[1].children[1].children[3].children[1].innerHTML
                    ? 0
                    : (a.children[1].children[1].children[3].children[1].innerHTML >
                        b.children[1].children[1].children[3].children[1].innerHTML
                        ? 1
                        : -1);
            });
            break;

        default:
            break;
    }

    while (list.lastChild) {
        list.removeChild(list.lastChild);
    }

    for (var i = 0; i < itemsArr.length; i++) {
        list.appendChild(itemsArr[i]);
    }
}

var inputs = document.getElementsByClassName("form-control");
for (var i = 0; i < inputs.length; i++) {
    inputs[i].addEventListener('focus', (event) => {
        event.target.style.background = 'lightgreen';
    });

    inputs[i].addEventListener('blur', (event) => {
        event.target.style.background = '';
    });
}