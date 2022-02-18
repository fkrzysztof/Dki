$("body").on("click", ".killButton", function (event) {
    event.preventDefault();
    let toHide = $(this).closest(".project-item");
    let send = $(this).data("id");
    $.post("/Projects/KillJS", { id: send }, function (data) {
        if (data == true) {
            toHide.fadeOut();
            refresh();
        }
        else {
            alert("Wystąpił problem..");
        }
    });
});