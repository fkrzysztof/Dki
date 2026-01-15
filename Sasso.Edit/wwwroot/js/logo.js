$.get("/Home/LogoJS", function (data) {
    if (data != null) {
        let logo = document.getElementsByClassName('page-logo');
        for (let i = 0; i < logo.length; i++) {
            logo[i].setAttribute("src", data);
        }
    }
    else {
        alert("Wystąpił problem..");
    }
});
