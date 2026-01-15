$(".nav-link").mouseover(function (e) {
    $(this).toggleClass("name");
}).mouseout(function (e) {
    $(this).toggleClass("name");
});

$(document).ready(function () {

    //rodo **********************************************************************
    const xButton = document.getElementById("rodoc");
    const rodoToHide = document.getElementById("rodo");
    if (sessionStorage.getItem("rodo") == "ok") {
        rodoToHide.style.display = "none";
    }


    xButton.addEventListener("click", function (e) {
        e.preventDefault();
        rodoToHide.style.display = "none";
        sessionStorage.setItem("rodo", "ok");
    });

    // Get the button ************************************************************
    let mybuttonTop = document.getElementById("myBtnTop");
    let mybuttonLeft = document.getElementById("myBtnLeft");

    // When the user scrolls down 20px from the top of the document, show the button
    window.onscroll = function () { scrollFunction() };

    function scrollFunction() {
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            mybuttonTop.style.display = "block";
            mybuttonLeft.style.display = "block";
        } else {
            mybuttonTop.style.display = "none";
            mybuttonLeft.style.display = "none";
        }
    }

    // When the user clicks on the button, scroll to the top of the document
    mybuttonTop.addEventListener('click', () => {
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0
    });
    mybuttonLeft.addEventListener('click', () => {
        history.back();
    });

});