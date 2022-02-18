$.get("/Home/LogoJS", function (data) {
    if (data != null) {
        let logo = document.getElementById("logo");
        logo.setAttribute("src", data);
    }
    else {
        alert("Wystąpił problem..");
    }
});
